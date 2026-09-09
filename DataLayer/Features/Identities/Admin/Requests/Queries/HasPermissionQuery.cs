using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Queries
{
    public class HasPermissionQuery :
        IRequest<ResultDTO>
    {
        public long UserId { get; set; }
        public string Permission { get; set; } = null!;
    }
}
