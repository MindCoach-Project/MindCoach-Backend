using Mapster;
using MinhCoach.App.Authentication.Commands.Register;
using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Authentication.Queries.Login;
using MinhCoach.App.Common.Response;
using MinhCoach.Contracts.Authentication;

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