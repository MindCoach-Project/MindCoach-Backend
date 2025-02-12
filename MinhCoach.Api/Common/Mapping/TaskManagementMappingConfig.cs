using Mapster;
using MinhCoach.App.TaskManagement.Commands.CreateTask;
using MinhCoach.App.TaskManagement.Commands.UpdateTask;
using MinhCoach.App.TaskManagement.Common;
using MinhCoach.Contracts.TaskManagement;

namespace MinhCoach.Api.Common.Mapping;

public class TaskManagementMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTaskRequest, CreateTaskCommand>();
        
        config.NewConfig<(UpdateTaskRequest req, Guid taskId), UpdateTaskCommand>()
            .Map(d => d.TaskId, s => s.taskId)
            .Map(d => d, s => s.req);
        
        config.NewConfig<CUDTaskResult, CUDTaskResponse>()
            .Map(d => d.Id, s => s.Task.Id.Value)
            .Map(d => d.UserId, s => s.Task.UserId.Value)
            .Map(d => d.TemplateId, s => s.Task.TemplateId.Value)
            .Map(d => d.Title, s => s.Task.TaskDetail.Title)
            .Map(d => d.Description, s => s.Task.TaskDetail.Description)
            .Map(d => d.Status, s => s.Task.TaskDetail.Status)
            .Map(d => d.StartTime, s => s.Task.TaskDetail.StartTime)
            .Map(d => d.EndTime, s => s.Task.TaskDetail.EndTime)
            .Map(d => d.CreatedAt, s => s.Task.Timestamps.CreatedAt)
            .Map(d => d.UpdatedAt, s => s.Task.Timestamps.UpdatedAt)
            .Map(d => d.DeletedAt, s => s.Task.Timestamps.DeletedAt)
            .Map(d => d, s => s.Task);
    }
}