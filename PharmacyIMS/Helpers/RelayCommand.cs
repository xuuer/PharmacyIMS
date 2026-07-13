using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PharmacyIMS.Helpers
{
    /// <summary>
    /// 通用命令类，实现 ICommand 接口。
    /// 作用：把一个"普通方法"包装成可以绑定到按钮 Command 属性的对象，
    /// 这样点击按钮就能执行 ViewModel 里的方法，而不用写后台事件。
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="execute">点击时执行的方法（必填）</param>
        /// <param name="canExecute">判断是否可执行的方法（可选，不传则永远可执行）</param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// 手动触发 CanExecute 重新评估，让按钮状态立即刷新
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
