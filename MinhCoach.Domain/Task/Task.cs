using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using MinhCoach.Domain.SubTask;

namespace MinhCoach.Domain.Task;

public sealed class Task : Model<TaskId, Guid>
{
    public Priorities Priority { get; private set; }
    public TaskDetail TaskDetail { get; private set; }
    public TaskTypes Type { get; private set; }
    public FullTimestamps Timestamps { get; private set; }
    public UserId UserId { get; private set; }
    public TemplateId TemplateId { get; private set; }
    
#pragma warning disable CS8618
    private Task()
    {

    }
#pragma warning disable CS8618
}