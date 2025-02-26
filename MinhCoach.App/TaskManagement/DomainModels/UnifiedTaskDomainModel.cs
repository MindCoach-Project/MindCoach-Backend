using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.DomainModels;

public record UnifiedTaskDomainModel(
    Guid Id,
    TaskDetail TaskDetail,
    Priorities? Priority,
    TaskTypes Type,
    FullTimestamps Timestamps,
    UserId? UserId,
    TemplateId? TemplateId,
    TaskId? TaskId
)
{
    public static UnifiedTaskDomainModel FromTask(Task task) =>
        new UnifiedTaskDomainModel(
            task.Id.Value,
            task.TaskDetail,
            task.Priority,
            task.Type,
            task.Timestamps,
            task.UserId,
            task.TemplateId,
            null
        );
    public static UnifiedTaskDomainModel FromSubTask(SubTask subTask) =>
        new UnifiedTaskDomainModel(
            subTask.Id.Value,
            subTask.TaskDetail,
            null, 
            subTask.Type,
            subTask.Timestamps,
            null, 
            null,
            subTask.TaskId
        );
}