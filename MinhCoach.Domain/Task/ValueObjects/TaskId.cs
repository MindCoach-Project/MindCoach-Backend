using MinhCoach.Domain.Common.Models;

namespace MinhCoach.Domain.Task.ValueObjects;

public class TaskId : ModelId<Guid>
{
    public override Guid Value { get; protected set; }
    private TaskId(Guid value)
    {
        Value = value;
    }
    
    public static TaskId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public static TaskId Create(string id)
    {
        return new(Guid.Parse(id));
    }
    
    public static TaskId Create(Guid id)
    {
        return new(id);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}