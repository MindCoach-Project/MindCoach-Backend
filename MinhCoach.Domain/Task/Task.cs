using ErrorOr;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.Utilities;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.Task.Events;
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
    
    public List<SubTask.SubTask>? SubTasks { get; private set; }
    
    public Task(
        TaskId id,
        TaskDetail taskDetail,
        Priorities priority,
        TaskTypes type,
        FullTimestamps timestamps,
        UserId userId,
        TemplateId templateId,
        List<SubTask.SubTask>? subTasks
        ) : base(id)
    {
        TaskDetail = taskDetail;
        Priority = priority;
        Type = type;
        Timestamps = timestamps;
        UserId = userId;
        TemplateId = templateId;
        SubTasks = subTasks;
    }

    public static Task Create(
        string title,
        string? description,
        string? priority,
        DateTime startTime,
        DateTime endTime,
        Guid userId,
        List<SubTask.SubTask>? subTasks
        )
    {
        //Parse string to enum or receive default value
        Priorities priorityEnum = EnumUtilities.ParseEnum<Priorities>(priority) ?? Priorities.Medium;
        
        var timestamps = new FullTimestamps(DateTime.UtcNow);
        
        var task =  new Task(
            TaskId.CreateUnique(),
            TaskDetail.Create(title, description, startTime, endTime),
            priorityEnum,
            TaskTypes.Task,
            timestamps,
            UserId.Create(userId),
            null,
            subTasks
        );
        
        task.AddDomainEvent(new TaskCreated(task));

        return task;
    }
    
    public void Update(
        string title,
        string? description,
        string? priority,
        string? status,
        DateTime startTime,
        DateTime endTime
        )
    {
        Timestamps = Timestamps.UpdateTimestamp();
        
        TaskDetail = TaskDetail.Update(
            title,
            description,
            status,
            startTime,
            endTime);
        
        if (!string.IsNullOrEmpty(priority) && 
            Enum.TryParse(priority, true, out Priorities parsedPriority))
            Priority = parsedPriority;
    }
    
    public void SoftDelete()
    {
        Timestamps = Timestamps.MarkAsDeleted();
    }
    
#pragma warning disable CS8618
    private Task()
    {

    }
#pragma warning disable CS8618
}