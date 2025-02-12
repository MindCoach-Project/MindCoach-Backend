using ErrorOr;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.Domain.Task;

public sealed class Task : Model<TaskId, Guid>
{
    public Priorities Priority { get; private set; }
    public TaskDetail TaskDetail { get; private set; }
    public TaskTypes Type { get; private set; }
    public FullTimestamps Timestamps { get; private set; }
    public UserId UserId { get; private set; }
    public TemplateId TemplateId { get; private set; }
    
    public Task(
        TaskId id,
        TaskDetail taskDetail,
        Priorities priority,
        TaskTypes type,
        FullTimestamps timestamps,
        UserId userId,
        TemplateId templateId) : base(id)
    {
        TaskDetail = taskDetail;
        Priority = priority;
        Type = type;
        Timestamps = timestamps;
        UserId = userId;
        TemplateId = templateId;
    }

    public static Task Create(
        string title,
        string? description,
        string? priority,
        DateTime startTime,
        DateTime endTime,
        Guid userId
        )
    {
        //Parse string to enum or receive default value
        Priorities priorityEnum = Enum.TryParse(priority, true, out Priorities parsedPriority)
            ? parsedPriority
            : Priorities.Medium;
        
        var timestamps = new FullTimestamps(DateTime.UtcNow);

        return new Task(
            TaskId.CreateUnique(),
            TaskDetail.Create(title, description, startTime, endTime),
            priorityEnum,
            TaskTypes.Task,
            timestamps,
            UserId.Create(userId),
            null
        );
    }

    public void Update(
        string title,
        string? description,
        string? priority,
        DateTime startTime,
        DateTime endTime
        )
    {
        Timestamps.UpdateTimestamp();
        
        TaskDetail = TaskDetail.Update(
            title,
            description,
            startTime,
            endTime);
        
        if (!string.IsNullOrEmpty(priority) && Enum.TryParse(priority, true, out Priorities parsedPriority))
            Priority = parsedPriority;
    }
    
#pragma warning disable CS8618
    private Task()
    {

    }
#pragma warning disable CS8618
}