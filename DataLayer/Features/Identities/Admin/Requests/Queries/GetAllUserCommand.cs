using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Queries
{
    public class GetAllUserCommand :
        IRequest<ResultDTO<PagedResult<GetAllUsersDTO>>>
    {
        public string? SearchKey { get; set; }
        public PaginationRequest PaginationRequest { get; set; } = null!;
    }
}
