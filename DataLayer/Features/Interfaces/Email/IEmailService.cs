namespace KiaKooshar.Application.Features.Interfaces.Email
{
    public interface IEmailService
    {
        public interface IEmailService
        {
            Task SendAsync (
                string to,
                string subject,
                string body,
                CancellationToken cancellationToken = default );
        }
    }
}
