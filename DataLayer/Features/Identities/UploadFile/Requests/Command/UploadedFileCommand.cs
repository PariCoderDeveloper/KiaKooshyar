using KiaKooshar.Application.DTOs.Common;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.UploadFile.Requests.Command
{
    public class UploadedFileCommand :
        IRequest<ResultDTO>
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public byte[] Data { get; set; } = [];
    }
}
