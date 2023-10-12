using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroGui.Model
{
    public class AppConfigModel : ChangeAwareObject
    {
        public bool AlwaysOnTop
        {
            get => _alwaysOnTop;
            set => SetAndRaise(ref _alwaysOnTop, value);
        }
        private bool _alwaysOnTop = false;

        public int DelaySecondsBeforeStart
        {
            get => _delaySecondsBeforeStart;
            set => SetAndRaise(ref _delaySecondsBeforeStart, value);
        }
        private int _delaySecondsBeforeStart = 0;
    }
}
