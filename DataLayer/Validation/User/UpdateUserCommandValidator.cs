using FluentValidation;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;

namespace KiaKooshar.Application.Validation.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator ()
        {
            RuleFor (x => x.UpdateUserDTO.FirstName)
      .NotEmpty ().WithMessage ("First name is required.")
      .MaximumLength (50).WithMessage ("First name cannot exceed 50 characters.");

            RuleFor (x => x.UpdateUserDTO.LastName)
                .NotEmpty ().WithMessage ("Last name is required.")
                .MaximumLength (50).WithMessage ("Last name cannot exceed 50 characters.");

            RuleFor (x => x.UpdateUserDTO.BirthDate)
                .NotEmpty ().WithMessage ("Birth date is required.")
                .Must (BeValidDate)
                .WithMessage ("Birth date is not in a valid format.");

            RuleFor (x => x.UpdateUserDTO.Gender)
                .NotEmpty ().WithMessage ("Gender is required.")
                .Must (g => g == "Male" || g == "Female")
                .WithMessage ("Gender must be either 'Male' or 'Female'.");

            RuleFor (x => x.UpdateUserDTO.Email)
                .EmailAddress ()
                .When (x => !string.IsNullOrWhiteSpace (x.UpdateUserDTO.Email))
                .WithMessage ("Email is not valid.");

            RuleFor (x => x.UpdateUserDTO.PhoneNumber)
                .Matches (@"^\+?[0-9]{10,15}$")
                .When (x => !string.IsNullOrWhiteSpace (x.UpdateUserDTO.PhoneNumber))
                .WithMessage ("Phone number is not valid.");
        }

        private bool BeValidDate ( string birthDate )
        {
            return DateTime.TryParse (birthDate, out _);
        }

    }
}

