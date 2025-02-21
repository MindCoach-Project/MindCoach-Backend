using ErrorOr;

namespace MinhCoach.Domain.Common.Errors;

public static partial class Errors
{
    public static class SubTask
    {
        public static Error NotFound  => Error.NotFound(
            code: "SubTask.NotFound",
            description: "The subTask was not found."
        );
        
        public static readonly Error UnauthorizedAccess = Error.Unauthorized(
            code: "Task.UnauthorizedAccess",
            description: "You do not have permission to update this subTask."
        );
        
        public static Error Mismatch => Error.Validation(
            code: "SubTask.Mismatch",
            description: "The subTask does not belong to the specified task."
        );
    }
}