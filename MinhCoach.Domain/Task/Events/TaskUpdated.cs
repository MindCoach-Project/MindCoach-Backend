using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;

namespace MinhCoach.Domain.Task.Events;

public record TaskUpdated(
    Task Task,
    List<SubTaskUpdatedEventData>? SubTaskUpdatedEventDatas) : IDomainEvent;
public record SubTaskUpdatedEventData(
    SubTaskId Id, 
    string Title, 
    string? Description, 
    string? Status,
    DateTime StartTime, 
    DateTime EndTime
);