using MinhCoach.Domain.Common.Models;

namespace MinhCoach.Domain.Common.ValueObjects;

public class BaseTimestamps : ValueObject
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected BaseTimestamps(DateTime createdAt)
    {
        CreatedAt = createdAt;
        UpdatedAt = null;
    }

    public void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return CreatedAt;
        yield return UpdatedAt;
    }
}