using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class DebugMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.Debug;

        public override void Execute()
        {
            Write();
            iterations++;
            WaitS(1);
        }

        protected int iterations = 0;
        protected virtual void Write() => Debug.WriteLine($"[1] Iterations: {iterations}");
    }

    public class DebugMacro2 : DebugMacro
    {
        protected override void Write() => Debug.WriteLine($"[2] Iterations: {iterations}");
    }
}
