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
        
        RuleForEach(x => x.SubTasks).SetValidator(new SubTaskCommandValidator());
    }
}

public class SubTaskCommandValidator : AbstractValidator<SubTaskCommand>
{
    public SubTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Subtask title is required.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Subtask start time is required.")
            .Must(date => date >= DateTime.Now).WithMessage("Subtask date cannot be in the past.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("Subtask end time is required.")
            .GreaterThan(x => x.StartTime).WithMessage("Subtask end time must be greater than start time.");
    }
}