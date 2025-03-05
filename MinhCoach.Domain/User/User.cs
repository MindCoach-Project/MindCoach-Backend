using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.Domain.User;

public sealed class  User : Model<UserId, Guid>
{
    public string Username { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string ImageUrl { get; private set; }
    public FullTimestamps Timestamps { get; private set; }

    public int? reminderOffset { get; private set; } = 5;

    public User(
        UserId id,
        string username, 
        string phone, 
        string address, 
        string email, 
        string password, 
        string imageUrl,
        FullTimestamps timestamps) : base(id)
    {
        Username = username;
        Phone = phone;
        Address = address;
        Email = email;
        Password = password;
        ImageUrl = imageUrl;
        Timestamps = timestamps;
    }
    public static User Create(
        string username, 
        string email, 
        string password)
    {
        var timestamps = new FullTimestamps(DateTime.UtcNow);

        var user = new User(
            UserId.CreateUnique(),
            username,
            null,
            null,
            email,
            password,
            null, 
            timestamps);
        
        user.UpdateReminderOffset(5);
        
        return user;
    }

    public void UpdateReminderOffset(int? reminderOffset = 5)
    {
        if (reminderOffset.HasValue) this.reminderOffset = reminderOffset;

        Timestamps = Timestamps.UpdateTimestamp();
    }
#pragma warning disable CS8618
    private User()
    {

    }
#pragma warning disable CS8618
}