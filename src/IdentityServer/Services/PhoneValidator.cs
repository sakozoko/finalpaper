using IdentityServer.Abstraction;

namespace IdentityServer.Services;

public class PhoneValidator : IPhoneValidator
{
    public PhoneValidator(string apiKey)
    {
        ApiKey = apiKey;
        Client = new HttpClient
        {
            BaseAddress = new Uri("https://phonevalidation.abstractapi.com/v1/")
        };
    }

    public HttpClient Client { get; }
    public string ApiKey { get; }

    public async Task<(bool, string?)> ValidatePhoneNumberAsync(string? phoneNumber)
    {
        var response = await Client.GetAsync($"?api_key={ApiKey}&phone={phoneNumber}");
        var result = await response.Content.ReadFromJsonAsync<PhoneValidationResponse>();
        return (result?.Valid ?? false, result?.Format.International);
    }

    private sealed class PhoneValidationResponse
    {
        public string Phone { get; set; } = string.Empty;
        public bool Valid { get; set; }
        public Format Format { get; } = new();
        public Country Country { get; set; } = new();
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Carrier { get; set; } = string.Empty;
    }

    private sealed class Format
    {
        public string International { get; } = string.Empty;
        public string Local { get; set; } = string.Empty;
    }

    private sealed class Country
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
    }
}