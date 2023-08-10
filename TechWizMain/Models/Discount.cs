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

<<<<<<< HEAD
        public DateTime DateTime { get; set; }
=======
        public DateTime DateBegin { get; set; }
>>>>>>> 051e3c39b56ea5c82b4056f329af5cb2edd40c2a
        public DateTime DateEnd { get; set; }

        [InverseProperty("Discount")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
