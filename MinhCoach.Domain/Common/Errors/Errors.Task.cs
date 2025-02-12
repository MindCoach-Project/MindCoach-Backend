using ErrorOr;

namespace MinhCoach.Domain.Common.Errors;

public static partial class Errors
{
    public static class Task
    {
        public static Error NotFound  => Error.NotFound(
            code: "Task.NotFound",
            description: "The task was not found."
        );
        
        public static readonly Error UnauthorizedAccess = Error.Unauthorized(
            code: "Task.UnauthorizedAccess",
            description: "You do not have permission to update this task."
        );
    }
}