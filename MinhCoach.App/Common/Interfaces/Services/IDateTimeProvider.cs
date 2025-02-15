namespace MinhCoach.App.Common.Interfaces.Services;

public interface IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    
    (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRange(DateTime? date);
}