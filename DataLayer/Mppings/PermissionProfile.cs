using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Admin.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile ()
        {
            CreateMap<Permission, GetPermissionDTO> ().ReverseMap ();
        }
    }
}
