using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;

public class ProfileInputModel
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? ReturnUrl { get; set; }
}