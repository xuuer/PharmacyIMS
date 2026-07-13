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
    /// 采购入库单
    /// </summary>
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string OrderNo { get; set; } = string.Empty;

        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 采购总金额
        /// </summary>
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.Completed;

        /// <summary>
        /// 操作人
        /// </summary>
        [MaxLength(50)]
        public string? OperatorName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string? Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        // ====== 外键关系 ======
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }

        /// <summary>
        /// 采购明细
        /// </summary>
        public ICollection<PurchaseOrderDetail> Details { get; set; } = new List<PurchaseOrderDetail>();
    }
}
