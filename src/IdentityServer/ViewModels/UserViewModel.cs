namespace IdentityServer.ViewModels;

public record UserViewModel(string Username, string Email, string ExternalProvider, bool IsPasswordSet,
    bool CanChangeUsername, string PhoneNumber, bool EmailConfirmed, bool PhoneNumberConfirmed);