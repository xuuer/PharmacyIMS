using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Helpers
{
    /// <summary>
    /// 状态下拉框选项辅助类
    /// </summary>
    public class StatusOptions
    {
        public string Text { get; set; } = string.Empty;
        public object? Status { get; set; }
    }
}
