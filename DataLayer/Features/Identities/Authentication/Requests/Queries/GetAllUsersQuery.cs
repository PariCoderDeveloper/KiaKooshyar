using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Requests.Queries
{
    public class GetAllUsersQuery :
        IRequest<ResultDTO<List<GetAllUsersDTO>>>
    {
    }
}
