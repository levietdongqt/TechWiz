using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechWizMain.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [InverseProperty("Category")]
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
