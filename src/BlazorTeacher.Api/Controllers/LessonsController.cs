using BlazorTeacher.Api.Data;
using BlazorTeacher.Api.Extensions;
using BlazorTeacher.Shared.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorTeacher.Api.Controllers;

/// <summary>
/// REST API Controller for Lesson CRUD operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IValidator<CreateLessonDto> _createValidator;
    private readonly IValidator<UpdateLessonDto> _updateValidator;
    private readonly ILogger<LessonsController> _logger;

    public LessonsController(
        AppDbContext context,
        IValidator<CreateLessonDto> createValidator,
        IValidator<UpdateLessonDto> updateValidator,
        ILogger<LessonsController> logger)
    {
        _context = context;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    /// <summary>
    /// GET api/lessons
    /// Retrieves all lessons with optional filtering by course.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons([FromQuery] int? courseId = null)
    {
        _logger.LogInformation("Getting lessons, CourseId filter: {CourseId}", courseId);

        var query = _context.Lessons.AsQueryable();

        if (courseId.HasValue)
        {
            query = query.Where(l => l.CourseId == courseId.Value);
        }

        var lessons = await query
            .OrderBy(l => l.CourseId)
            .ThenBy(l => l.Order)
            .ToListAsync();

        return Ok(lessons.Select(l => l.ToDto()));
    }

    /// <summary>
    /// GET api/lessons/{id}
    /// Retrieves a single lesson by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDto>> GetLesson(int id)
    {
        _logger.LogInformation("Getting lesson with ID: {LessonId}", id);

        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson is null)
        {
            return NotFound($"Lesson with ID {id} not found.");
        }

        return Ok(lesson.ToDto());
    }

    /// <summary>
    /// POST api/lessons
    /// Creates a new lesson.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LessonDto>> CreateLesson(CreateLessonDto dto)
    {
        _logger.LogInformation("Creating new lesson: {Title} for course {CourseId}", dto.Title, dto.CourseId);

        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        // Verify course exists
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == dto.CourseId);
        if (!courseExists)
        {
            return BadRequest($"Course with ID {dto.CourseId} not found.");
        }

        var lesson = dto.ToEntity();
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created lesson with ID: {LessonId}", lesson.Id);

        return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson.ToDto());
    }

    /// <summary>
    /// PUT api/lessons/{id}
    /// Updates an existing lesson.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<LessonDto>> UpdateLesson(int id, UpdateLessonDto dto)
    {
        _logger.LogInformation("Updating lesson: {LessonId}", id);

        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson is null)
        {
            return NotFound($"Lesson with ID {id} not found.");
        }

        lesson.ApplyUpdate(dto);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated lesson: {LessonId}", id);

        return Ok(lesson.ToDto());
    }

    /// <summary>
    /// DELETE api/lessons/{id}
    /// Deletes a lesson.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        _logger.LogInformation("Deleting lesson: {LessonId}", id);

        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson is null)
        {
            return NotFound($"Lesson with ID {id} not found.");
        }

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted lesson: {LessonId}", id);

        return NoContent();
    }
}
