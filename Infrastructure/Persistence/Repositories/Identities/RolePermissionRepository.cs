using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class RolePermissionRepository :
        GenericRepository<RolePermission>,
        IRolePermissionRepository
    {
        public RolePermissionRepository (
            DatabaseContext context
            ) : base (context)
        {
        }
    }
}
