using FluentValidation;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;

namespace KiaKooshar.Application.Validation.User
{
    public class ChangePasswordValidation :
        AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidation ()
        {
            RuleFor (x => x.Id)
                 .GreaterThan (0)
                 .WithMessage ("Id must be greater than 0.");

            RuleFor (x => x.Password)
                .NotEmpty ()
                .WithMessage ("Password is required.")
                .MinimumLength (8)
                .WithMessage ("Password must be at least 8 characters long.")
                .Matches (@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
                .WithMessage ("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
        }
    }
}
