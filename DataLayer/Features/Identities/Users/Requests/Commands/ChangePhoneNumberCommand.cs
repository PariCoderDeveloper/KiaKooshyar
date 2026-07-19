using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Commands
{
    public class ChangePhoneNumberCommand : IRequest<ResultDTO>
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}

