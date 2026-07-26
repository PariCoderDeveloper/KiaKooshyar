using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Requests.Queries
{
    public class GetUserByIdQuery : IRequest<ResultDTO<GetUserByIdDTO>>
    {
        public long Id { get; set; }
    }
}
