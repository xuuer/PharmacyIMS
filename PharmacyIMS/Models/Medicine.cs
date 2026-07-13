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
    /// 药品实体类 - 药品台账
    /// </summary>
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string MedicineCode { get; set; } = string.Empty;

        /// <summary>
        /// 药品名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string MedicineName { get; set; } = string.Empty;

        /// <summary>
        /// 通用名
        /// </summary>
        [MaxLength(100)]
        public string? GenericName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [MaxLength(50)]
        public string? Specification { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        [MaxLength(30)]
        public string? DosageForm { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        [MaxLength(100)]
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 批准文号
        /// </summary>
        [MaxLength(50)]
        public string? ApprovalNo { get; set; }

        /// <summary>
        /// 处方类型
        /// </summary>
        public PrescriptionType PrescriptionType { get; set; }

        /// <summary>
        /// 进货价
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 当前库存数量
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 库存预警下限
        /// </summary>
        public int StockAlertLevel { get; set; } = 10;

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string? Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        // ====== 计算属性（非数据库字段） ======
        [NotMapped]
        public bool IsLowStock => StockQuantity <= StockAlertLevel;

        [NotMapped]
        public bool IsNearExpiry => ExpiryDate.HasValue && ExpiryDate.Value <= DateTime.Now.AddMonths(3);

        // ====== 外键关系 ======
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public MedicineCategory? Category { get; set; }
    }
}
