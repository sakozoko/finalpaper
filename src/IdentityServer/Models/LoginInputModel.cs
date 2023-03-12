using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;

public class LoginInputModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? ReturnUrl { get; set; }
    public bool RememberLogin { get; set; }
}