using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Permission> Permission => throw new NotImplementedException();

        public IRepository<Role> Role => throw new NotImplementedException();

        public IRepository<User> User => throw new NotImplementedException();

        public IRepository<UserRole> UserRoles => throw new NotImplementedException();

        public IRepository<UserSession> UserSession => throw new NotImplementedException();

        public IRepository<RolePermission> RolePermissions => throw new NotImplementedException();

        public IRepository<RefreshToken> RefreshToken => throw new NotImplementedException();

        public Task<ResultDTO> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
