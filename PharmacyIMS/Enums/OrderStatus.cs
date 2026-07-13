using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Enums
{
    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>待处理</summary>
        Pending = 0,

        /// <summary>已完成</summary>
        Completed = 1,

        /// <summary>已取消</summary>
        Cancelled = 2
    }
}
