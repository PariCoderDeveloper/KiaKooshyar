using KiaKooshar.Application.DTOs.Commons;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.Logging
{
    public static class AuthLogExtensions
    {
        public static void LogUserLogin (
            this IBaseLogger logger,
            User user,
            bool success,
            string? failReason = null,
            string device = "UNKNOWN",
            string ip = "UKNOWN"
            )
        {
            logger.Logging (new LogOptionsDTO
            {
                Message = success
                    ? "User {UserId} ({Username}) logged in successfully from {Device}"
                    : "Failed login attempt for {Username} - Reason: {Reason}",
                Args = success
                    ? new object[] { user.Id, user.UserName, device }
                    : new object[] { user.UserName, failReason ?? "Unknown" },
                Request = new { Username = user.UserName, Device = device },
                Level = success ? LogLevel.Information : LogLevel.Warning,
                IncludeResponse = false,
                IP = ip
            });
        }
        public static void LogUserLogout (
            this IBaseLogger logger,
            long userId,
            string device = "UKNOWN",
            string ip = "UKNOWN"
            )
        {
            logger.Logging (new LogOptionsDTO
            {
                Message = "User {UserId} logged out from {Device}",
                Args = new object[] { userId, device },
                Level = LogLevel.Information,
                IncludeResponse = false,
                IP = ip
            });
        }
    }
}
