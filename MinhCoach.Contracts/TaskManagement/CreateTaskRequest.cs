namespace MinhCoach.Contracts.TaskManagement;

public record CreateTaskRequest
(
    string Title,
    string? Description,
    string? Priority,
    DateTime StartTime,
    DateTime EndTime,
    List<SubTask>? SubTasks = null
    );

public record SubTask
(
    string Title,
    string? Description,
    DateTime StartTime,
    DateTime EndTime
);