namespace MinhCoach.App.TaskManagement.Common;
public record GetDailyTaskTrackingResult(
    string Date,
    int ToDo,
    int InProgress,
    int Done
);