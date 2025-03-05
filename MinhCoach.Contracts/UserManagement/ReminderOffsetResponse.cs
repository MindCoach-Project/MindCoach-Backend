namespace MinhCoach.Contracts.UserManagement;

public record ReminderOffsetResponse
(
    Guid Id,
    string Username,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? DeletedAt,
    int? ReminderOffset
);