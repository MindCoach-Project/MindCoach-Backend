using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhCoach.Domain.User;
using MinhCoach.Domain.User.ValueObjects;
using MinhCoach.Infra.Enums;

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
            
        builder.Property(m => m.Username)
            .HasMaxLength(100)
            .IsRequired(false);  

        builder.Property(m => m.Phone)
            .HasMaxLength(100)
            .IsRequired(false);  
        
        builder.Property(m => m.Address)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(m => m.Password)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasIndex(m => m.Email)
            .IsUnique();

        builder.Property(m => m.Email)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(m => m.ImageUrl)
            .HasMaxLength(100)
            .HasDefaultValue("/images/users/default-user.png")
            .IsRequired();
        
        builder.Property(m => m.reminderOffset)
            .HasDefaultValue(5)
            .IsRequired();
        
        builder.OwnsOne(m => m.Timestamps, timestamps =>
        {
            timestamps.Property(t => t.CreatedAt)
                .HasColumnName(TimeColumnNames.CreatedAt.ToString())
                .IsRequired();
            
            timestamps.Property(t => t.UpdatedAt)
                .HasColumnName(TimeColumnNames.UpdatedAt.ToString()) 
                .IsRequired(false);

            timestamps.Property(t => t.DeletedAt)
                .HasColumnName(TimeColumnNames.DeletedAt.ToString()) 
                .IsRequired(false);
        });
    }
}