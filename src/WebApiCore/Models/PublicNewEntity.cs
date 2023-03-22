using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiCore.Models;

public class PublicNewEntity : BaseEntity
{
    [Required] [MaxLength(100)] public string Title { get; set; }

    [Required] [MaxLength(5000)] public string Description { get; set; }

    [Required] [Url] public string ImageUrl { get; set; }

    [Required] [MaxLength(30)] public string Author { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public DateTime UpdatedDate { get; set; }

    [Required] public Guid AuthorId { get; set; }
    [Required] [DefaultValue(false)] public bool IsDeleted { get; set; }
}