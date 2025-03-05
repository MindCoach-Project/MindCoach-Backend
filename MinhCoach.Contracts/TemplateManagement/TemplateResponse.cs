using MinhCoach.Contracts.TaskManagement;

namespace MinhCoach.Contracts.TemplateManagement;

public record TemplateResponse
(
    Guid Id,
    string Name,
    string? Description,
    string IsPrivateTemplate,
    string? UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? DeletedAt,
    string? Type,
    List<UnifiedTaskResponse>? UnifiedTasks
);
