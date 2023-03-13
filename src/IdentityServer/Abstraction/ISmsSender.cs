namespace IdentityServer.Abstraction;

public interface ISmsSender
{
    Task<bool> SendSmsAsync(string number, string message);
}