using BlazorTeacher.Shared.DTOs;
using FluentValidation;

namespace BlazorTeacher.Api.Validators;

/// <summary>
/// FluentValidation validator for CreateLessonDto.
/// </summary>
public class CreateLessonValidator : AbstractValidator<CreateLessonDto>
{
    public CreateLessonValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Content)
            .MaximumLength(10000).WithMessage("Content cannot exceed 10000 characters.");

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.");

        RuleFor(x => x.VideoUrl)
            .MaximumLength(500).WithMessage("Video URL cannot exceed 500 characters.")
            .Must(url => url == null || Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Video URL must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.VideoUrl));

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Course ID is required.");
    }
}

/// <summary>
/// FluentValidation validator for UpdateLessonDto.
/// </summary>
public class UpdateLessonValidator : AbstractValidator<UpdateLessonDto>
{
    public UpdateLessonValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.")
            .When(x => x.Title != null);

        RuleFor(x => x.Content)
            .MaximumLength(10000).WithMessage("Content cannot exceed 10000 characters.")
            .When(x => x.Content != null);

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater.")
            .When(x => x.Order.HasValue);

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.")
            .When(x => x.DurationInMinutes.HasValue);

        RuleFor(x => x.VideoUrl)
            .MaximumLength(500).WithMessage("Video URL cannot exceed 500 characters.")
            .Must(url => url == null || Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Video URL must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.VideoUrl));
    }
}
