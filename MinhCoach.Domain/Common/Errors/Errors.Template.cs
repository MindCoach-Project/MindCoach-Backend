using ErrorOr;

namespace MinhCoach.Domain.Common.Errors;

public static partial class Errors
{
    public static class Template
    {
        public static Error NotFound  => Error.NotFound(
            code: "Template.NotFound",
            description: "The template was not found."
        );
    }
}