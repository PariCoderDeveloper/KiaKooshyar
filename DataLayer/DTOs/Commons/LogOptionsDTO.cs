using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Commons
{
    public class LogOptionsDTO
    {
        public string? Message { get; set; }
        public object[] Args { get; set; } = Array.Empty<object> ();
        public object? Request { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Information;
        public bool IncludeResponse { get; set; } = true;
        public string? IP { get; set; }
    }
}
