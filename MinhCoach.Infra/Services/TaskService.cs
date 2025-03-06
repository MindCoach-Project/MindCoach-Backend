using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Interfaces.Services;
using ErrorOr;
using MinhCoach.App.TaskManagement.Common;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.User.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;
namespace MinhCoach.Infra.Services;

public class TaskService  : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private ITaskService _taskServiceImplementation;
    public TaskService(
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<List<Task>> GenerateAndSaveTasks(
        Template template, 
        UserId userId)
    {
        var taskInTemplate = await _unitOfWork.TaskRepository
            .GetTaskByTemplateId(template.Id, 0);

        var tasks =  taskInTemplate.Select( t =>
        {
            var taskDetail = TaskDetail.Create(
                t.TaskDetail.Title,
                t.TaskDetail.Description,
                _dateTimeProvider.GetNearestWeekday(t.TaskDetail.StartTime),
                _dateTimeProvider.GetNearestWeekday(t.TaskDetail.EndTime));
            taskDetail.UpdateStatus(t.TaskDetail.Status);

            var subTasks =  t.SubTasks.Select(s =>
            {
                var subTask = SubTask.Create(
                    s.TaskDetail.Title,
                    s.TaskDetail.Description,
                    _dateTimeProvider.GetNearestWeekday(s.TaskDetail.StartTime),
                    _dateTimeProvider.GetNearestWeekday(s.TaskDetail.EndTime)
                );
                subTask.TaskDetail.UpdateStatus(s.TaskDetail.Status);
                return subTask;
            }).ToList();
            
            return Task.GenerateTaskFromTemplate(
                t.Priority,
                taskDetail,
                t.Type,
                userId,
                subTasks);
        }).ToList();

        await _unitOfWork.TaskRepository.AddRangeAsync(tasks);
        await _unitOfWork.SaveChangesAsync();    
        
        return tasks;
    }

    public async Task<List<GetDailyTaskTrackingResult>> GetTasksTrackingByWeekAsync(UserId userId)
    {
        var (startOfWeek, endOfWeek) = _dateTimeProvider.GetWeekRange(_dateTimeProvider.UtcNow);
        
        var tasks = await _unitOfWork.TaskRepository.GetTasksByWeekAsync(startOfWeek, endOfWeek, userId);

        var dailyTasks = tasks
            .GroupBy(t => t.TaskDetail.StartTime.Date)
            .OrderBy(g => g.Key)
            .Select(g => new GetDailyTaskTrackingResult(
                CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(g.Key.DayOfWeek),
                g.Count(t => t.TaskDetail.Status == TaskStatuses.Todo),
                g.Count(t => t.TaskDetail.Status == TaskStatuses.Inprogress),
                g.Count(t => t.TaskDetail.Status == TaskStatuses.Done)
            ))
            .ToList();
        
        return dailyTasks;
    }
}