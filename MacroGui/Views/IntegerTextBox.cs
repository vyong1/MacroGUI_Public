

using System.Windows.Controls;
using System.Windows.Input;

namespace MacroGui.Views
{
    internal class IntegerTextBox : TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!Allow(e.Key))
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private bool Allow(Key k)
        {
            switch (k) 
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    return true;
                default:
                    return false;
            }
        }
    }
}
