using MediatR;
using ErrorOr;
using Task = MinhCoach.Domain.Task.Task;
using MinhCoach.App.Common.Response;

namespace MinhCoach.App.TemplateManagement.Commands.GenerateTasksFromTemplate;

public record GenerateTasksFromTemplateCommand
(Guid TemplateId
    ) : IRequest<ErrorOr<ObjectResponse<List<Task>>>>;