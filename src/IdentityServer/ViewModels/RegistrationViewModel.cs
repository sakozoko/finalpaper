using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class RegistrationViewModel : RegistrationInputModel
{
    public RegistrationViewModel(string returnUrl)
    {
        ReturnUrl = returnUrl;
    }
}