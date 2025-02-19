using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Task.ValueObjects;

namespace MinhCoach.Domain.Task.Events;

public record TaskCreated(
    Task task) : IDomainEvent;