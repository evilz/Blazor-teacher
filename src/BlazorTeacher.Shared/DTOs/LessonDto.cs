namespace BlazorTeacher.Shared.DTOs;

/// <summary>
/// Data Transfer Object for Lesson - used for API responses.
/// </summary>
public record LessonDto(
    int Id,
    string Title,
    string Content,
    int Order,
    int DurationInMinutes,
    string? VideoUrl,
    int CourseId,
    DateTime CreatedAt);

/// <summary>
/// DTO for creating a new lesson.
/// </summary>
public record CreateLessonDto(
    string Title,
    string Content,
    int Order,
    int DurationInMinutes,
    string? VideoUrl,
    int CourseId);

/// <summary>
/// DTO for updating an existing lesson.
/// </summary>
public record UpdateLessonDto(
    string? Title,
    string? Content,
    int? Order,
    int? DurationInMinutes,
    string? VideoUrl);
