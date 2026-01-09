using Microsoft.EntityFrameworkCore;
using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.User;
using MinhCoach.Infra.Persistence.Interceptors;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence;

public class MindCoachDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    public MindCoachDbContext(
        DbContextOptions<MindCoachDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor
    ) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<SubTask> SubTasks { get; set; } = null!;
    public DbSet<Template> Templates { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(MindCoachDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}