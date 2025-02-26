using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;

namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface ISubTaskRepository
{
    Task<SubTask?> ValidateSubtaskMatchWithTask(TaskId taskId, SubTaskId subTaskId);

    Task<List<SubTask>> GetByTaskIdAsync(TaskId taskId);
    
    Task  UpdateAsync(SubTask subTask);

    Task AddRangeAsync(List<SubTask> subTasks);
}