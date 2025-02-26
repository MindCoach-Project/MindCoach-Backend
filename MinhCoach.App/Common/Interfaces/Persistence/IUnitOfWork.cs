namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    ITaskRepository TaskRepository { get; }
    IUserRepository UserRepository { get; }
    ISubTaskRepository SubTaskRepository { get; }
    ITemplateRepository TemplateRepository { get; }
    Task<int> SaveChangesAsync();
}
