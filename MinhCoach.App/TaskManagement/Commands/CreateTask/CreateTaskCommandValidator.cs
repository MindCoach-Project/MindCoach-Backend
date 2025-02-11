using FluentValidation;

namespace MinhCoach.App.TaskManagement.Commands.CreateTask;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Date is required.")
            .Must(date => date >= DateTime.Now).WithMessage("Task date cannot be in the past.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("Duration is required.")
            .GreaterThan(x => x.StartTime).WithMessage("End time must be greater than start time.");
    }
}