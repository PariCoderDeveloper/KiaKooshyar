using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Common
{
    public record ResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Error { get; set; }
        public ErrorType ErrorType { get; set; }
        protected ResultDTO (
            bool isSuccess,
            string? message,
            List<string> error,
            ErrorType errorType
            )
        {
            IsSuccess = isSuccess;
            Message = message;
            Error = error;
        }

        public static ResultDTO Success (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (true, message, error, ErrorType.Success);

        public static ResultDTO Failure (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.Failure);

        public static ResultDTO NotFound (
             string message = "",
             List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.NotFound);

        public static ResultDTO Unauthorized (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.Unauthorized);

        public static ResultDTO Forbid (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.Forbid);

        public static ResultDTO ValidationError (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.ValidationError);

        public static ResultDTO Conflict (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.Conflict);

        public static ResultDTO BadRequest (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.BadRequest);

        public static ResultDTO ServerError (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (false, message, error, ErrorType.ServerError);
    }

    public record ResultDTO<T> : ResultDTO
    {
        public T? Data { get; }
        private ResultDTO (
            bool isSuccess,
            string? message,
            T? data,
            List<string> error,
            ErrorType errorType
            ) : base (isSuccess, message, error, errorType)
        {
            Data = data;
        }

        public static ResultDTO<T> Success (
            T data,
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (true, message, data, error, ErrorType.BadRequest);

        public static ResultDTO<T> Failure (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.BadRequest);

        public static ResultDTO<T> NotFound (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.NotFound);

        public static ResultDTO<T> Unauthorized (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.Unauthorized);

        public static ResultDTO<T> Forbid (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.Forbid);

        public static ResultDTO<T> ValidationError (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.ValidationError);

        public static ResultDTO<T> Conflict (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.Conflict);

        public static ResultDTO<T> BadRequest (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (false, message, default, error, ErrorType.BadRequest);

        public static ResultDTO<T> ServerError (
        string message = "",
        List<string> error = null
        )
        => new ResultDTO<T> (false, message, default, error, ErrorType.ServerError);
    }
}
