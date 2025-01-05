namespace MinhCoach.Domain.Common.Models;

public abstract class ModelId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}