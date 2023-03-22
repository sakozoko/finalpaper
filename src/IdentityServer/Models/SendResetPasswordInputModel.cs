namespace IdentityServer.Models;

public class SendResetPasswordInputModel
{
    public string? ReturnUrl { get; set; }
    public string? Credential { get; set; }
}