namespace MinhCoach.Contracts.RemiderManagement;

public record RemiderMessage(
    string Title,
    DateTime StartTime,
    List<SubtaskMessage> SubtaskMessages
    
    );
    public record SubtaskMessage(
        string Title,
        DateTime StartTime
        );