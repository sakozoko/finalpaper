using IdentityServer.Abstraction;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace IdentityServer.Services;

public class TwilioSmsSender : ISmsSender
{
    public TwilioSmsSender(string accountSid, string authToken)
    {
        TwilioClient.Init(accountSid, authToken);
    }

    public async Task<bool> SendSmsAsync(string number, string message)
    {
        var msg = await MessageResource.CreateAsync(
            body: message,
            from: new PhoneNumber("+15073846011"),
            to: new PhoneNumber(number));
        return msg.Status == MessageResource.StatusEnum.Queued;
    }
}