namespace MinhCoach.Contracts.TaskManagement;
public record DailyTaskTrackingResponse(
    string Date,
     int ToDo,
     int InProgress,
     int Done
    );