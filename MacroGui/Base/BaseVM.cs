using MacroGui.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroGui
{
    internal abstract class BaseVM : ChangeAwareObject
    {
        /// <summary>
        /// Registers the property changed from an <see cref="INotifyPropertyChanged"/>
        /// to bubble up to the VM as well.
        /// Use dumb property name handling for now, assume that we want to update every property.
        /// </summary>
        /// <param name="notifies"></param>
        protected void RegisterPropChangedBubbleUp(INotifyPropertyChanged notifies)
        {
            notifies.PropertyChanged += (sender, prop) => RaisePropertyChanged(null);
        }

        protected static ActionCommand AsCommand(Action act, Func<bool> canExecute = null) =>
            new ActionCommand(act, canExecute);
    }
}
