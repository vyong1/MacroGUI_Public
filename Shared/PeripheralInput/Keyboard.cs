using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Shared.PeripheralInput
{
    public class Keyboard
    {
        static Keyboard()
        {
            // Add Numpad support to SendKeys
            // https://stackoverflow.com/questions/44952693/how-do-you-send-numpad-keys-using-sendkeys
            FieldInfo info = typeof(SendKeys).GetField("keywords", BindingFlags.Static | BindingFlags.NonPublic);
            Array oldKeys = (Array)info.GetValue(null);
            Type elementType = oldKeys.GetType().GetElementType();
            Array newKeys = Array.CreateInstance(elementType, oldKeys.Length + 10);
            Array.Copy(oldKeys, newKeys, oldKeys.Length);
            for (int i = 0; i < 10; i++)
            {
                var newItem = Activator.CreateInstance(elementType, "NUM" + i, (int)Keys.NumPad0 + i);
                newKeys.SetValue(newItem, oldKeys.Length + i);
            }
            info.SetValue(null, newKeys);
        }

        public static void Type(string key)
        {
            SendKeys.SendWait(key);
        }
    }
}
