using BlazorTeacher.Shared.Models;

namespace BlazorTeacher.Shared.DTOs;

/// <summary>
/// Data Transfer Object for Course - used for API responses.
/// Separates the API contract from the domain model.
/// </summary>
public record CourseDto(
    int Id,
    string Title,
    string Description,
    string Instructor,
    int DurationInHours,
    decimal Price,
    CourseLevel Level,
    bool IsPublished,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int LessonCount);

/// <summary>
/// DTO for creating a new course.
/// Contains validation-ready properties.
/// </summary>
public record CreateCourseDto(
    string Title,
    string Description,
    string Instructor,
    int DurationInHours,
    decimal Price,
    CourseLevel Level);

/// <summary>
/// DTO for updating an existing course.
/// All fields are optional to support partial updates.
/// </summary>
public record UpdateCourseDto(
    string? Title,
    string? Description,
    string? Instructor,
    int? DurationInHours,
    decimal? Price,
    CourseLevel? Level,
    bool? IsPublished);
