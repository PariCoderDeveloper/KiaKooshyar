using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class UserRoleRepository :
        GenericRepository<UserRole>,
        IUserRoleRepository
    {
        private readonly IDatabaseContext _context;
        public UserRoleRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }
        public async Task AddRangeAsync (
            List<UserRole> userRoles,
            CancellationToken cancellationToken = default
            )
        {
            await _context.UserRoles
                .AddRangeAsync (
                    userRoles,
                    cancellationToken
                );
        }
    }
}
