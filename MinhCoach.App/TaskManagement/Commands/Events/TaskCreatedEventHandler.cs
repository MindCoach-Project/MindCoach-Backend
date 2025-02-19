using MediatR;
using MinhCoach.Domain.Task.Events;

namespace MinhCoach.App.TaskManagement.Commands.Events;

public class TaskCreatedEventHandler :  INotificationHandler<TaskCreated>
{
    public Task Handle(
        TaskCreated notification, 
        CancellationToken cancellationToken)
    {
       var subTask = notification.Task.SubTasks;
       subTask.ForEach(s => s.UpdateTaskId(notification.Task.Id));
       
       return Task.CompletedTask;
    }
}