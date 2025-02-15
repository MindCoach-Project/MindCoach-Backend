using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.Common.Persistence;

public interface ITaskRepository
{
    Task<TaskEntity?> FindByIdAsync(TaskId taskId);
    Task  AddAsync(TaskEntity task);
    Task  UpdateAsync(TaskEntity task);
    Task<List<TaskEntity>> GetTasksByDateAsync(DateTime date, TaskStatuses? status, UserId userId);

    Task<List<TaskEntity>> GetTasksByWeekAsync(DateTime startOfWeek, DateTime endOfWeek, UserId userId);

    Task<List<TaskEntity>> GetUpcomingTasksTodayAsync(DateTime now, UserId userId);
}