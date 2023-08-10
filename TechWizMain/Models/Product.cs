using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechWizMain.Models
{
    public enum TypeProduct
    {
        Plant,
        Accessories
    }
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
            ProductBills = new HashSet<ProductBill>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? BasePrice { get; set; }
        [Column("ImageURL")]
        [StringLength(255)]
        public string ImageUrl { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string? TypeProduct { get; set; }
        public int? DiscountId { get; set; }

        public int InventoryQuantity { get; set; }

        public bool? Status { get; set; } = true;


        public bool status { get; set; } = true;


        [ForeignKey("DiscountId")]
        [InverseProperty("Products")]
        public virtual Discount? Discount { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductBill> ProductBills { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
