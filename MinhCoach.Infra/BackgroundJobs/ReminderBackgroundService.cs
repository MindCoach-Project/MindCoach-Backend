using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Interfaces.Services;

namespace MinhCoach.Infra.BackgroundJobs;

public class ReminderBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReminderBackgroundService(
        IServiceScopeFactory serviceScopeFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessRemindersAsync();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task ProcessRemindersAsync()
    {
        Console.WriteLine("Job ProcessRemindersAsync are running!...");

        using var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();

        var currentTime = _dateTimeProvider.Now;
        var tasksToNotify = await unitOfWork.TaskRepository.GetTasksWithUpcomingRemindersAsync(currentTime);
        foreach (var task in tasksToNotify)
        {
            var reminderOffset = (await unitOfWork.UserRepository.GetUserById(task.UserId))?.reminderOffset ?? 5;

            bool shouldSendReminder = false;
            
            if (task.SubTasks.Any())
            {
                foreach (var subTask in task.SubTasks)
                {
                    if (subTask.TaskDetail.StartTime <= currentTime.AddMinutes(reminderOffset) &&
                        !subTask.IsReminderSent)
                    {
                        shouldSendReminder = true;
                        subTask.MarkReminderSent();
                    }
                }

                if (task.SubTasks.All(subTask => subTask.IsReminderSent) && !task.IsReminderSent)
                {
                    shouldSendReminder = true;
                    task.MarkReminderSent();
                }
                
            }
            else if (task.TaskDetail.StartTime <= currentTime.AddMinutes(reminderOffset) && !task.IsReminderSent)
            {
                task.MarkReminderSent();
                shouldSendReminder = true;
            }
            

            if (shouldSendReminder)
            {
                await unitOfWork.TaskRepository.UpdateAsync(task);
                await unitOfWork.SaveChangesAsync();
                await reminderService.SendReminderAsync(task.UserId, task, _dateTimeProvider.Now);
            }
        }
    }
}
