using MinhCoach.App.Common.Persistence;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly MindCoachDbContext _dbContext;
    
    public TaskRepository(MindCoachDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(Task task)
    {
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();
    }
}