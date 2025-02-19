using MediatR;
using MinhCoach.Domain.Task.Events;

namespace MinhCoach.App.TaskManagement.Commands.Events;

public class TaskCreatedEventHandler :  INotificationHandler<TaskCreated>
{
    public Task Handle(
        TaskCreated notification, 
        CancellationToken cancellationToken)
    {
       var subTask = notification.task.SubTasks;
       subTask.ForEach(s => s.UpdateTaskId(notification.task.Id));
       
       return Task.CompletedTask;
    }
}