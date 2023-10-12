using Shared;
using Shared.FFXIV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class AfkMacroFFXIV : Macro
    {
        public override string Name => "Anti-AFK (FFXIV)";

        public override MacroGroup Group => MacroGroup.FFXIV;

        public override void Execute()
        {
            Input("W", 10, 30);
            Input("S", 1, 3);
        }

        private void Input(string key, int waitSecLow, int waitSecHigh)
        {
            if (!XivGame.IsRunning) { return; }
            //XivGame.FocusGameWindow(); // TODO - this is buggy for some reason
            KeyWaitS(key, waitSecLow, waitSecHigh);
        }
    }
}
