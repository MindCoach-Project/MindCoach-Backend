namespace MinhCoach.Contracts.TaskManagement;

public record UpdateTaskRequest
(
    string Title,
    string? Description,
    string? Priority,
    DateTime StartTime,
    DateTime EndTime
);