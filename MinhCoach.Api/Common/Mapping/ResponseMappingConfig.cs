using Mapster;
using MinhCoach.App.Common.Response;

namespace MinhCoach.Api.Common.Mapping;

public class ResponseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig(typeof(ObjectResponse<>), typeof(ApiResponse<>))
            .Map("Message", "Message")
            .Map("Data", "Data");
    }
}