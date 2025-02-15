using MinhCoach.App.Common.Interfaces.Services;

namespace MinhCoach.Infra.Services;

public class DateTimeProvider : IDateTimeProvider
{
    DateTime UtcNow { get; }
    
    public (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRange(DateTime? date)
    {
        date ??= UtcNow;
        var dayOfWeek = (int)date.Value.DayOfWeek;
        var startOfWeek = date.Value.Date.AddDays(dayOfWeek == 0 ? -6 : - (dayOfWeek - 1));
        var endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
        
        return (startOfWeek, endOfWeek);
    }
}