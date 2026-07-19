using FluentValidation;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;

namespace KiaKooshar.Application.Validation.User
{
    public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>

    {
        public RegisterUserCommandValidation ()
        {
            //RuleFor (x => x.FirstName)
            //    .NotEmpty ().WithMessage ("First name is required.")
            //    .Length (2, 50).WithMessage ("First name must be between 2 and 50 characters.");

            //RuleFor (x => x.LastName)
            //    .NotEmpty ().WithMessage ("Last name is required.")
            //    .Length (2, 50).WithMessage ("Last name must be between 2 and 50 characters.");

            //RuleFor (x => x.UserName)
            //    .NotEmpty ().WithMessage ("Username is required.")
            //    .Length (3, 30).WithMessage ("Username must be between 3 and 30 characters.")
            //    .Matches (@"^[a-zA-Z0-9_]+$")
            //    .WithMessage ("Username can only contain letters, numbers, and underscores.");

            //RuleFor (x => x.PasswordHash)
            //    .NotEmpty ().WithMessage ("Password is required.")
            //    .MinimumLength (8).WithMessage ("Password must be at least 8 characters long.")
            //    .Matches (@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            //    .WithMessage ("Password must contain at least one uppercase letter, one lowercase letter, and one number.");

            ////RuleFor (x => x.RegisterUserDTO.BirthDate)
            ////    .NotEmpty ().WithMessage ("Birth date is required.")
            ////    .Must (BeValidDate)
            ////    .WithMessage ("Birth date must be a valid date.");

            //RuleFor (x => x.Gender)
            //    .NotEmpty ().WithMessage ("Gender is required.")
            //    .Must (g => g == "Male" || g == "Female")
            //    .WithMessage ("Gender must be either 'Male' or 'Female'.");

            //RuleFor (x => x.NationalCode)
            //    .NotEmpty ().WithMessage ("National code is required.")
            //    .Length (10).WithMessage ("National code must be exactly 10 digits.")
            //    .Matches (@"^\d{10}$")
            //    .WithMessage ("National code must contain only digits.");

            //RuleFor (x => x.Email)
            //    .EmailAddress ()
            //    .When (x => !string.IsNullOrWhiteSpace (x.RegisterUserDTO.Email))
            //    .WithMessage ("Email address is invalid.");

            //RuleFor (x => x.PhoneNumber)
            //    .Matches (@"^\+?[0-9]{10,15}$")
            //    .When (x => !string.IsNullOrWhiteSpace (x.RegisterUserDTO.PhoneNumber))
            //    .WithMessage ("Phone number is invalid.");

            //RuleFor (x => x.Status)
            //    .IsInEnum ()
            //    .WithMessage ("Invalid user status.");
        }
        private bool BeValidDate ( string birthDate )
        {
            return DateTime.TryParse (birthDate, out _);
        }
    }
}
