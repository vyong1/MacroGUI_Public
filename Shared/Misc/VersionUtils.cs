using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Misc
{
    public static class VersionUtils
    {
        public static string GetVersion()
        {
            Version version = Assembly.GetCallingAssembly().GetName().Version;
            return $"{version}";
        }
    }
}
