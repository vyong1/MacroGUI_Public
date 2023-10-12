using Shared;
using Shared.Math;
using Shared.Misc;
using Shared.PeripheralInput;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Shared.Macros
{
    public abstract class Macro
    {
        public abstract void Execute();

        public virtual string Name => StringUtils.SplitCamelCase(GetType().Name);
        public abstract MacroGroup Group { get; }

        #region utility methods
        protected static void Key(string keys) => Keyboard.Type(keys);
        protected static void KeyWaitMs(string keys, int lowMs, int highMs)
        {
            Key(keys);
            WaitMsRandom(lowMs, highMs);
        }
        protected static void KeyWaitS(string keys, int lowS, int highS)
        {
            Key(keys);
            WaitSRandom(lowS, highS);
        }
        protected static void Click() => Mouse.LeftClick(RNG.Range(70, 100));
        protected static void MoveMouseTo(int x, int y) => Mouse.SetCursorPos(x, y);
        protected static void WaitS(int seconds) => Thread.Sleep(seconds * 1000);
        protected static void WaitMs(int ms) => Thread.Sleep(ms);
        protected static void WaitSRandom(int low, int high, bool discrete = false)
        {
            if (discrete)
            {
                WaitS(RNG.Range(low, high));
            }
            else
            {
                WaitMs(RNG.Range(low * 1000, high * 1000));
            }
        }
        protected static void WaitMsRandom(int low, int high) => Thread.Sleep(RNG.Range(low, high));
        #endregion
    }
}
