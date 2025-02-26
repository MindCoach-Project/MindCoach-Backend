using MinhCoach.Domain.Template;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface ITemplateRepository
{
    Task<List<Template>> GetTemplates(UserId? userId);
}