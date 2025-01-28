// using Mapster;
// using MinhCoach.App.Authentication.Commands.Register;
// using MinhCoach.App.Authentication.Common;
// using MinhCoach.App.Authentication.Queries.Login;
// using MinhCoach.Contracts.Authentication;
//
// namespace MinhCoach.Api.Common.Mapping;
//
// public class AuthenticationMappingConfig : IRegister
// {
//     public void Register(TypeAdapterConfig config)
//     {
//         config.NewConfig<RegisterRequest, RegisterCommand>();
//         config.NewConfig<LoginRequest, LoginQuery>();
//         config.NewConfig<AuthenticationResult, AuthenticationResponse>()
//             .Map(d => d.Token, s => s.Token)
//             .Map(d => d.Id, s => s.User.Id.Value)
//             .Map(d => d, s => s.User);
//     }
// }