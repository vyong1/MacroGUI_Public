using Shared.FFXIV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class ConfirmMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.FFXIV;

        public int DelayLow { get; set; } = 750;
        public int DelayHigh { get; set; } = 1250;

        public override void Execute()
        {
            KeyWaitMs(XivInputs.Confirm, DelayLow, DelayHigh);
        }
    }
}
