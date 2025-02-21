using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.Domain.SubTask;

public sealed class SubTask : Model<SubTaskId, Guid>
{
    public TaskDetail TaskDetail { get; private set; }
    public TaskTypes Type { get; private set; }
    public FullTimestamps Timestamps { get; private set; }
    public TaskId TaskId { get; private set; }
    
    public SubTask(
        SubTaskId id,
        TaskDetail taskDetail,
        TaskTypes type,
        FullTimestamps timestamps,
        TaskId taskId) : base(id)
    {
        TaskDetail = taskDetail;
        Type = type;
        Timestamps = timestamps;
        TaskId = taskId;
    }
    
    public static SubTask Create(
        string title,
        string? description,
        DateTime startTime,
        DateTime endTime
    )
    {
        
        var timestamps = new FullTimestamps(DateTime.UtcNow);
        
        var subTask =  new SubTask(
            SubTaskId.CreateUnique(),
            TaskDetail.Create(title, description, startTime, endTime),
            TaskTypes.SubTask,
            timestamps,
            null
        );
        
        return subTask;
    }

    public void UpdateTaskId(TaskId taskId)
    {
        TaskId = taskId;
    }
    
    public void Update(
        string title,
        string? description,
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
    }
    
    public void SoftDelete()
    {
        Timestamps = Timestamps.MarkAsDeleted();
    }
    
    
#pragma warning disable CS8618
    private SubTask()
    {

    }
#pragma warning disable CS8618
}