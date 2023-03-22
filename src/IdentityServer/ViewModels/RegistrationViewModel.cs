using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class RegistrationViewModel : RegistrationInputModel
{
    public RegistrationViewModel(string returnUrl)
    {
        ReturnUrl = returnUrl;
    }

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

    public IEnumerable<ExternalProvider> VisibleExternalProviders =>
        ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
}