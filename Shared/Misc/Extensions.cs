using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Misc
{
    public static class Extensions
    {

    }

    /// <summary>
    /// "Extension class" for static Console class https://stackoverflow.com/a/309414/8105758
    /// </summary>
    public static class ConsoleExt
    {
        public static void WriteError(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
