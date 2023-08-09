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
        public string Content { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FeedbackDate { get; set; }
        [StringLength(450)]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Feedbacks")]
        public virtual UserManager?   User { get; set; }
    }
}
