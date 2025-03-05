namespace MinhCoach.Contracts.RemiderManagement;

public record RemiderMessage(
    string Title,
    DateTime StartTime,
    DateTime NotifyTime,
    List<SubtaskMessage> SubtaskMessages
    
    );
    public record SubtaskMessage(
        string Title,
        DateTime StartTime
        );