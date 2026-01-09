using ErrorOr;

namespace MinhCoach.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound  => Error.NotFound(
            code: "User.NotFound",
            description: "User task was not found."
        );
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "This email is already in use."
        );
    }
}