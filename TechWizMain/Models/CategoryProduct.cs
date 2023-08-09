using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechWizMain.Models
{
    [Table("CategoryProduct")]
    public partial class CategoryProduct
    {
        [Key]
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("CategoryProducts")]
        public virtual Category? Category { get; set; }
        [ForeignKey("ProductId")]
        [InverseProperty("CategoryProducts")]
        public virtual Product? Product { get; set; }
    }
}
