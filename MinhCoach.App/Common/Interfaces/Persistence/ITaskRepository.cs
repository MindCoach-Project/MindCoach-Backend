using Task = MinhCoach.Domain.Task;

namespace MinhCoach.App.Common.Persistence;

public interface ITaskRepository
{
    void Add(Task.Task task);
}