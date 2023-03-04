using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class SendResetPasswordViewModel : SendResetPasswordInputModel
{
    public string? StatusMessage { get; set; }
}