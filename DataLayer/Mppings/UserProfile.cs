using AutoMapper;
using KiaKooshar.Application.Requests.Identities.Commands;
using KiaKooshar.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.Mppings
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<InsertUserCommand, User>();
            CreateMap<User, InsertUserCommand>();
        }
    }
}
