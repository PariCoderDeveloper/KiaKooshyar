using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Identities.Cache;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class UserRepository :
        GenericRepository<User>,
        IUserRepository
    {
        private readonly IDatabaseContext _context;
        public UserRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }
        public async Task<List<string>> GetUserPermissions (
            long id,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var userPermission = await _context.Users
                .Where (x => x.Id == id)
                .SelectMany (u => u.UserRole)
                .SelectMany (ur => ur.Role.RolePermission)
                .Select (rp => rp.Permission.DiplayName)
                .Distinct ()
                .ToListAsync ();
            return userPermission;
        }
        public async Task<User?> GetUserByEmail (
            string email,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Users
                 .Where (x => x.Email == email)
                 .FirstOrDefaultAsync (cancellationToken);
        }

        public async Task<List<string>> GetUserRoles (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var roleNames = await _context.Users
                .Where (x => x.Id == userId)
                .SelectMany (x => x.UserRole)
                .Select (x => x.Role.Name)
                .ToListAsync ();
            return roleNames;
        }

        public async Task<User?> GetUserByChangingValues (
            Expression<Func<User, bool>> wherePredicate,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Users
                .Where (wherePredicate)
                .FirstOrDefaultAsync (
                    cancellationToken
                    );
        }
        public async Task<CachedUserDTO?> GetCachedUserAsync (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Users
                .Where (u => u.Id == userId)
                .Select (u => new CachedUserDTO
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Roles = u.UserRole
                        .Select (ur => ur.Role.Name)
                        .Distinct ()
                        .ToList (),
                    RolePermissions = u.UserRole
                        .SelectMany (ur => ur.Role.RolePermission)
                        .Select (rp => rp.Permission.DiplayName)
                        .Distinct ()
                        .ToList (),
                    Permissions = u.UserPermissions
                        .Select (x => x.Permission.DiplayName)
                        .ToList ()
                })
                .FirstOrDefaultAsync (cancellationToken);
        }
        public IQueryable<GetAllUsersDTO?> GetAllUsersAsync (
            CancellationToken cancellationToken = default
            )
        {
            return _context.Users
                .Select (u => new GetAllUsersDTO
                {
                    Id = u.Id,
                    UserStatus = u.Status,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    PhoneNumber = u.PhoneNumber,
                    Roles = u.UserRole
                        .Select (ur => ur.Role.Name)
                        .Distinct ()
                        .ToList (),
                    Permissions = u.UserPermissions
                        .Select (x => x.Permission.DiplayName)
                        .ToList (),
                    RolePermissions = u.UserRole
                        .SelectMany (ur => ur.Role.RolePermission)
                        .Select (rp => rp.Permission.DiplayName)
                        .Distinct ()
                        .ToList (),
                }).AsQueryable ();
        }

        public async Task<bool> HasPermission (
            long userId,
            string permission,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Users
                .Where (u => u.Id == userId)
                .SelectMany (u => u.UserRole)
                .SelectMany (ur => ur.Role.RolePermission)
                .AnyAsync (
                    rp => rp.Permission.DiplayName == permission,
                    cancellationToken);
        }
    }
}
