using FluentValidation;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;

namespace KiaKooshar.Application.Validation.User
{
    public class ChangeStatusUserValidation : AbstractValidator<ChangeStatusUserCommand>
    {
        public ChangeStatusUserValidation ()
        {
            RuleFor (x => x.Id)
                .GreaterThan (0)
                .WithMessage ("Id must be greater than 0.");

            RuleFor (x => x.Status)
                .IsInEnum ()
                .WithMessage ("Status is invalid.");
        }
    }
}
