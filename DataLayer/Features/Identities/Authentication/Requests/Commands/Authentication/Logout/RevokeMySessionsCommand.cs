using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Logout
{
    public class RevokeMySessionsCommand
        : IRequest<ResultDTO>
    {
    }
}
