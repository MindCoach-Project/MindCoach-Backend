using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Persistence;
using MinhCoach.Domain.Task.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly MindCoachDbContext _dbContext;
    
    public TaskRepository(MindCoachDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TaskEntity?> FindByIdAsync(TaskId taskId)
    {
        return await _dbContext.Tasks.SingleOrDefaultAsync(
            t => t.Id == taskId && 
                 t.Timestamps.DeletedAt == null);
    }
    
    public async Task AddAsync(TaskEntity task)
    {
        await  _dbContext.Tasks.AddAsync(task);
        await  _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskEntity task)
    {
         _dbContext.Tasks.Update(task);
        await  _dbContext.SaveChangesAsync();
    }
    
}