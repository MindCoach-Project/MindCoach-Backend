using MinhCoach.App.TaskManagement.Common;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.User.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.Common.Interfaces.Services;

public interface ITaskService
{
    Task<List<Task>> GenerateAndSaveTasks(Template template, UserId userId);
    Task<List<GetDailyTaskTrackingResult>> GetTasksTrackingByWeekAsync(UserId userId);
}