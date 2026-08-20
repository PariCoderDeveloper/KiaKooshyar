using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile ()
        {
            CreateMap<User, PermissionDTO> ().ReverseMap ();
        }
    }
}
