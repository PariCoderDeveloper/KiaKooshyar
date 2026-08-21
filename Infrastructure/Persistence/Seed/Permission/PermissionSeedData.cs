using KiaKooshar.Domain.Constants;

namespace KiaKooshar.Infrastructure.Persistence.Seed.Permission
{
    public static class PermissionSeedData
    {
        public const long ViewUsersId = -1;
        public const string ViewUsersDisplayName = "View Users";

        public const long CreateUserId = -2;
        public const string CreateUserDisplayName = "Create User";

        public const long UpdateUserId = -3;
        public const string UpdateUserDisplayName = "Update User";

        public const long DeleteUserId = -4;
        public const string DeleteUserDisplayName = "Delete User";

        public const long DisableUserId = -5;
        public const string DisableUserDisplayName = "Disable User";

        public const long BlockUserId = -6;
        public const string BlockUserDisplayName = "User Block";

        public static List<KiaKooshar.Domain.Entities.Identity.Permission>
            GetPermissions ()
        {
            var permissions = new List<KiaKooshar.Domain.Entities.Identity.Permission>
            {
                new()
                {
                    Id = ViewUsersId,
                    DiplayName =  ViewUsersDisplayName,
                    Code = Permissions.UserView
                },
                new()
                {
                    Id =  CreateUserId,
                    DiplayName =  CreateUserDisplayName,
                    Code = Permissions.UserCreate
                },
                new()
                {
                    Id =  UpdateUserId,
                    DiplayName =  UpdateUserDisplayName,
                    Code = Permissions.UserUpdate
                },
                new()
                {
                    Id =  DeleteUserId,
                    DiplayName =  DeleteUserDisplayName,
                    Code = Permissions.UserDelete
                },
                new()
                {
                    Id =  DisableUserId,
                    DiplayName =  DisableUserDisplayName,
                    Code = Permissions.UserDisable
                },
                new()
                {
                    Id =  BlockUserId,
                    DiplayName =  BlockUserDisplayName,
                    Code = Permissions.UserBlock
                }
            };
            return permissions;
        }
    }
}