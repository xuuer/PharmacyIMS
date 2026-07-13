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
    /// 销售出库单
    /// </summary>
    public class SaleOrder
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string OrderNo { get; set; } = string.Empty;

        /// <summary>
        /// 销售日期
        /// </summary>
        public DateTime SaleDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 客户姓名
        /// </summary>
        [MaxLength(50)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        [MaxLength(20)]
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// 销售总金额
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

        /// <summary>
        /// 销售明细
        /// </summary>
        public ICollection<SaleOrderDetail> Details { get; set; } = new List<SaleOrderDetail>();
    }
}
