using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Enums
{
    /// <summary>
    /// 药品处方类型枚举
    /// </summary>
    public enum PrescriptionType
    {
        /// <summary>处方药 - 需凭医师处方购买</summary>
        Prescription = 0,

        /// <summary>非处方药 - 可自行购买</summary>
        OTC = 1,

        /// <summary>中药饮片</summary>
        TraditionalChinese = 2,

        /// <summary>保健品</summary>
        HealthProduct = 3
    }
}
