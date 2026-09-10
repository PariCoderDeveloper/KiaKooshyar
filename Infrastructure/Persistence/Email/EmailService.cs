using KiaKooshar.Application.Features.Interfaces.Email;
using KiaKooshar.Infrastructure.Persistence.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

public sealed class EmailService : IEmailService, IDisposable
{
    private readonly EmailOptions _options;

    private readonly SemaphoreSlim _semaphore = new (1, 1);

    public EmailService ( IOptions<EmailOptions> options )
    {
        _options = options.Value;

        _semaphore = new SemaphoreSlim (
            _options.MaxConcurrentEmails,
            _options.MaxConcurrentEmails);
    }

    public async Task SendAsync (
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken = default )
    {
        try
        {
            await _semaphore.WaitAsync (cancellationToken);

            await SendInternalAsync (
                to,
                subject,
                body,
                cancellationToken);
        }
        finally
        {
            _semaphore.Release ();
        }
    }

    private async Task SendInternalAsync (
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken )
    {
        var message = new MimeMessage ();

        message.From.Add (
            new MailboxAddress (
                _options.FromName,
                _options.FromEmail));

        message.To.Add (
            MailboxAddress.Parse (to));

        message.Subject = subject;

        message.Body = new BodyBuilder
        {
            HtmlBody = body
        }.ToMessageBody ();

        using var smtp = new SmtpClient ();

        await smtp.ConnectAsync (
            _options.Host,
            _options.Port,
            SecureSocketOptions.StartTls,
            cancellationToken);

        await smtp.AuthenticateAsync (
            _options.Username,
            _options.Password,
            cancellationToken);

        await smtp.SendAsync (
            message,
            cancellationToken);

        await smtp.DisconnectAsync (
            true,
            cancellationToken);
    }

    public void Dispose ()
    {
        _semaphore.Dispose ();
    }
}