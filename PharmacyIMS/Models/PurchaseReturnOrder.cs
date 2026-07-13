using PharmacyIMS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Models
{
    /// <summary>
    /// 采购退货单
    /// </summary>
    public class PurchaseReturnOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string OrderNo { get; set; } = string.Empty;

        public DateTime ReturnDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Completed;

        [MaxLength(50)]
        public string? OperatorName { get; set; }

        [MaxLength(500)]
        public string? Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        // 关联原采购单
        public int? PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder? PurchaseOrder { get; set; }

        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }

        public ICollection<PurchaseReturnOrderDetail> Details { get; set; } = new List<PurchaseReturnOrderDetail>();
    }
}
