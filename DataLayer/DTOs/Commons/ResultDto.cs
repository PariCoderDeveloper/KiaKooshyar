using KiaKooshar.Application.DTOs.Commons;
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
            ReturnResultDTO returnResultDTO
            )
        {
            IsSuccess = returnResultDTO.IsSuccess;
            Message = returnResultDTO.Message;
            Error = returnResultDTO.Error;
            ErrorType = returnResultDTO.ErrorType;
        }

        public static ResultDTO Success (
            string message = ""
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Message = message,
                IsSuccess = true
            });

        public static ResultDTO Failure (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO NotFound (
             string message = "",
             List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO Unauthorized (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO Forbid (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO ValidationError (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO Conflict (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO BadRequest (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });

        public static ResultDTO ServerError (
            string message = "",
            List<string> error = null
            )
            => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false
            });
    }

    public record ResultDTO<T> : ResultDTO
    {
        public T? Data { get; }
        private ResultDTO (
            ReturnResultDTO<T> returnResultDTO
            ) : base (new ReturnResultDTO
            {
                Error = returnResultDTO.Error,
                Message = returnResultDTO.Message,
                IsSuccess = returnResultDTO.IsSuccess
            })
        {
            Data = Data;
        }

        public static ResultDTO<T> Success (
                T data,
                string message = ""
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Message = message,
                IsSuccess = true,
                data = data
            });

        public static ResultDTO<T> Failure (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> NotFound (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> Unauthorized (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> Forbid (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> ValidationError (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> Conflict (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> BadRequest (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });

        public static ResultDTO<T> ServerError (
        string message = "",
        List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
            });
    }
}
