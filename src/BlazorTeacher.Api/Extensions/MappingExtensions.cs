using BlazorTeacher.Shared.DTOs;
using BlazorTeacher.Shared.Models;

namespace BlazorTeacher.Api.Extensions;

/// <summary>
/// Extension methods for mapping between domain models and DTOs.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Maps a Course entity to CourseDto.
    /// </summary>
    public static CourseDto ToDto(this Course course)
    {
        return new CourseDto(
            course.Id,
            course.Title,
            course.Description,
            course.Instructor,
            course.DurationInHours,
            course.Price,
            course.Level,
            course.IsPublished,
            course.CreatedAt,
            course.UpdatedAt,
            course.Lessons?.Count ?? 0
        );
    }

    /// <summary>
    /// Maps CreateCourseDto to a new Course entity.
    /// </summary>
    public static Course ToEntity(this CreateCourseDto dto)
    {
        return new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Instructor = dto.Instructor,
            DurationInHours = dto.DurationInHours,
            Price = dto.Price,
            Level = dto.Level,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Applies UpdateCourseDto changes to an existing Course entity.
    /// Only updates non-null fields (partial update pattern).
    /// </summary>
    public static void ApplyUpdate(this Course course, UpdateCourseDto dto)
    {
        if (dto.Title is not null) course.Title = dto.Title;
        if (dto.Description is not null) course.Description = dto.Description;
        if (dto.Instructor is not null) course.Instructor = dto.Instructor;
        if (dto.DurationInHours.HasValue) course.DurationInHours = dto.DurationInHours.Value;
        if (dto.Price.HasValue) course.Price = dto.Price.Value;
        if (dto.Level.HasValue) course.Level = dto.Level.Value;
        if (dto.IsPublished.HasValue) course.IsPublished = dto.IsPublished.Value;
        course.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Maps a Lesson entity to LessonDto.
    /// </summary>
    public static LessonDto ToDto(this Lesson lesson)
    {
        return new LessonDto(
            lesson.Id,
            lesson.Title,
            lesson.Content,
            lesson.Order,
            lesson.DurationInMinutes,
            lesson.VideoUrl,
            lesson.CourseId,
            lesson.CreatedAt
        );
    }

    /// <summary>
    /// Maps CreateLessonDto to a new Lesson entity.
    /// </summary>
    public static Lesson ToEntity(this CreateLessonDto dto)
    {
        return new Lesson
        {
            Title = dto.Title,
            Content = dto.Content,
            Order = dto.Order,
            DurationInMinutes = dto.DurationInMinutes,
            VideoUrl = dto.VideoUrl,
            CourseId = dto.CourseId,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Applies UpdateLessonDto changes to an existing Lesson entity.
    /// </summary>
    public static void ApplyUpdate(this Lesson lesson, UpdateLessonDto dto)
    {
        if (dto.Title is not null) lesson.Title = dto.Title;
        if (dto.Content is not null) lesson.Content = dto.Content;
        if (dto.Order.HasValue) lesson.Order = dto.Order.Value;
        if (dto.DurationInMinutes.HasValue) lesson.DurationInMinutes = dto.DurationInMinutes.Value;
        if (dto.VideoUrl is not null) lesson.VideoUrl = dto.VideoUrl;
    }
}
