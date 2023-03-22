using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels;

public class ConfirmEmailViewModel
{
    public bool IsSuccessful { get; set; }

    [Url] public string? ReturnUrl { get; set; }
}