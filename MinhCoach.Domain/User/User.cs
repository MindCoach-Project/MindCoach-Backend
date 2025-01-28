using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.Models.ValueObjects;

namespace MinhCoach.Domain.User;

public sealed class User : Model<UserId, Guid>
{
    public string Username { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string ImageUrl { get; private set; }
    
    public FullTimestamps Timestamps { get; private set; }
   
#pragma warning disable CS8618
    private User()
    {

    }
#pragma warning disable CS8618
}