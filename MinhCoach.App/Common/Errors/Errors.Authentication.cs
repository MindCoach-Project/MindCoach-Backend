using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace MinhCoach.App.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "Invalid email or password. Please try again.",
            metadata: new Dictionary<string, object>
            {
                { "IsValidationError", false }
            }
        );
        public static Error UserIdFromTokenNotFound => Error.Unauthorized(
            code: "Auth.UserIdFromTokenNotFound",
            description: "User Id is missing from token."
        );
    }
}