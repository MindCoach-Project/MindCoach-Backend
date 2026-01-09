using Mapster;
using MinhCoach.App.Authentication.Commands.UpdateReminderOffset;
using MinhCoach.Contracts.UserManagement;
using MinhCoach.Domain.User;

namespace MinhCoach.Api.Common.Mapping;

public class UserManagementMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateReminderOffsetRequest, UpdateReminderOffsetCommand>();
        
        config.NewConfig<User, ReminderOffsetResponse>()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.CreatedAt, s => s.Timestamps.CreatedAt)
            .Map(d => d.UpdatedAt, s => s.Timestamps.UpdatedAt)
            .Map(d => d.DeletedAt, s => s.Timestamps.DeletedAt)
            .Map(d => d.ReminderOffset, s => s.reminderOffset);
    }
}