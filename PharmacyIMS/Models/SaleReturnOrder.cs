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
    /// 销售退货单
    /// </summary>
    public class SaleReturnOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string OrderNo { get; set; } = string.Empty;

        public DateTime ReturnDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CustomerName { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Completed;

        [MaxLength(50)]
        public string? OperatorName { get; set; }

        [MaxLength(500)]
        public string? Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        // 关联原销售单
        public int? SaleOrderId { get; set; }

        [ForeignKey("SaleOrderId")]
        public SaleOrder? SaleOrder { get; set; }

        public ICollection<SaleReturnOrderDetail> Details { get; set; } = new List<SaleReturnOrderDetail>();
    }
}
