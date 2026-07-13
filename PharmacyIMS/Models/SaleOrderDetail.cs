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
    /// 销售出库明细
    /// </summary>
    public class SaleOrderDetail
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 小计金额
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        // ====== 外键关系 ======
        public int SaleOrderId { get; set; }

        [ForeignKey("SaleOrderId")]
        public SaleOrder? SaleOrder { get; set; }

        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }
    }
}
