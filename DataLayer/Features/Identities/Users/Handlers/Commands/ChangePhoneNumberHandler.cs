using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.Users.Quueries;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Specifications.Users;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Users.Handlers.Commands
{
    public class ChangePhoneNumberHandler :
        IRequestHandler<ChangePhoneNumberCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public ChangePhoneNumberHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            ChangePhoneNumberCommand request,
            CancellationToken cancellationToken
            )
        {
            var specification = new UserByIdSpecification (request.Id);

            var user = await _unit.User.FirstOrDefaultAsync (specification);
            if ( user != null )
                return ResultDTO<GetUserByIdDTO>.NotFound ("User not found");

            user.PhoneNumber = request.PhoneNumber;
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync ();
            return ResultDTO.Success (
                "The phone number of user changed successfully"
              );
        }
    }
}
