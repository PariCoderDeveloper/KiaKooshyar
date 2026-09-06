using KiaKooshar.Application.Features.Interfaces.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers.SignalR.V1
{
    [Route ("api/[controller]")]
    [ApiController]
    public class Hub : ControllerBase
    {
        private readonly IUserNotificationService _userNotificationService;
        public Hub (
            IUserNotificationService userNotificationService
            )
        {
            _userNotificationService = userNotificationService;
        }
        [Authorize]
        public async Task<IActionResult> Notification (
            long userId
            )
        {
            await _userNotificationService
               .NotifyForceLogoutAsync (
                   userId.ToString (),
                   "For some reason, you should login again"
               );
            return Ok ();
        }

    }
}
