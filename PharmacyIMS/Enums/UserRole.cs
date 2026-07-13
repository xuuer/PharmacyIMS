using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Enums
{
    /// <summary>
    /// 用户角色枚举
    /// </summary>
    public enum UserRole
    {
        /// <summary>系统管理员 - 最高权限</summary>
        Admin = 0,

        /// <summary>店长 - 管理门店日常运营</summary>
        Manager = 1,

        /// <summary>采购员 - 负责药品采购入库</summary>
        Purchaser = 2,

        /// <summary>销售员 - 负责药品销售出库</summary>
        Salesperson = 3
    }
}
