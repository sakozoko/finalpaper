using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;

public class PasswordChangingInputModel
{
    public string? OldPassword { get; set; }
    public string? Password { get; set; }

    [Compare("Password")] public string? ConfirmPassword { get; set; }

    [Required] public string? ReturnUrl { get; set; }
}