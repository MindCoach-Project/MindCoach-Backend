namespace MinhCoach.Contracts.TaskManagement;

public record DeleteSubTaskResponse(
    Guid Id,
    string Title,
    string? Description,
    string Status,
    string Type,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? DeletedAt,
    Guid TaskId
    );