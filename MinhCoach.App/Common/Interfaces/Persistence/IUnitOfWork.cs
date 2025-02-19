namespace MinhCoach.App.Common.Persistence;

public interface IUnitOfWork : IDisposable
{
    ITaskRepository TaskRepository { get; }
    IUserRepository UserRepository { get; }
    ISubTaskRepository SubTaskRepository { get; }
    Task<int> SaveChangesAsync();
}
