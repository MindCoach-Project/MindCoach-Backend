using FluentValidation;

namespace MinhCoach.App.TaskManagement.Commands.UpdateTask;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Date is required.");
        
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
            .NotEmpty().WithMessage("Subtask start time is required.");
        
        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("Subtask end time is required.")
            .GreaterThan(x => x.StartTime).WithMessage("Subtask end time must be greater than start time.");
    }
}