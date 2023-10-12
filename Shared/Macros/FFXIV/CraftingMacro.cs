using Shared;
using Shared.FFXIV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    /// <summary>
    /// Use for crafts that only take 1 button to finish
    /// </summary>
    public class CraftingMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.FFXIV;

        public int CraftTime { get; set; }
        private string _MacroKey;

        public CraftingMacro(string macroKey = "B", int macroRuntime = 40)
        {
            CraftTime = macroRuntime;
            _MacroKey = macroKey;
        }

        public override void Execute()
        {
            KeyWaitS(_MacroKey, CraftTime + 2, CraftTime + 4);
            KeyWaitS(XivInputs.Confirm, 1, 2);
            KeyWaitS(XivInputs.Confirm, 1, 2);
            KeyWaitS(XivInputs.Confirm, 2, 3);
        }
    }
}
