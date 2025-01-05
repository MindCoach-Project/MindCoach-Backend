using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Models.ValueObjects;

namespace MinhCoach.Domain.User;

public sealed class User : Model<UserId, Guid>
{
    public string FirstName { get;set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public User(
        UserId id, 
        string email, 
        string password, 
        string lastName, 
        string firstName)
        : base(id)
    {
        Email = email;
        Password = password;
        LastName = lastName;
        FirstName = firstName;
    }
    
    public static User Create(
        string email,
        string password,
        string lastName,
        string firstName)
    {
        return new(
            UserId.CreateUnique(),
            email,
            password,
            lastName,
            firstName
        );
    }
#pragma warning disable CS8618
    private User()
    {

    }
#pragma warning disable CS8618
}