using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [Serializable]
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
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.00,10000000.00,ErrorMessage ="Price is invalid !")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0, 10000000, ErrorMessage = "Price is invalid !")]

        public decimal BasePrice { get; set; }
        [Column("ImageURL")]
        [StringLength(255)]
        [DisplayName("Image")]
        public string? ImageUrl { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Type")]
        public string TypeProduct { get; set; }
        public int? DiscountId { get; set; }

        [DisplayName("Inventory")]
        [Range(0,10000000,ErrorMessage ="Price is invalid !")]
        public int InventoryQuantity { get; set; }

        [DisplayName("Date Sale")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Selling")]
        public bool status { get; set; } = true;


        [ForeignKey("DiscountId")]
        [InverseProperty("Products")]
        [DisplayName("Discount")]
        public virtual Discount? Discount { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductBill> ProductBills { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
