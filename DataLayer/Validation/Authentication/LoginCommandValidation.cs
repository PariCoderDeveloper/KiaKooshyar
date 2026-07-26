using FluentValidation;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;

namespace KiaKooshar.Application.Validation.Authentication
{
    public class LoginCommandValidation :
        AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation ()
        {
            RuleFor (x => x.Email)
                .NotEmpty ()
                .WithMessage ("Email is required")
                .EmailAddress ()
                .WithMessage ("Email is not valid");
            RuleFor (x => x.Password)
                .NotEmpty ()
                .WithMessage ("Password is required")
                .MinimumLength (8)
                .WithMessage ("Password must be at least 6 characters long")
                .Matches ("[A-Z]")
                .WithMessage ("Password must contain at least one uppercase letter.")
                .Matches ("[a-z]")
                .WithMessage ("Password must contain at least one lowercase letter.")
                .Matches ("[0-9]")
                .WithMessage ("Password must contain at least one number.");
        }
    }
}
