using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;

namespace MinhCoach.Domain.Common.ValueObjects;

public class TaskDetail : ValueObject
{
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatuses Status { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
        yield return Status;
        yield return StartTime;
        yield return EndTime;
    }
    
}