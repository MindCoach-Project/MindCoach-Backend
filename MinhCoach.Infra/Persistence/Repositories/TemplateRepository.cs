using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Task.ValueObjects;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using TaskEntity = MinhCoach.Domain.Task.Task;

namespace MinhCoach.Infra.Persistence.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly MindCoachDbContext _dbContext;
    
    public TemplateRepository(
        MindCoachDbContext dbContext
        )
    {
        _dbContext = dbContext;
    }

    public async Task<List<Template>> GetTemplates(UserId? userId)
    {
        var query = _dbContext.Templates
            .Where(t => t.Timestamps.DeletedAt == null);

        if (userId is not null)
            query = query
                .Where(t => !t.IsPrivateTemplate || t.UserId == userId)
                .OrderByDescending(t => t.UserId);
        else
            query = query.Where(t => !t.IsPrivateTemplate);

        return await query.ToListAsync();
    }

    public async Task<Template?> FindByIdAsync(TemplateId templateId)
    {
        return await _dbContext.Templates
            .SingleOrDefaultAsync(
                t => t.Id == templateId && 
                     t.Timestamps.DeletedAt == null);
    }
} 