using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Models
{
    /// <summary>
    /// 供应商实体类
    /// </summary>
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SupplierName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactPerson { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? LicenseNo { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 该供应商的采购记录
        /// </summary>
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }
}
