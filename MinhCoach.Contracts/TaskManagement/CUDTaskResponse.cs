namespace MinhCoach.Contracts.TaskManagement;

public record CUDTaskResponse(
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
    DateTime? DeletedAt,
    Guid UserId,
    Guid? TemplateId,
    List<SubTaskResponse> SubTasks
    );
    
    public record SubTaskResponse(
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