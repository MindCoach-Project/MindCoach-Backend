using MinhCoach.App.TaskManagement.DomainModels;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.TemplateManagement.Common;

public record GetTemplatesResult(
    TemplateId Id,
    string Name,
    string? Description,
    bool IsPrivateTemplate,
    FullTimestamps Timestamps,
    UserId? UserId,
    List<UnifiedTaskDomainModel>? UnifiedTasks
    );