using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Persistence;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
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
        return await _dbContext.Tasks
            .Include(t => t.SubTasks
                .Where(st => st.Timestamps.DeletedAt == null)
                .OrderBy(st => st.TaskDetail.StartTime))
            .SingleOrDefaultAsync(
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
             return await query.Where(t => 
                    t.TaskDetail.Status == status.Value ).ToListAsync();
        
        var result = await query.ToListAsync();
        
        
        return result
            .Select(t =>
            {
                var obj =  new
                {
                    Task = t,
                    OrderedSubTasks = _dbContext.SubTasks
                        .Where(
                        st => st.TaskId == t.Id && 
                              st.Timestamps.DeletedAt == null)
                        .OrderBy(st => st.TaskDetail.StartTime).ToList(),
                };
                return obj;
            })
            .OrderBy(t =>
                    t.OrderedSubTasks.Any()
                        ? t.OrderedSubTasks.First().TaskDetail.StartTime 
                        : t.Task.TaskDetail.StartTime 
            )
            .Select(t =>
            {
                t.Task.UpdateListSubtasks(t.OrderedSubTasks);
                return t.Task;
            })
            .ToList();
    }
    
    public async Task<List<TaskEntity>> GetTasksByWeekAsync(DateTime startOfWeek, DateTime endOfWeek, UserId userId)
    {
        var query = await _dbContext.Tasks
            .Include(t => t.SubTasks
                .Where(t => t.Timestamps.DeletedAt == null))
            .Where(t => t.UserId == userId &&
                        t.Timestamps.DeletedAt == null &&
                        t.TaskDetail.StartTime >= startOfWeek &&
                        t.TaskDetail.StartTime <= endOfWeek).ToListAsync();

        return query.SelectMany(t => t.SubTasks.Any()
            ? t.SubTasks.Select(s => TaskEntity.ConvertSubTaskToTask(s, t.Priority))
            : new List<TaskEntity>() {t})
            .OrderBy(t => t.TaskDetail.StartTime)
            .ToList();
    }
    
    public async Task<List<TaskEntity>> GetUpcomingTasksTodayAsync(DateTime now, UserId userId)
    {
        var query = await _dbContext.Tasks
            .Include(t => t.SubTasks
                .Where(t => t.Timestamps.DeletedAt == null &&
                            t.TaskDetail.StartTime >= now))
            .Where(t => t.UserId == userId &&
                        t.Timestamps.DeletedAt == null &&
                        t.TaskDetail.StartTime >= now).ToListAsync();
        
        return query.SelectMany(t => t.SubTasks.Any()
                ? t.SubTasks.Select(s => TaskEntity.ConvertSubTaskToTask(s, t.Priority))
                : new List<TaskEntity>() {t})
            .OrderBy(t => t.TaskDetail.StartTime)
            .Take(3)
            .ToList();
    }
    
    
} 