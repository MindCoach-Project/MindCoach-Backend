namespace MinhCoach.Contracts.TaskManagement;

public record TaskResponse
(
    Guid Id,
    string Title,
    string? Description,
    string Priority,
    string Status,
    string Type,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? DeletedAt
);