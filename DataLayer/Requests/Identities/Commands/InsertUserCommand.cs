using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.Requests.Identities.Commands
{
    public class InsertUserCommand : IRequest<ResultDTO>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        public UserStatus Status { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }

    }
}
