using Microsoft.EntityFrameworkCore;
using MinhCoach.Domain.Models;
using MinhCoach.Domain.User;

namespace MinhCoach.Infra.Persistence;

public class MindCoachDbContext : DbContext
{
    public MindCoachDbContext(DbContextOptions<MindCoachDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(MindCoachDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}