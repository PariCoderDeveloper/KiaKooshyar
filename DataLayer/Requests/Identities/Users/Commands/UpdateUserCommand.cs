using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.User;
using MediatR;

namespace KiaKooshar.Application.Requests.Identities.User.Commands
{
    public class UpdateUserCommand : IRequest<ResultDTO<UpdateUserDTO>>
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string Gender { get; set; } = null!;

    }
}
