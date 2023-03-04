using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;

public class LoginInputModel
{
    [Required]
    [MinLength(4)]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? ReturnUrl { get; set; }
    public bool RememberLogin { get; set; }
}