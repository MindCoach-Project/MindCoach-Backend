using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhCoach.Domain.Models.ValueObjects;
using MinhCoach.Domain.User;

namespace MinhCoach.Infra.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    public void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
            
        builder.Property(m => m.LastName)
            .HasMaxLength(100);

        builder.Property(m => m.FirstName)
            .HasMaxLength(100);

        builder.Property(m => m.Password)
            .HasMaxLength(100);

        builder.Property(m => m.Email)
            .HasMaxLength(100);
    }
}