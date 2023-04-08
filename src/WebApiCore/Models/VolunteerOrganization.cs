namespace WebApiCore.Models;

public class VolunteerOrganization
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string[]? Phones { get; set; }
    public string[]? Addresses { get; set; }
    public string[]? SocialNetworks { get; set; }
}