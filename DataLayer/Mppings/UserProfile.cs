using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Users.Commands;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class UserProfile : Profile
    {
        public UserProfile ()
        {
            CreateMap<RegisterUserDTO, User> ().ReverseMap ();
            CreateMap<UpdateUserDTO, User> ().ReverseMap ();
        }
    }
}
