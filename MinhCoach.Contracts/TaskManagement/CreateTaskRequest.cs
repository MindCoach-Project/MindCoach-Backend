namespace MinhCoach.Contracts.TaskManagement;

public record CreateTaskRequest
(
    string Title,
    string? Description,
    string? Priority,
    DateTime StartTime,
    DateTime EndTime
);