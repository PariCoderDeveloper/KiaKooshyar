using AutoMapper;
using KiaKooshar.Application.Common.Models;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Queries;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Queries.GetAllUsers
{
    public class GetAllUserHandler :
        IRequestHandler<GetAllUserCommand,
            ResultDTO<PagedResult<GetAllUsersDTO>>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetAllUserHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO<PagedResult<GetAllUsersDTO>>> Handle (
            GetAllUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var users = _unit.Users.GetAllAsync
                (cancellationToken);

            var filteredUser = await users.ToPagedResultAsync (
                 request.PaginationRequest,
                 cancellationToken
                 );

            var mappedUsers = _mapper.Map<PagedResult<GetAllUsersDTO>>
                (filteredUser);

            return ResultDTO<PagedResult<GetAllUsersDTO>>.Success (
                 mappedUsers,
                 ""
                );
        }
    }
}
