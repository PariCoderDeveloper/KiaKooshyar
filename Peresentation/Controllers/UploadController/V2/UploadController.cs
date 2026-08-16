using Asp.Versioning;
using KiaKooshar.Application.Features.Identities.UploadFile.Requests.Command;
using KiaKooshar.Application.Features.Identities.UploadFile.Requests.Query;
using KiaKooshar.Application.Features.Interfaces.Files;
using KiaKooshar.Peresentation.Extentions;
using KiaKooshar.Peresentation.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers.UploadController.V2
{
    [ApiVersion (2.0)]
    [Route ("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IFileConverter _converter;
        private readonly IMediator _mediator;
        private static readonly SemaphoreSlim _uploadSemaphore = new (2, 2);
        public UploadController (
            IFileConverter converter,
            IMediator mediator
            )
        {
            _converter = converter;
            _mediator = mediator;
        }
        [HttpPost ("upload")]
        [Consumes ("multipart/form-data")]
        public async Task<IActionResult> Upload (
            [FromForm] UploadFileRequest request,
            CancellationToken cancellationToken
            )
        {
            await _uploadSemaphore.WaitAsync (
                cancellationToken
                );
            try
            {
                await using var stream =
                request.FormFile.OpenReadStream ();
                var dataByte = await _converter.ToByteAsync (
                    stream,
                    cancellationToken
                    );
                var uploadResult = await _mediator.Send (new UploadedFileCommand
                {
                    Data = dataByte,
                    ContentType = request.FormFile.ContentType,
                    FileName = request.FormFile.FileName,
                    FileSize = request.FormFile.Length
                });
                return ResultExtensions.ToActionResult (uploadResult);
            }
            finally
            {
                _uploadSemaphore.Release ();
            }
        }
        [HttpGet ("download/{fileId}")]
        public async Task<IActionResult> DownloadFile (
            long fileId,
            CancellationToken cancellationToken
            )
        {
            var getFileResult = await _mediator.Send (new GetUploadedFileCommand
            {
                id = fileId
            });
            return File (
                getFileResult.Data.Data,
                getFileResult.Data.ContentType,
                getFileResult.Data.FileName
                );
        }
    }
}
