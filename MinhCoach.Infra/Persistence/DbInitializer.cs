using Microsoft.EntityFrameworkCore;

namespace MinhCoach.Infra.Persistence;

public class DbInitializer : IDbInitializer
{
    private readonly MindCoachDbContext _dbContext;
    
    public DbInitializer(MindCoachDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task  InitializeAsync()
    {
        try
        {
            if ((await  _dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration Error: {ex.Message}");
        }
    }

}