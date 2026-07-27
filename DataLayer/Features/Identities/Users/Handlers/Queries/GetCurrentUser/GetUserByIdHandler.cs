using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Identities.Users.Requests.Queries;
using KiaKooshar.Application.Specifications.Identities.Users;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Queries.GetCurrentUser
{
    public class GetUserByIdHandler :
        IRequestHandler<GetUserByIdQuery, ResultDTO<GetUserByIdDTO>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetUserByIdHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<ResultDTO<GetUserByIdDTO>> Handle (
            GetUserByIdQuery request,
            CancellationToken cancellationToken
            )
        {
            var specification = new UserByIdSpecification (request.Id);
            var user = await _unit.User.FirstOrDefaultAsync (
                specification,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO<GetUserByIdDTO>.NotFound ("User not found");
            var result = _mapper.Map<GetUserByIdDTO> (user);
            return ResultDTO<GetUserByIdDTO>.Success (result, "User found");
        }
    }
}
