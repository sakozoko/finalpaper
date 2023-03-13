namespace IdentityServer.Entities;

public class VerificationSmsCode
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public int Code { get; set; }
}