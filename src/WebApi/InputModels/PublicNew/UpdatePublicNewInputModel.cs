namespace WebApi.InputModels.PublicNew;

public record UpdatePublicNewInputModel( 
    Guid Id,
    string Title, 
    string Description, 
    string? ImageUrl,
    DateTime CreatedAt, 
    string Author
    );
