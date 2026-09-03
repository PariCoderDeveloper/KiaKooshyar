using KiaKooshar.Application.Features.Interfaces.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace KiaKooshar.Infrastructure.SignalRHub
{
    public class UserNotificationService :
        IUserNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public UserNotificationService (
            IHubContext<NotificationHub> hubContext
            )
        {
            _hubContext = hubContext;
        }
        public async Task NotifyForceLogoutAsync (
            string userId,
            string reason
            )
        {
            await _hubContext.Clients.User (userId)
                .SendAsync ("Force Logout", reason);
        }

        public async Task NotifyForceLogoutAsync (
            IEnumerable<string> userIds,
            string reason
            )
        {
            await _hubContext.Clients.Users (userIds)
                .SendAsync ("", reason);
        }
    }
}
