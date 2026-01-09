using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;

namespace MinhCoach.Infra.Persistence.Repositories;

public class SubTaskRepository : ISubTaskRepository
{
    private readonly MindCoachDbContext _dbContext;
    
    public SubTaskRepository(
        MindCoachDbContext dbContext
        )
    {
        _dbContext = dbContext;
    }

    public async Task<SubTask?> ValidateSubtaskMatchWithTask(TaskId taskId, SubTaskId subTaskId)
    {
        return await _dbContext.SubTasks
            .Where(s => s.TaskId == taskId &&
                        s.Id == subTaskId &&
                        s.Timestamps.DeletedAt == null)
            .SingleOrDefaultAsync();
    }

    public async Task<List<SubTask>> GetByTaskIdAsync(TaskId taskId)
    {
        return await _dbContext.SubTasks.Where(
            s => s.TaskId == taskId &&
                 s.Timestamps.DeletedAt == null).ToListAsync();
    }

    public async Task UpdateAsync(SubTask subTask)
    {
        _dbContext.SubTasks.Update(subTask);
    }

    public async Task AddRangeAsync(List<SubTask> subTasks)
    {
        await _dbContext.SubTasks.AddRangeAsync(subTasks);
    }

}