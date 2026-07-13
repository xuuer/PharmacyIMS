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
    /// 采购退货明细
    /// </summary>
    public class PurchaseReturnOrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        public int PurchaseReturnOrderId { get; set; }

        [ForeignKey("PurchaseReturnOrderId")]
        public PurchaseReturnOrder? PurchaseReturnOrder { get; set; }

        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }
    }
}
