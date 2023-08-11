using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Models
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Subject { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FeedbackDate { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? UserID { get; set; } = null!;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
