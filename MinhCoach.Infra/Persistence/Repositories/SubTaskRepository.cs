using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using MinhCoach.Infra.Services;
using TaskEntity = MinhCoach.Domain.Task.Task;

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

    public async Task<List<SubTask>> GetByTaskIdAsync(TaskId taskId)
    {
        return await _dbContext.SubTasks.Where(
            s => s.TaskId == taskId &&
                 s.Timestamps.DeletedAt == null).ToListAsync();
    }

    public async Task AddRangeAsync(List<SubTask> subTasks)
    {
        await _dbContext.SubTasks.AddRangeAsync(subTasks);
    }

}