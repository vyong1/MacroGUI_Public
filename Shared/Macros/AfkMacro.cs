using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class AfkMacro : Macro
    {
        public override string Name => "Anti-AFK";

        public override MacroGroup Group => MacroGroup.Standard;

        public override void Execute()
        {
            MoveMouseTo(700, 500);
            Click();
            WaitSRandom(5, 10);
            

            MoveMouseTo(700, 700);
            Click();
            WaitSRandom(5, 10);
        }
    }
}
