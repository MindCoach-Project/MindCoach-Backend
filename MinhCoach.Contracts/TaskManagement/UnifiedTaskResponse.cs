
namespace MinhCoach.Contracts.TaskManagement;

public record UnifiedTaskResponse(
    Guid Id,
    string Title,
    string? Description,
    string? Priority,
    string Status,
    string Type,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? DeletedAt,
    Guid? TaskId
);
