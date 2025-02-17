namespace MinhCoach.App.Common.Interfaces.Services;

public interface IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    
    (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRange(DateTime? date);
}