using BlazorTeacher.Shared.DTOs;
using FluentValidation;

namespace BlazorTeacher.Api.Validators;

/// <summary>
/// FluentValidation validator for CreateCourseDto.
/// Demonstrates input validation best practices.
/// </summary>
public class CreateCourseValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

        RuleFor(x => x.Instructor)
            .NotEmpty().WithMessage("Instructor name is required.")
            .MaximumLength(100).WithMessage("Instructor name cannot exceed 100 characters.");

        RuleFor(x => x.DurationInHours)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 hours.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

        RuleFor(x => x.Level)
            .IsInEnum().WithMessage("Invalid course level.");
    }
}

/// <summary>
/// FluentValidation validator for UpdateCourseDto.
/// Validates only non-null fields.
/// </summary>
public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.")
            .When(x => x.Title != null);

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.")
            .When(x => x.Description != null);

        RuleFor(x => x.Instructor)
            .MaximumLength(100).WithMessage("Instructor name cannot exceed 100 characters.")
            .When(x => x.Instructor != null);

        RuleFor(x => x.DurationInHours)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 hours.")
            .When(x => x.DurationInHours.HasValue);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.")
            .When(x => x.Price.HasValue);

        RuleFor(x => x.Level)
            .IsInEnum().WithMessage("Invalid course level.")
            .When(x => x.Level.HasValue);
    }
}
