namespace MinhCoach.App.Common.Interfaces.Authentication;

public interface ITokenService
{
    Guid GetUserIdFromToken();
}