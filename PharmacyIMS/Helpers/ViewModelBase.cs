using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyIMS.Helpers
{
    /// <summary>
    /// 所有 ViewModel 的基类。
    /// 实现 INotifyPropertyChanged 接口，为派生类提供"属性变更通知"能力，
    /// 这样 View 上的绑定才能在数据变化时自动刷新。
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性变更事件。WPF 的绑定系统会自动订阅这个事件，
        /// 一旦触发，界面上对应的绑定就会重新读取属性值。
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 触发属性变更通知。
        /// [CallerMemberName] 让编译器自动把"调用者属性名"填进来，
        /// 因此在属性里调用时不用手写属性名字符串。
        /// </summary>
        /// <param name="propertyName">属性名（由编译器自动填入，无需手动传）</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性的"标准写法"：值真正发生变化时才赋值并通知，
        /// 值没变就什么都不做（避免不必要的界面刷新）。
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="field">后台字段（用 ref 传引用，方法内部直接修改它）</param>
        /// <param name="value">要设置的新值</param>
        /// <param name="propertyName">属性名（编译器自动填入）</param>
        /// <returns>值发生了变化返回 true，否则返回 false</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
