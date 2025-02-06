using MinhCoach.App.Common.Interfaces.Services;

namespace MinhCoach.Infra.Services;

public class DateTimeProvider : IDateTimeProvider
{
    DateTime UtcNow { get; }
}