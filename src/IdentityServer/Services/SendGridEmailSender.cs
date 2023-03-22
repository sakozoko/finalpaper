using IdentityServer.Abstraction;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdentityServer.Services;

public class SendGridEmailSender : IEmailSender
{
    private readonly ISendGridClient _gridClient;

    public SendGridEmailSender(ISendGridClient gridClient)
    {
        _gridClient = gridClient;
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string message)
    {
        var msg = MailHelper.CreateSingleEmail(
            new EmailAddress("olesis.ko@gmail.com"),
            new EmailAddress(email),
            subject,
            message,
            message);
        var result = await _gridClient.SendEmailAsync(msg);
        return result.IsSuccessStatusCode;
    }
}