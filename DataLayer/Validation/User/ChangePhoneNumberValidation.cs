using FluentValidation;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;

namespace KiaKooshar.Application.Validation.User
{
    public class ChangePhoneNumberValidation :
        AbstractValidator<ChangePhoneNumberCommand>
    {
        public ChangePhoneNumberValidation ()
        {
            RuleFor (x => x.Id)
                .GreaterThan (0)
                .WithMessage ("Id must be greater than 0.");

            RuleFor (x => x.PhoneNumber)
                .NotEmpty ()
                .WithMessage ("Phone number is required.")
                .Matches (@"^\+?[0-9]{10,15}$")
                .WithMessage ("Phone number is invalid.");
        }
    }
}
