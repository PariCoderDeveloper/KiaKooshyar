using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Extentions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult (
                this ResultDTO resultDTO
            )
        {
            return resultDTO.ResultStatus switch
            {
                ResultStatus.Success =>
                    new OkObjectResult (resultDTO),
                ResultStatus.NotFound =>
                    new NotFoundObjectResult (resultDTO),
                ResultStatus.Unauthorized =>
                    new UnauthorizedObjectResult (resultDTO),
                ResultStatus.Forbid =>
                    new ObjectResult (resultDTO)
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    },
                ResultStatus.ValidationError &
                ResultStatus.Failure &
                ResultStatus.BadRequest =>
                    new BadRequestObjectResult (resultDTO),
                ResultStatus.Conflict =>
                    new ConflictObjectResult (resultDTO),
                ResultStatus.ServerError =>
                    new ObjectResult (resultDTO)
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    },
                _ => new OkObjectResult (resultDTO)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };
        }
    }
}
