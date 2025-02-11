using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Common;

public record CUDTaskResult(
    Task Task
    );