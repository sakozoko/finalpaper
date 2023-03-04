using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class PasswordChangingViewModel : PasswordChangingInputModel
{
    public bool IsPasswordSet { get; set; }
    public bool IsSuccessful { get; set; }

}