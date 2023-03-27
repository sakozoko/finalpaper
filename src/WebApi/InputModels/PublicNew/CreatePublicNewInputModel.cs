namespace WebApi.InputModels.PublicNew;

public record class CreatePublicNewInputModel(
    string Title, 
    string Description, 
    string? ImageUrl,
    DateTime? CreatedAt
    );