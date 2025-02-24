using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.SubTask;

namespace MinhCoach.App.TaskManagement.Commands.DeleteSubTask;

public record DeleteSubTaskCommand(
    Guid TaskId,
    Guid SubTaskId
    ) : IRequest<ErrorOr<ObjectResponse<SubTask>>>;