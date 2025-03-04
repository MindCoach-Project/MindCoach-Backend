using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using MinhCoach.Infra.Enums;

namespace MinhCoach.Infra.Persistence.Configurations;

public class TemplateConfigurations : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        ConfigureTemplatesTable(builder);
    }

    public void ConfigureTemplatesTable(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Templates");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TemplateId.Create(value));

        builder.Property(m => m.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(m => m.Description)
            .IsRequired(false);

        builder.Property(m => m.IsPrivateTemplate)
            .HasDefaultValue(false)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
             .IsRequired(false);
        
        builder.Property(x => x.Type)
            .HasMaxLength(100)
            .IsRequired(false);
        
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