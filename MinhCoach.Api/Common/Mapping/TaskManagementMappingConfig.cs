using Mapster;
using MapsterMapper;
using MinhCoach.App.TaskManagement.Commands.CreateTask;
using MinhCoach.App.TaskManagement.Commands.DeleteTask;
using MinhCoach.App.TaskManagement.Commands.UpdateTask;
using MinhCoach.App.TaskManagement.Queries.GetTaskById;
using MinhCoach.App.TaskManagement.Queries.GetTasksByDate;
using MinhCoach.App.TaskManagement.Queries.GetTasksByWeek;
using MinhCoach.Contracts.TaskManagement;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Api.Common.Mapping;

public class TaskManagementMappingConfig : IRegister
{   
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTaskRequest, CreateTaskCommand>();
        
        config.NewConfig<(UpdateTaskRequest req, Guid taskId), UpdateTaskCommand>()
            .Map(d => d.TaskId, s => s.taskId)
            .Map(d => d, s => s.req);

        config.NewConfig<Guid, DeleteTaskCommand>()
            .Map(d => d.TaskId, s => s);
        
        config.NewConfig<Guid, GetTaskByIdQuery>()
            .Map(d => d.TaskId, s => s);
        
        config.NewConfig<(DateTime date, string status), GetTasksByDateQuery>()
            .Map(d => d.Date, s => s.date)
            .Map(d => d.Status, s => s.status);
        
        config.NewConfig<DateTime, GetTasksByWeekQuery>()
            .Map(d => d.Date, s => s);
        
        config.NewConfig<Task, CUDTaskResponse>()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.UserId, s => s.UserId.Value)
            .Map(d => d.TemplateId, s => s.TemplateId.Value)
            .Map(d => d.Title, s => s.TaskDetail.Title)
            .Map(d => d.Description, s => s.TaskDetail.Description)
            .Map(d => d.Status, s => s.TaskDetail.Status)
            .Map(d => d.StartTime, s => s.TaskDetail.StartTime)
            .Map(d => d.EndTime, s => s.TaskDetail.EndTime)
            .Map(d => d.CreatedAt, s => s.Timestamps.CreatedAt)
            .Map(d => d.UpdatedAt, s => s.Timestamps.UpdatedAt)
            .Map(d => d.DeletedAt, s => s.Timestamps.DeletedAt);

        config.NewConfig<Task, GetTaskByIdResponse>()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.Title, s => s.TaskDetail.Title)
            .Map(d => d.Description, s => s.TaskDetail.Description)
            .Map(d => d.Status, s => s.TaskDetail.Status)
            .Map(d => d.StartTime, s => s.TaskDetail.StartTime)
            .Map(d => d.EndTime, s => s.TaskDetail.EndTime)
            .Map(d => d.CreatedAt, s => s.Timestamps.CreatedAt)
            .Map(d => d.UpdatedAt, s => s.Timestamps.UpdatedAt)
            .Map(d => d.DeletedAt, s => s.Timestamps.DeletedAt);
        
        config.NewConfig<Task, TaskResponse>()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.Title, s => s.TaskDetail.Title)
            .Map(d => d.Description, s => s.TaskDetail.Description)
            .Map(d => d.Status, s => s.TaskDetail.Status)
            .Map(d => d.StartTime, s => s.TaskDetail.StartTime)
            .Map(d => d.EndTime, s => s.TaskDetail.EndTime)
            .Map(d => d.CreatedAt, s => s.Timestamps.CreatedAt)
            .Map(d => d.UpdatedAt, s => s.Timestamps.UpdatedAt)
            .Map(d => d.DeletedAt, s => s.Timestamps.DeletedAt);
    }
}