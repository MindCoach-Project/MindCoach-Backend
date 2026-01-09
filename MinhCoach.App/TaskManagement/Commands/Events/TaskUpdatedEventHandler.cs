using MediatR;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Task.Events;

namespace MinhCoach.App.TaskManagement.Commands.Events;

public class TaskUpdatedEventHandler :  INotificationHandler<TaskUpdated>
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskUpdatedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(
        TaskUpdated notification, 
        CancellationToken cancellationToken)
    {
        if (notification.SubTaskUpdatedEventDatas == null || !notification.SubTaskUpdatedEventDatas.Any())
        {
            return;
        }
        
        var existingSubTasks = await _unitOfWork.SubTaskRepository.GetByTaskIdAsync(notification.Task.Id);
        var newSubTasks = new List<SubTask>();
        
        notification.SubTaskUpdatedEventDatas.ForEach(s =>
        {
            if (existingSubTasks.Any(existingSubTask => existingSubTask.Id == s.Id))
            {
                var existingTask = existingSubTasks.First(existingSubTask => existingSubTask.Id == s.Id);
                existingTask.Update(
                    s.Title,
                    s.Description,
                    s.Status,
                    s.StartTime,
                    s.EndTime);
            } else
            {
                newSubTasks.Add(SubTask.Create(s.Title, s.Description, s.StartTime, s.EndTime));
            }
        });

        if (newSubTasks.Any())
        {
            newSubTasks.ForEach(s => s.UpdateTaskId(notification.Task.Id));
            await _unitOfWork.SubTaskRepository.AddRangeAsync(newSubTasks);
        }
    }
}