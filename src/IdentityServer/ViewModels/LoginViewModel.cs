using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class LoginViewModel : LoginInputModel
{
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

    public LoginViewModel(string returnUrl)
    {
        ReturnUrl= returnUrl;
    }
}