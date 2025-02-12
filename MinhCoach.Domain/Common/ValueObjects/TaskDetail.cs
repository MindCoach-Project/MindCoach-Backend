using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Models;

namespace MinhCoach.Domain.Common.ValueObjects;

public class TaskDetail : ValueObject
{
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatuses Status { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public TaskDetail(
        string title, 
        string description, 
        TaskStatuses status, 
        DateTime? startTime, 
        DateTime? endTime)
    {
        Title = title;
        Description = description;
        Status = status;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public static TaskDetail Create(
        string title, 
        string? description, 
        DateTime startTime, 
        DateTime endTime)
    {
        return new TaskDetail(
            title,
            description ?? string.Empty,  
            TaskStatuses.Todo,     
            startTime,
            endTime
        );
    }

    public TaskDetail Update(
        string title,
        string? description,
        DateTime startTime,
        DateTime endTime)
    {
        return new TaskDetail(
            title,
            description ?? string.Empty,
            TaskStatuses.Todo,
            startTime,
            endTime);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
        yield return Status;
        yield return StartTime;
        yield return EndTime;
    }
    
}