using KiaKooshar.Domain.Constants;

namespace KiaKooshar.Infrastructure.Persistence.Seed.Role
{
    public static class RoleSeedData
    {
        public const long SuperAdministratorId = 1;
        public const string SuperAdministratorCode = Roles.SuperAdmin;

        public const long AdministratorId = 2;
        public const string AdministratorCode = Roles.Admin;

        public const long ManagerId = 3;
        public const string ManagerCode = Roles.Manager;

        public const long UserId = 4;
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
                    Name = "Super Admin",
                    Code = SuperAdministratorCode
                },
                new()
                {
                    Id =  AdministratorId,
                    Name = AdministratorCode,
                    Code = AdministratorCode
                },
                new()
                {
                    Id =  ManagerId,
                    Name = ManagerCode,
                    Code = ManagerCode
                },
                new()
                {
                    Id =  UserId,
                    Name = UserCode,
                    Code = UserCode
                }
            };
            return roles;
        }
    }
}
