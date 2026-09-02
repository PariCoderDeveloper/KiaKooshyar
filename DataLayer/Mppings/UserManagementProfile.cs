using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class UserManagementProfile : Profile
    {
        public UserManagementProfile ()
        {
            CreateMap<GetAllUsersDTO, User> ().ReverseMap ();
            CreateMap<User, GetUserByIdDTO> ().ReverseMap ();
        }
    }
}
