using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class AutoClickMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.Standard;

        public int MsBetweenClicks { get; set; }
        public AutoClickMacro(int msBetweenClicks = 1000)
        {
            MsBetweenClicks = msBetweenClicks;
        }

        public override void Execute()
        {
            Click();
            WaitMs(MsBetweenClicks);
        }
    }
}
