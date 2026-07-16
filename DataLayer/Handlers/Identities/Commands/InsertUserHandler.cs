using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Requests.Identities.Commands;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.Handlers.Identities.Commands
{
    public class InsertUserHandler
        : IRequestHandler<InsertUserCommand, ResultDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        public InsertUserHandler(
            IUnitOfWork unit, 
            IPasswordHasher passwordHasher, 
            IMapper mapper
            )
        {
            _mapper = mapper;
            _unit = unit;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResultDTO> Handle(
            InsertUserCommand request, 
            CancellationToken cancellationToken
            )
        {   
            var result = new ResultDTO();

            var user = _mapper.Map<User>(request);
           
            _unit.User.AddAsync(user);
            await _unit.CommitAsync();


            return result;
        }
    }
}
