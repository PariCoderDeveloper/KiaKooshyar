using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Permission> Permission { get; }
        IRepository<Role> Role { get; }
        IRepository<User> User { get; }
        IRepository<UserRole> UserRoles { get; }
        IRepository<UserSession> UserSession { get; }
        IRepository<RolePermission> RolePermissions { get; }
        IRepository<RefreshToken> RefreshToken { get; }
        Task<ResultDTO> CommitAsync();
    }
}
