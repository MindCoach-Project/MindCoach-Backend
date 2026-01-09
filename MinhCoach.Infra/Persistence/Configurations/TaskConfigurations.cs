using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using MinhCoach.Domain.Common.Utilities;
using MinhCoach.Infra.Enums;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence.Configurations;

public class TaskConfigurations : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        ConfigureTasksTable(builder);
    }

    public void ConfigureTasksTable(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TaskId.Create(value));

        builder.Property(m => m.Type)
            .HasColumnType($"ENUM({EnumUtilities.GetEnumAsString<TaskTypes>()})")
            .HasDefaultValue(TaskTypes.Task)
            .IsRequired();
        
        builder.Property(m => m.Priority)
            .HasColumnType($"ENUM({EnumUtilities.GetEnumAsString<Priorities>()})")
            .HasDefaultValue(Priorities.Medium)
            .IsRequired();
         
        builder.OwnsOne(m => m.TaskDetail, taskDetails =>
        {
            taskDetails.Property(p => p.Title)
                .HasColumnName("Title")
                .HasMaxLength(100)
                .IsRequired();
            
            taskDetails.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired(false);
            
            taskDetails.Property(p => p.Status)
                .HasColumnName("Status")
                .HasColumnType($"ENUM({EnumUtilities.GetEnumAsString<TaskStatuses>()})")
                .HasDefaultValue(TaskStatuses.Todo)
                .IsRequired();

            taskDetails.Property(p => p.StartTime)
                .HasColumnName("StartTime")
                .IsRequired();
            
            taskDetails.Property(p => p.EndTime)
                .HasColumnName("EndTime")
                .IsRequired();
        });
        
        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .IsRequired((false));
        
        builder.Property(x => x.TemplateId)
            .HasConversion(
                id => id.Value,
                value => TemplateId.Create(value))
            .IsRequired((false));
        
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
        
        builder.Property(t => t.IsReminderSent)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
