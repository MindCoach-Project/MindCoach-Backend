using MinhCoach.Domain.Common.Models;

namespace MinhCoach.Domain.Template.ValueObjects;

public class TemplateId : ModelId<Guid>
{
    public override Guid Value { get; protected set; }
    private TemplateId(Guid value)
    {
        Value = value;
    }
    
    public static TemplateId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public static TemplateId Create(string id)
    {
        return new(Guid.Parse(id));
    }
    
    public static TemplateId Create(Guid id)
    {
        return new(id);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}