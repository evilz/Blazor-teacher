using BlazorTeacher.Api.Data;
using BlazorTeacher.Api.Extensions;
using BlazorTeacher.Shared.DTOs;
using BlazorTeacher.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorTeacher.Api.Controllers;

/// <summary>
/// REST API Controller for Course CRUD operations.
/// Demonstrates:
/// - Standard REST conventions
/// - Input validation
/// - EF Core operations
/// - Proper HTTP status codes
/// - Response DTOs
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IValidator<CreateCourseDto> _createValidator;
    private readonly IValidator<UpdateCourseDto> _updateValidator;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(
        AppDbContext context,
        IValidator<CreateCourseDto> createValidator,
        IValidator<UpdateCourseDto> updateValidator,
        ILogger<CoursesController> logger)
    {
        _context = context;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    /// <summary>
    /// GET api/courses
    /// Retrieves all courses with optional filtering.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(
        [FromQuery] CourseLevel? level = null,
        [FromQuery] bool? isPublished = null,
        [FromQuery] string? search = null)
    {
        _logger.LogInformation("Getting courses with filters: Level={Level}, Published={Published}, Search={Search}",
            level, isPublished, search);

        var query = _context.Courses
            .Include(c => c.Lessons)
            .AsQueryable();

        if (level.HasValue)
        {
            query = query.Where(c => c.Level == level.Value);
        }

        if (isPublished.HasValue)
        {
            query = query.Where(c => c.IsPublished == isPublished.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c =>
                c.Title.Contains(search) ||
                c.Description.Contains(search) ||
                c.Instructor.Contains(search));
        }

        var courses = await query
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return Ok(courses.Select(c => c.ToDto()));
    }

    /// <summary>
    /// GET api/courses/{id}
    /// Retrieves a single course by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        _logger.LogInformation("Getting course with ID: {CourseId}", id);

        var course = await _context.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course is null)
        {
            _logger.LogWarning("Course not found: {CourseId}", id);
            return NotFound($"Course with ID {id} not found.");
        }

        return Ok(course.ToDto());
    }

    /// <summary>
    /// GET api/courses/{id}/lessons
    /// Retrieves all lessons for a specific course.
    /// </summary>
    [HttpGet("{id}/lessons")]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetCourseLessons(int id)
    {
        _logger.LogInformation("Getting lessons for course: {CourseId}", id);

        var courseExists = await _context.Courses.AnyAsync(c => c.Id == id);
        if (!courseExists)
        {
            return NotFound($"Course with ID {id} not found.");
        }

        var lessons = await _context.Lessons
            .Where(l => l.CourseId == id)
            .OrderBy(l => l.Order)
            .ToListAsync();

        return Ok(lessons.Select(l => l.ToDto()));
    }

    /// <summary>
    /// POST api/courses
    /// Creates a new course.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto dto)
    {
        _logger.LogInformation("Creating new course: {Title}", dto.Title);

        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var course = dto.ToEntity();
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created course with ID: {CourseId}", course.Id);

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course.ToDto());
    }

    /// <summary>
    /// PUT api/courses/{id}
    /// Updates an existing course (partial update supported).
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> UpdateCourse(int id, UpdateCourseDto dto)
    {
        _logger.LogInformation("Updating course: {CourseId}", id);

        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var course = await _context.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course is null)
        {
            return NotFound($"Course with ID {id} not found.");
        }

        course.ApplyUpdate(dto);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated course: {CourseId}", id);

        return Ok(course.ToDto());
    }

    /// <summary>
    /// DELETE api/courses/{id}
    /// Deletes a course and its associated lessons.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        _logger.LogInformation("Deleting course: {CourseId}", id);

        var course = await _context.Courses.FindAsync(id);

        if (course is null)
        {
            return NotFound($"Course with ID {id} not found.");
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted course: {CourseId}", id);

        return NoContent();
    }

    /// <summary>
    /// GET api/courses/stats
    /// Retrieves statistics about courses.
    /// </summary>
    [HttpGet("stats")]
    public async Task<ActionResult<CourseStatsDto>> GetStats()
    {
        var stats = new CourseStatsDto
        {
            TotalCourses = await _context.Courses.CountAsync(),
            PublishedCourses = await _context.Courses.CountAsync(c => c.IsPublished),
            TotalLessons = await _context.Lessons.CountAsync(),
            CoursesPerLevel = await _context.Courses
                .GroupBy(c => c.Level)
                .ToDictionaryAsync(g => g.Key.ToString(), g => g.Count())
        };

        return Ok(stats);
    }
}

/// <summary>
/// DTO for course statistics.
/// </summary>
public record CourseStatsDto
{
    public int TotalCourses { get; init; }
    public int PublishedCourses { get; init; }
    public int TotalLessons { get; init; }
    public Dictionary<string, int> CoursesPerLevel { get; init; } = new();
}
