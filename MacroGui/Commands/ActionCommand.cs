using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MacroGui.Commands
{
    /// <summary>
    /// Simple generic implementation of ICommand
    /// </summary>
    internal class ActionCommand : ICommand
    {
        #region public
        public event EventHandler CanExecuteChanged;

        public ActionCommand(Action action, Func<bool> canExecute = null) 
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object _) => canExecute?.Invoke() ?? true;
        
        public void Execute(object _) => action?.Invoke();
        
        #endregion

        #region private
        private Action action;
        private Func<bool> canExecute;
        #endregion
    }
}
