using AutoMapper;
using KiaKooshar.Application.Requests.Identities.User.Commands;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Mppings
{
    public class UserProfile : Profile
    {
        public UserProfile ()
        {
            CreateMap<RegisterUserCommand, User> ();
            CreateMap<User, RegisterUserCommand> ();

            CreateMap<UpdateUserCommand, User> ();
            CreateMap<User, UpdateUserCommand> ();
        }
    }
}
