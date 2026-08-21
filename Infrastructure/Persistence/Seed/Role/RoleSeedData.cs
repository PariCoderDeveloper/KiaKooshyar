using KiaKooshar.Domain.Constants;

namespace KiaKooshar.Infrastructure.Persistence.Seed.Role
{
    public static class RoleSeedData
    {
        public const long SuperAdministratorId = 1;
        public const string SuperAdministratorCode = Roles.SuperAdmin;

        public const long AdministratorId = 1;
        public const string AdministratorCode = Roles.Admin;

        public const long ManagerId = 1;
        public const string ManagerCode = Roles.Manager;

        public const long UserId = 1;
        public const string UserCode = Roles.User;

        public static List<KiaKooshar.Domain.Entities.Identity.Role>
            GetRoles ()
        {
            var roles = new List
                <KiaKooshar.Domain.Entities.Identity.Role>
            {
                new()
                {
                    Id = SuperAdministratorId,
                    Code = SuperAdministratorCode
                },
                new()
                {
                    Id =  AdministratorId,
                    Code = AdministratorCode
                },
                new()
                {
                    Id =  ManagerId,
                    Code = ManagerCode
                },
                new()
                {
                    Id =  UserId,
                    Code = UserCode
                }
            };
            return roles;
        }
    }
}
