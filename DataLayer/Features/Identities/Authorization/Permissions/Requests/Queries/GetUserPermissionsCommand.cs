using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authorization.Permissions.Requests.Queries
{
    public class GetUserPermissionsCommand : IRequest<ResultDTO>
    {
        public long userId { get; set; }
    }
}
