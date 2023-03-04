namespace IdentityServer.Models;

public class ResetPasswordInputModel
{
    public string? Id { get; set; }
    public string? ReturnUrl { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Token { get; set; }
}