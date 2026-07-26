using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Authentication;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile ()
        {
            CreateMap<UserRole, RoleDTO> ()
                .ForMember (
                    dest => dest.Id,
                    opt => opt.MapFrom (src => src.Role.Id)
                )
                .ForMember (
                    dest => dest.Name,
                    opt => opt.MapFrom (src => src.Role.Name)
                );
            CreateMap<User, UserInfoDTO> ();
        }
    }
}
