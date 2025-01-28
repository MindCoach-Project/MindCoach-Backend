namespace MinhCoach.Domain.Common.ValueObjects;

public abstract class BaseTimestamps
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
}