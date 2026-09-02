using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class RolePermissionManagementProfile :
        Profile
    {
        public RolePermissionManagementProfile ()
        {
            CreateMap<RemoveRoleFromUserCommand, UserRole> ()
                .ReverseMap ();
            CreateMap<User, GetAllUsersDTO> ().ReverseMap ();
            CreateMap<User, RegisterUserDTO> ().ReverseMap ();
        }
    }
}
