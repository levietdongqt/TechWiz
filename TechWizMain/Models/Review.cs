using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Models
{
    [Table("Review")]
    public partial class Review
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Content { get; set; }
        public int? Rating { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReviewDate { get; set; }
        public int? ProductId { get; set; }
        [StringLength(450)]
        public string? UserId { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("Reviews")]
        public virtual Product? Product { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Reviews")]
        public virtual UserManager? User { get; set; }
    }
}
