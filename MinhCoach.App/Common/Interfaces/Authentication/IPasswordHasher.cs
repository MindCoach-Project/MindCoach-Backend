namespace MinhCoach.App.Common.Interfaces.Authentication;

public interface IPasswordHasher
{
     string HashPassword(string password);
     bool VerifyPassword(string password, string hashedPassword);

}