using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Users.Request.Commands;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class UserProfile : Profile
    {
        public UserProfile ()
        {

            CreateMap<RegisterUserDTO, User> ().ReverseMap ();

            CreateMap<RegisterUserDTO, User> ().ReverseMap ();

        }
    }
}
