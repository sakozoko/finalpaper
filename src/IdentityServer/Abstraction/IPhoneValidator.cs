namespace IdentityServer.Abstraction;

public interface IPhoneValidator
{
    Task<(bool IsValid, string? International)> ValidatePhoneNumberAsync(string? phoneNumber);
}