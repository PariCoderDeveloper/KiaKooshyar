using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Commons
{
    public class ReturnResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string>? Error { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
    public class ReturnResultDTO<T> : ReturnResultDTO
    {
        public T? Data { get; set; }
    }
}
