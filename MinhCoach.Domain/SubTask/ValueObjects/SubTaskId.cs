using MinhCoach.Domain.Common.Models;

namespace MinhCoach.Domain.SubTask.ValueObjects;

public class SubTaskId : ModelId<Guid>
{
    public override Guid Value { get; protected set; }
    private SubTaskId(Guid value)
    {
        Value = value;
    }
    
    public static SubTaskId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public static SubTaskId Create(string id)
    {
        return new(Guid.Parse(id));
    }
    
    public static SubTaskId Create(Guid id)
    {
        return new(id);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}