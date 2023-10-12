using Shared.FFXIV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class LogInMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.FFXIV;

        public override void Execute()
        {
            KeyWaitMs(XivInputs.Confirm, 1000, 1400);
            KeyWaitS(XivInputs.Confirm, 10, 15);
            KeyWaitMs(XivInputs.Confirm, 1000, 1400);
            Key(XivInputs.Confirm);
        }
    }
}
