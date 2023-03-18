using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class HelpRequestEntity : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public HelpRequestStatus Status { get; set; } = HelpRequestStatus.New;
        public DateTime? StatusChangedAt { get; set; }
        public Guid? StatusChangedBy { get; set; }
    }
}