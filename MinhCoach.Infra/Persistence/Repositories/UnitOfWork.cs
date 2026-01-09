using MinhCoach.App.Common.Interfaces.Persistence;

namespace MinhCoach.Infra.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MindCoachDbContext _context;

    public ITaskRepository TaskRepository { get; }
    public IUserRepository UserRepository { get; }
    public ISubTaskRepository SubTaskRepository { get; }
    public ITemplateRepository TemplateRepository { get; }

    public UnitOfWork(
        MindCoachDbContext context, 
        IUserRepository userRepository, 
        ITaskRepository taskRepository, 
        ISubTaskRepository subTaskRepository,
        ITemplateRepository templateRepository)
    {
        _context = context;
        UserRepository = userRepository;
        TaskRepository = taskRepository;
        SubTaskRepository = subTaskRepository;
        TemplateRepository = templateRepository;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
         _context.Dispose();
    }
}