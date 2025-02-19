namespace MinhCoach.Contracts.TaskManagement;

public record UpdateTaskRequest
(
    string Title,
    string? Description,
    string? Priority,
    string? Status,
    DateTime StartTime,
    DateTime EndTime,
    List<SubTaskUpdate>? SubTasks
);

public record SubTaskUpdate
(
    Guid Id, 
    string Title,
    string? Description,
    string? Status,
    DateTime StartTime,
    DateTime EndTime
);