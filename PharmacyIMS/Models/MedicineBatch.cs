using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyIMS.Models
{
    /// <summary>
    /// 药品批次管理
    /// </summary>
    public class MedicineBatch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string BatchNo { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? ProductionDate { get; set; }

        [MaxLength(200)]
        public string? Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }

        public int? PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder? PurchaseOrder { get; set; }
    }
}
