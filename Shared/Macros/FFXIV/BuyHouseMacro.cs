using Shared;
using Shared.FFXIV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class BuyHouseMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.FFXIV;

        public override void Execute()
        {
            Input(XivInputs.Confirm);
            Input(XivInputs.Confirm);
            Input(XivInputs.Confirm);
            Input(XivInputs.Confirm);
            Input(XivInputs.Left);
            Input(XivInputs.Confirm);
        }

        /// <summary>
        /// Shorthand for input, includes factoring the time between menus
        /// </summary>
        private void Input(string key)
        {
            KeyWaitMs(key, 550, 750);
        }
    }
}
