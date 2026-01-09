using FluentValidation;

namespace MinhCoach.App.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
 {
     public LoginQueryValidator()
     {
         RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required.")
             .EmailAddress().WithMessage("Invalid email format.");

         RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Password is required.")
             .Must(password => 
                 password.Length >= 8 &&
                 password.Any(char.IsUpper) &&
                 password.Any(char.IsLower) &&
                 password.Any(char.IsDigit) &&
                 password.Any(ch => !char.IsLetterOrDigit(ch))
             ).WithMessage("Password must have at least 8 characters, including letters, numbers, and special characters.");
     }
 }
