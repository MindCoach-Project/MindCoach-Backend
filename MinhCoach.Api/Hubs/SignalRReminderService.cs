using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.Contracts.RemiderManagement;
using MinhCoach.Domain.User.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;
namespace MinhCoach.Api.Hubs;

public class SignalRReminderService : IReminderService
{
    
    private readonly IHubContext<ReminderHub> _hubContext;
    private readonly IMapper _mapper;

    public SignalRReminderService(
        IHubContext<ReminderHub> hubContext,
        IMapper mapper)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public async Task SendReminderAsync(UserId userId, TaskEntity message, DateTime notifyTime)
    {
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Task '{message.TaskDetail.Title}' {message.TaskDetail.StartTime} is starting soon!");
        Console.WriteLine($"Sending reminder to user {userId.Value} at {notifyTime}");
        Console.WriteLine($"-------------------------------------------------------");
        await _hubContext.Clients.Group(userId.Value.ToString())
            .SendAsync("ReceiveReminder", _mapper.Map<RemiderMessage>(message));
    }

}