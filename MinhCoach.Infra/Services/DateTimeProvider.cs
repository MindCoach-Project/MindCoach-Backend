using MinhCoach.App.Common.Interfaces.Services;

namespace MinhCoach.Infra.Services;

public class DateTimeProvider : IDateTimeProvider
{
    DateTime Now { get; }
    
    public (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRange(DateTime? date)
    {
        date ??= Now;
        var dayOfWeek = (int)date.Value.DayOfWeek;
        var startOfWeek = date.Value.Date.AddDays(dayOfWeek == 0 ? -6 : - (dayOfWeek - 1));
        var endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
        
        return (startOfWeek, endOfWeek);
    }

    public DateTime GetNearestWeekday(DateTime date)
    {
        
        var (startOfWeek, endOfWeek) = GetWeekRange(DateTime.Now);

        int daysOffset = ((int)date.DayOfWeek - (int)startOfWeek.DayOfWeek + 7) % 7;
        
        DateTime nearestDate = startOfWeek.AddDays(daysOffset);
        
        return nearestDate.Date
            .AddHours(date.Hour)
            .AddMinutes(date.Minute)
            .AddSeconds(date.Second)
            .AddMilliseconds(date.Millisecond);
    }
}