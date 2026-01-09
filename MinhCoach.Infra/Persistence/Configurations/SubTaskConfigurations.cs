using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Common.Utilities;
using MinhCoach.Infra.Enums;
using Task = MinhCoach.Domain.Task;

namespace MinhCoach.Infra.Persistence.Configurations;

public class SubTaskConfigurations : IEntityTypeConfiguration<SubTask>
{
    public void Configure(EntityTypeBuilder<SubTask> builder)
    {
        ConfigureTasksTable(builder);
    }

    public void ConfigureTasksTable(EntityTypeBuilder<SubTask> builder)
    {
        builder.ToTable("SubTasks");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SubTaskId.Create(value));

        builder.Property(m => m.Type)
            .HasColumnType($"ENUM({EnumUtilities.GetEnumAsString<TaskTypes>()})")
            .HasDefaultValue(TaskTypes.SubTask)
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

        builder.Property(x => x.TaskId)
            .HasConversion(
                id => id.Value,
                value => TaskId.Create(value))
            .IsRequired();;
        
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
