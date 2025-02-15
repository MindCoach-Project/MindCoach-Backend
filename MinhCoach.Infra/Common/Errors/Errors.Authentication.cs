using ErrorOr;

namespace MinhCoach.Infra.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error TokenNotFound => Error.Unauthorized(
            code: "Auth.TokenNotFound",
            description: "Authorization token is missing."
        );

        public static Error InvalidToken => Error.Unauthorized(
            code: "Auth.InvalidToken",
            description: "The provided token is invalid or expired."
        );

        public static Error UserIdNotFound => Error.Unauthorized(
            code: "Auth.UserIdNotFound",
            description: "User ID is missing from the token."
        );

        public static Error InvalidUserIdFormat => Error.Unauthorized(
            code: "Auth.InvalidUserIdFormat",
            description: "User ID in the token is not a valid GUID."
        );
    }
}