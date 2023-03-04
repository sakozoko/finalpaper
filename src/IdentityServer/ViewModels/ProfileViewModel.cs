using IdentityServer.Models;

namespace IdentityServer.ViewModels;

public class ProfileViewModel : ProfileInputModel
{
    public UserViewModel? User { get; set; }
    
}