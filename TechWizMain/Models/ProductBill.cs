using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechWizMain.Models
{
    [Table("ProductBill")]
    public partial class ProductBill
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SalePrice { get; set; }
        public int? ProductId { get; set; }
        public int? BillId { get; set; }

        [ForeignKey("BillId")]
        [InverseProperty("ProductBills")]
        public virtual Bill? Bill { get; set; }
        [ForeignKey("ProductId")]
        [InverseProperty("ProductBills")]
        public virtual Product? Product { get; set; }
    }
}
