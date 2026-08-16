using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.DTOs.Identities.UploadFile;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.UploadFile.Requests.Query
{
    public class GetUploadedFileCommand
        : IRequest<ResultDTO<UploadedFileResponseDTO>>
    {
        public long id { get; set; }
    }
}
