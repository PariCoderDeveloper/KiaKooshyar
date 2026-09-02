using FluentValidation;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;

namespace KiaKooshar.Application.Validation.User
{
    public class DeleteUserCommandValidation :
        AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidation ()
        {
            RuleFor (x => x.UserId)
                .GreaterThan (0)
                .WithMessage ("User id must be greater than zero.");
        }
    }
}
