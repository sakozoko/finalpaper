using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;

public class RegistrationInputModel
{
    [Required] [EmailAddress] public string? Email { get; set; }

    [Required] public string? Password { get; set; }

    [Required] [Compare("Password")] public string? ConfirmPassword { get; set; }

    [Required] [MinLength(4)] public string? Username { get; set; }

    [Required] public string? ReturnUrl { get; set; }

    [Required] [Phone] public string? PhoneNumber { get; set; }
}