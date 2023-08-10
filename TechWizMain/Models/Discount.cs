using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechWizMain.Models
{
    [Table("Discount")]
    public partial class Discount
    {
        public Discount()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public double Percent { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        [InverseProperty("Discount")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
