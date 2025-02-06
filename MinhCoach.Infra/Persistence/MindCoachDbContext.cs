using Microsoft.EntityFrameworkCore;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.User;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence;

public class MindCoachDbContext : DbContext
{
    public MindCoachDbContext(DbContextOptions<MindCoachDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<SubTask> SubTasks { get; set; } = null!;
    public DbSet<Template> Templates { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(MindCoachDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}