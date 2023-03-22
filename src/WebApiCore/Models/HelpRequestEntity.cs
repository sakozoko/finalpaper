using System.ComponentModel.DataAnnotations;

namespace WebApiCore.Models;

public class HelpRequestEntity : BaseEntity
{
    [MaxLength(100)] [Required] public string Title { get; set; } = default!;

    [Required] [MaxLength(500)] public string Description { get; set; } = default!;

    [Required] public Guid UserId { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public HelpRequestStatus Status { get; set; } = HelpRequestStatus.New;
    public DateTime? StatusChangedAt { get; set; }
    public Guid? StatusChangedBy { get; set; }
}