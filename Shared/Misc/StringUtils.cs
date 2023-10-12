using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Misc
{
    public class StringUtils
    {
        public static string SplitCamelCase(string input)
        {
            // Taken from https://stackoverflow.com/a/6137889/8105758
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        public static string AlphabetizeLines(string input)
        {
            var split = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(Environment.NewLine, split.OrderBy(s => s).ToList());
        }
    }
}
