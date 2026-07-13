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
    /// 销售退货明细
    /// </summary>
    public class SaleReturnOrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        public int SaleReturnOrderId { get; set; }

        [ForeignKey("SaleReturnOrderId")]
        public SaleReturnOrder? SaleReturnOrder { get; set; }

        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }
    }
}
