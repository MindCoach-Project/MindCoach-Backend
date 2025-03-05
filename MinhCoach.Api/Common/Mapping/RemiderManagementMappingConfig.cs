using Mapster;
using MinhCoach.App.TemplateManagement.Common;
using MinhCoach.Contracts.RemiderManagement;
using MinhCoach.Contracts.TemplateManagement;
using Task = MinhCoach.Domain.Task.Task;
namespace MinhCoach.Api.Common.Mapping;

public class RemiderManagementMappingConfig : IRegister
{   
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Task task, DateTime notifyTime), RemiderMessage>()
            .Map(d => d.NotifyTime, s => s.notifyTime)
           .Map(d => d.Title, s => s.task.TaskDetail.Title)
           .Map(d => d.StartTime, s => s.task.TaskDetail.StartTime)
           .Map(d => d.SubtaskMessages, s => s.task.SubTasks != null
                ? s.task.SubTasks.Select(sub => new SubtaskMessage(
                    sub.TaskDetail.Title,
                    sub.TaskDetail.StartTime)).ToList()
                : new List<SubtaskMessage>()
            );
    }
}