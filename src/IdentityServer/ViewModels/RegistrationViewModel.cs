using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class RegistrationViewModel : RegistrationInputModel
{
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));


    public RegistrationViewModel(string returnUrl)
    {
        ReturnUrl = returnUrl;
    }
}