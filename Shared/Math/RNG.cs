using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Math
{
    public static class RNG
    {
        private static Random rand;

        static RNG()
        {
            rand = new Random();
        }

        /// <summary>
        /// Between inclusive
        /// </summary>
        public static int Range(int low, int high)
        {
            return rand.Next(low, high + 1);
        }
    }
}
