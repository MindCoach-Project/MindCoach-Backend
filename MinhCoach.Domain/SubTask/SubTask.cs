using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;

namespace MinhCoach.Domain.SubTask;

public sealed class SubTask : Model<SubTaskId, Guid>
{
    public TaskDetail TaskDetail { get; private set; }
    public TaskTypes Type { get; private set; }
    public FullTimestamps Timestamps { get; private set; }
    public TaskId TaskId { get; private set; }
    
#pragma warning disable CS8618
    private SubTask()
    {

    }
#pragma warning disable CS8618
}