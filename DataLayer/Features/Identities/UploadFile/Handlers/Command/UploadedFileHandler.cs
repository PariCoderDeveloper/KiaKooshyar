using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.UploadFile.Requests.Command;
using KiaKooshar.Domain.Entities.UploadFile;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.UploadFile.Handlers.Command
{
    public class UploadedFileHandler :
        IRequestHandler<UploadedFileCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public UploadedFileHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<ResultDTO> Handle (
            UploadedFileCommand request,
            CancellationToken cancellationToken
            )
        {
            var uploadFile = _mapper.Map<UploadedFile> (request);
            var result = await _unit.UploadedFile.AddAsync (
                uploadFile,
                cancellationToken
                );
            await _unit.CommitAsync (cancellationToken);
            if ( result is null )
                return ResultDTO.Failure ("File doesnt add");
            return ResultDTO.Success ("File added successfully");
        }
    }
}
