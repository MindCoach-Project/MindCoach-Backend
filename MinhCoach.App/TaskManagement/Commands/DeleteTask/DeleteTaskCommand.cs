using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Common;

namespace MinhCoach.App.TaskManagement.Commands.DeleteTask;

public record DeleteTaskCommand(
    Guid TaskId
    ) : IRequest<ErrorOr<ObjectResponse<CUDTaskResult>>>;