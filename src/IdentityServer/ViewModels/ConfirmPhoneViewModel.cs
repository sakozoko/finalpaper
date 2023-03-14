using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class ConfirmPhoneViewModel : ConfirmPhoneInputModel
{
    public bool IsSuccessful { get; set; }
    public bool IsConfirmed { get; set; }
}