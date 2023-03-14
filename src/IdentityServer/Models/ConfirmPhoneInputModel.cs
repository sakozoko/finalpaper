using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models;

public class ConfirmPhoneInputModel
{
    [Required]
    public string? ReturnUrl { get; set; }
    [Required]
    public string Code { get; set; }
}