using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.RevokeToken
{
    public class RevokeTokenHandler :
        IRequestHandler<RevokeTokenCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public RevokeTokenHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            RevokeTokenCommand request,
            CancellationToken cancellationToken
            )
        {
            var tokenResult = await _unit.RefreshToken.FindByTokenAsync (
                request.RefreshToken,
                cancellationToken
                );
            if ( string.IsNullOrWhiteSpace (request.RefreshToken) )
                return ResultDTO.Success (
                    "Refresh token revoked."
                    );
            tokenResult.Revoked = DateTime.UtcNow;
            await _unit.CommitAsync ();
            return ResultDTO.Success (
                    "Refresh token revoked."
                    );
        }
    }
}
