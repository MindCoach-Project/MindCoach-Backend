using MinhCoach.App.TaskManagement.DomainModels;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface ITaskRepository
{
    Task<TaskEntity?> FindByIdAsync(TaskId taskId);
    
    Task<List<TaskEntity>> GetTaskByTemplateId(TemplateId templateId, int numberOfTasks);

    Task  AddAsync(TaskEntity task);
    Task  AddRangeAsync(List<TaskEntity> tasks);
    Task  UpdateAsync(TaskEntity task);
    Task<List<TaskEntity>> GetTasksByDateAsync(DateTime date, TaskStatuses? status, UserId userId);

    Task<List<TaskEntity>> GetTasksByWeekAsync(DateTime startOfWeek, DateTime endOfWeek, UserId userId);

    Task<List<TaskEntity>> GetUpcomingTasksTodayAsync(DateTime now, UserId userId);

    Task<List<TaskEntity>> GetTasksWithUpcomingRemindersAsync(DateTime now);
}