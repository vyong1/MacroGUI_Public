using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MacroGui
{
    public abstract class ChangeAwareObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Marshall property changes to the UI thread so we're always in sync with
            // changes to the model
            Application.Current.Dispatcher.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        public void SetAndRaise<T>(ref T item, T value, Action before = null, Action after = null, [CallerMemberName] string propname = "")
        {
            before?.Invoke();
            item = value;
            RaisePropertyChanged(propname);
            after?.Invoke();
        }
    }
}
