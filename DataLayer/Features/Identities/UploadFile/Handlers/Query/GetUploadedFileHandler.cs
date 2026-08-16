using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.UploadFile;
using KiaKooshar.Application.Features.Identities.UploadFile.Requests.Query;
using KiaKooshar.Application.Features.Interfaces.Files;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.UploadFile.Handlers.Query
{
    public class GetUploadedFileHandler
        : IRequestHandler<GetUploadedFileCommand, ResultDTO<UploadedFileResponseDTO>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IFileConverter _fileConverter;
        public GetUploadedFileHandler (
            IUnitOfWork unit,
            IMapper mapper,
            IFileConverter fileConverter
            )
        {
            _unit = unit;
            _mapper = mapper;
            _fileConverter = fileConverter;
        }
        public async Task<ResultDTO<UploadedFileResponseDTO>> Handle (
            GetUploadedFileCommand request,
            CancellationToken cancellationToken
            )
        {
            var uploadedFile = await _unit.UploadedFile.GetByIdAsync (
                request.id
                );
            if ( uploadedFile is null )
                return ResultDTO<UploadedFileResponseDTO>.NotFound
                    (
                        "File not found"
                    );
            var uploadFileResult = _mapper.Map<UploadedFileResponseDTO>
                (
                    uploadedFile
                );
            uploadFileResult.Data = _fileConverter.ToStream
                (
                    uploadedFile.Data
                );
            return ResultDTO<UploadedFileResponseDTO>.Success (
                uploadFileResult,
                "Operation does successfully"
                );
        }
    }
}
