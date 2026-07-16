using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.DTOs.Common
{
    public record ResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        protected ResultDTO(bool isSuccess, string? message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ResultDTO Success(string message = "")
        => new ResultDTO(true, message);

        public static ResultDTO Failure(string message = "")
        => new ResultDTO(false, message);
        public static ResultDTO Forbid(
            string message = ""
            ) => new ResultDTO(false, message);
    }

    public record ResultDTO<T> : ResultDTO
    {
        public T? Data { get; }
        private ResultDTO(
            bool isSuccess,
            string? message,
            T? data)
            : base(isSuccess, message)
        {
            Data = data;
        }
        public static ResultDTO<T> Success(
            T data,
            string message = ""
            ) => new ResultDTO<T>(true, message, data);

        public static ResultDTO<T> Failure(
            string message = ""
            ) => new ResultDTO<T>(false, message, default);

        public static ResultDTO<T> Forbid(
            string message = ""
            ) => new ResultDTO<T>(false, message, default);
    }
}
