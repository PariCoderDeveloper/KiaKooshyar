using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Job;

namespace KiaKooshar.Application.Features.Jobs
{
    public class RefreshTokenCleanupJob :
        IRecurringJob
    {
        private readonly IUnitOfWork _unit;
        public string JobId => "cleanup-refresh-tokens";
        public string CronExpression => "0 0 * * *";
        public RefreshTokenCleanupJob (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }

        public async Task ExecuteAsync (
            CancellationToken cancellationToken = default
            )
        {
            var now = DateTime.UtcNow;
            var expiredOrRevokedTokens = await _unit.RefreshToken
                .GetExpiredOrRevokedAsync (now, cancellationToken);
            _unit.RefreshToken.RemoveRange (expiredOrRevokedTokens);
            await _unit.CommitAsync (cancellationToken);
        }
    }
}