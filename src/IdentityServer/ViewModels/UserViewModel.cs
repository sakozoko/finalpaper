namespace IdentityServer.ViewModels;

public record UserViewModel(string Username, string Email, string ExternalProvider, bool IsPasswordSet, bool CanChangeUsername);