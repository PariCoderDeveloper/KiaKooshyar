namespace KiaKooshar.Application.Features.Interfaces.SignalR
{
    public interface IUserNotificationService
    {
        Task NotifyForceLogoutAsync (
            string userId,
            string reason
            );
        Task NotifyForceLogoutAsync (
            IEnumerable<string> userId,
            string reason
            );
    }
}
