using MediatR;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.Domain.Task.Events;

namespace MinhCoach.App.TaskManagement.Commands.Events;

public class TaskDeletedEventHandler :  INotificationHandler<TaskDeleted>
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskDeletedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        TaskDeleted notification, 
        CancellationToken cancellationToken)
    {
       var task = notification.Task;    
       var subtasks = await _unitOfWork.SubTaskRepository
           .GetByTaskIdAsync(task.Id);

       subtasks.ForEach(s => s.SoftDelete());
    }
}