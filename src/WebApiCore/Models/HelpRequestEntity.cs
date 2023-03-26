using System.ComponentModel.DataAnnotations;

namespace WebApiCore.Models;

public class HelpRequestEntity : BaseEntity
{
    [MaxLength(100)] [Required] public string Title { get; set; } = default!;

    [Required] [MaxLength(500)] public string Description { get; set; } = default!;
    
    [MaxLength(5000)] public string? Answer { get; set; }
    [Required] public Guid UserId { get; set; }

    [Required][MaxLength(30)] public string UserName { get; set; } = default!;
    [Required][MaxLength(50)] public string UserEmail { get; set; } = default!;

    [Required] public DateTime CreatedAt { get; set; }

    public HelpRequestStatus Status { get; set; } = HelpRequestStatus.New;
    public DateTime? StatusChangedAt { get; set; }
    public Guid? StatusChangedBy { get; set; }
}