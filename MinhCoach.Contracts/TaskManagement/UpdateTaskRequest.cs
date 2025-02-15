namespace MinhCoach.Contracts.TaskManagement;

public record UpdateTaskRequest
(
    string Title,
    string? Description,
    string? Priority,
    string? Status,
    DateTime StartTime,
    DateTime EndTime
);