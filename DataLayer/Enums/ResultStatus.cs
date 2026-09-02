namespace KiaKooshar.Domain.Enums;

public enum ResultStatus
{
    Success = 200,
    Failure = 500,
    NotFound = 404,
    Unauthorized = 401,
    Forbid = 403,
    ValidationError = 422,
    Conflict = 409,
    BadRequest = 400,
    ServerError = 500
}
