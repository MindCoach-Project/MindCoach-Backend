using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Task.ValueObjects;

namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface ISubTaskRepository
{
    Task<List<SubTask>> GetByTaskIdAsync(TaskId taskId);

    Task AddRangeAsync(List<SubTask> subTasks);
}