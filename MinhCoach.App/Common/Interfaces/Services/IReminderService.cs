using MinhCoach.Domain.User.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.Common.Interfaces.Services;

public interface IReminderService
{
    Task SendReminderAsync(UserId userId, TaskEntity message, DateTime notifyNow);
}