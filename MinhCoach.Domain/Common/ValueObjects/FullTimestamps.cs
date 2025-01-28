namespace MinhCoach.Domain.Common.ValueObjects;

public class FullTimestamps : BaseTimestamps
{
    public DateTime? DeletedAt { get; private set; }

    public FullTimestamps(DateTime createdAt) : base(createdAt)
    {
        DeletedAt = null;
    }

    public void MarkAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}