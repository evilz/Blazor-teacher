using BlazorTeacher.Shared.DTOs;
using BlazorTeacher.Shared.Models;
using System.Net.Http.Json;

namespace BlazorTeacher.Dashboard.Services;

/// <summary>
/// Service for interacting with the Courses API.
/// Demonstrates HTTP client usage in Blazor.
/// </summary>
public class CourseService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CourseService> _logger;

    public CourseService(HttpClient httpClient, ILogger<CourseService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<CourseDto>> GetCoursesAsync(CourseLevel? level = null, bool? isPublished = null, string? search = null)
    {
        try
        {
            var queryParams = new List<string>();
            
            if (level.HasValue)
                queryParams.Add($"level={level.Value}");
            
            if (isPublished.HasValue)
                queryParams.Add($"isPublished={isPublished.Value}");
            
            if (!string.IsNullOrWhiteSpace(search))
                queryParams.Add($"search={Uri.EscapeDataString(search)}");

            var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            
            var courses = await _httpClient.GetFromJsonAsync<List<CourseDto>>($"api/courses{query}");
            return courses ?? new List<CourseDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching courses");
            return new List<CourseDto>();
        }
    }

    public async Task<CourseDto?> GetCourseAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CourseDto>($"api/courses/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching course {CourseId}", id);
            return null;
        }
    }

    public async Task<CourseDto?> CreateCourseAsync(CreateCourseDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/courses", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CourseDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating course");
            return null;
        }
    }

    public async Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto dto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/courses/{id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CourseDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating course {CourseId}", id);
            return null;
        }
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/courses/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting course {CourseId}", id);
            return false;
        }
    }

    public async Task<List<LessonDto>> GetCourseLessonsAsync(int courseId)
    {
        try
        {
            var lessons = await _httpClient.GetFromJsonAsync<List<LessonDto>>($"api/courses/{courseId}/lessons");
            return lessons ?? new List<LessonDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching lessons for course {CourseId}", courseId);
            return new List<LessonDto>();
        }
    }
}
