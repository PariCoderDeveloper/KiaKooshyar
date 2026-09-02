using KiaKooshar.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment
{
    public class UnblockUserCommand 
        : IRequest<ResultDTO>
    {
        public long Id { get; set; }
        public long userId { get; set; }
    }
}
