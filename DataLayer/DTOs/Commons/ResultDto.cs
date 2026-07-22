using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Common
{
    public record ResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string>? Error { get; set; }
        public ResultStatus ResultStatus { get; set; }
        protected ResultDTO (
            ReturnResultDTO returnResultDTO
            )
        {
            IsSuccess = returnResultDTO.IsSuccess;
            Message = returnResultDTO.Message;
            Error = returnResultDTO.Error ?? new List<string> ();
            ResultStatus = returnResultDTO.ResultStatus;
        }

        public static ResultDTO Success (
            string message = ""
            ) => new ResultDTO (new ReturnResultDTO
            {
                Message = message,
                IsSuccess = true,
                ResultStatus = ResultStatus.Success
            });

        public static ResultDTO Failure (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Failure
            });

        public static ResultDTO NotFound (
             string message = "",
             List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.NotFound
            });

        public static ResultDTO Unauthorized (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Unauthorized
            });

        public static ResultDTO Forbid (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Forbid
            });

        public static ResultDTO ValidationError (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.ValidationError
            });

        public static ResultDTO Conflict (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Conflict
            });

        public static ResultDTO BadRequest (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.BadRequest
            });

        public static ResultDTO ServerError (
            string message = "",
            List<string> error = null
            ) => new ResultDTO (new ReturnResultDTO
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.ServerError
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
            Data = returnResultDTO.Data;
        }

        public static ResultDTO<T> Success (
                T data,
                string message = ""
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Message = message,
                IsSuccess = true,
                Data = data,
                ResultStatus = ResultStatus.Success
            });

        public new static ResultDTO<T> Failure (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error ?? new List<string> (),
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.ServerError
            });

        public new static ResultDTO<T> NotFound (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.NotFound
            });

        public new static ResultDTO<T> Unauthorized (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Unauthorized
            });

        public new static ResultDTO<T> Forbid (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Forbid
            });

        public new static ResultDTO<T> ValidationError (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.ValidationError
            });

        public new static ResultDTO<T> Conflict (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.Conflict
            });

        public new static ResultDTO<T> BadRequest (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.BadRequest
            });

        public new static ResultDTO<T> ServerError (
            string message = "",
            List<string> error = null
            ) => new ResultDTO<T> (new ReturnResultDTO<T>
            {
                Error = error,
                Message = message,
                IsSuccess = false,
                ResultStatus = ResultStatus.ServerError
            });
    }
}
