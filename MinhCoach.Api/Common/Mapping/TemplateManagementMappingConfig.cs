using Mapster;
using MinhCoach.App.TemplateManagement.Common;
using MinhCoach.Contracts.TemplateManagement;

namespace MinhCoach.Api.Common.Mapping;

public class TemplateManagementMappingConfig : IRegister
{   
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetTemplatesResult, TemplateResponse>()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.UserId, s => s.UserId.Value)
            .Map(d => d.CreatedAt, s => s.Timestamps.CreatedAt)
            .Map(d => d.UpdatedAt, s => s.Timestamps.UpdatedAt)
            .Map(d => d.DeletedAt, s => s.Timestamps.DeletedAt);
    }
}