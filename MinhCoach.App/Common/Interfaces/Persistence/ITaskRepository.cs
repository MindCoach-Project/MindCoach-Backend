using MinhCoach.Domain.Task.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.Common.Persistence;

public interface ITaskRepository
{
    Task<TaskEntity?> FindByIdAsync(TaskId taskId);
    Task  AddAsync(TaskEntity task);
    Task  UpdateAsync(TaskEntity task);
}