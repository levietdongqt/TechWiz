using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Models
{

    public enum ProcessBill
    {
       Cancel,
       Pending,
       Success,
       Temporary
    }
    [Table("Bill")]
    [Serializable]
    public partial class Bill
    {
        public Bill()
        {
            ProductBills = new HashSet<ProductBill>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Total { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string? Status { get; set; }
        [StringLength(13)]
        [Unicode(false)]
        public string DeliveryPhone { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string DeliveryAddress { get; set; } = null!;
        [StringLength(450)]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Bills")]
        public virtual UserManager? User { get; set; }
        [InverseProperty("Bill")]
        public virtual ICollection<ProductBill> ProductBills { get; set; }
    }
}
