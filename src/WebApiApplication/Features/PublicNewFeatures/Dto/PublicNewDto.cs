namespace WebApiApplication.Features.PublicNewFeatures.Dto;

public class PublicNewDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl{ get; set;}
    public string? Author { get; set; }
    public DateTime CreatedAt { get; set; }
}