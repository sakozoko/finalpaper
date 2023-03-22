using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class LoginViewModel : LoginInputModel
{
    public LoginViewModel(string returnUrl)
    {
        ReturnUrl = returnUrl;
    }

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

    public IEnumerable<ExternalProvider> VisibleExternalProviders =>
        ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
}