using Shared.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroGui.Model
{
    internal class MacrosToDisplay
    {
        public static List<Macro> Get(params MacroGroup[] exclude)
        {
            List<Macro> list = new List<Macro>()
            {
                #if DEBUG
                // Debug
                new DebugMacro(),
                new DebugMacro2(),
                #endif
                
                // Standard
                new AfkMacro(),
                new ArcMouseMovementMacro(),
                new AutoClickMacro(),

                // FFXIV
                new AfkMacroFFXIV(),
                new BuyHouseMacro(),
                new ConfirmMacro(),
                new CraftingMacro(),
            };

            // Filter out unwanted groups
            var filtered = list.Where(macro => !exclude.Contains(macro.Group)).ToList();

            return filtered;
        }
    }
}
