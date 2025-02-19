using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Persistence;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using MinhCoach.Infra.Services;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly MindCoachDbContext _dbContext;
    
    public TaskRepository(
        MindCoachDbContext dbContext
        )
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
        await _dbContext.Tasks.AddAsync(task);
    }

    public async Task UpdateAsync(TaskEntity task)
    {
         _dbContext.Tasks.Update(task);
    }

    public async  Task<List<TaskEntity>> GetTasksByDateAsync(
        DateTime date, 
        TaskStatuses? status, 
        UserId userId)
    {
        var query = _dbContext.Tasks
            .Where(t => t.UserId == userId &&
                        t.Timestamps.DeletedAt == null &&
                        t.TaskDetail.StartTime.Date == date.Date);

        if (status.HasValue)
        {
            query = query.Where(t => t.TaskDetail.Status == status.Value);
        }

        return await query.OrderBy(t => t.TaskDetail.StartTime).ToListAsync();
    }
    
    public async Task<List<TaskEntity>> GetTasksByWeekAsync(DateTime startOfWeek, DateTime endOfWeek, UserId userId)
    {
        return await _dbContext.Tasks
            .Where(t => t.UserId == userId &&
                        t.Timestamps.DeletedAt == null &&
                        t.TaskDetail.StartTime >= startOfWeek &&
                        t.TaskDetail.StartTime <= endOfWeek)
            .OrderBy(t => t.TaskDetail.StartTime)
            .ToListAsync();
    }
    
    public async Task<List<TaskEntity>> GetUpcomingTasksTodayAsync(DateTime now, UserId userId)
    {
        return await _dbContext.Tasks
            .Where(t => t.UserId == userId &&
                        t.Timestamps.DeletedAt == null &&
                        t.TaskDetail.StartTime.Date == now.Date &&
                        t.TaskDetail.StartTime >= now)
            .OrderBy(t => t.TaskDetail.StartTime) 
            .Take(3) 
            .ToListAsync();
    }

}