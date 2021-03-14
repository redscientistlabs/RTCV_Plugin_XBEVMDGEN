using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBEVMDGEN
{
    internal static class Endpoint
    {
        public const string PREFIX = "XBEVMDGEN";
        public const string EMU_SIDE = PREFIX + "EMU";
        public const string RTC_SIDE = PREFIX + "RTC";
    }

    internal static class Commands
    {
        public const string SHOW_WINDOW = Endpoint.PREFIX + nameof(SHOW_WINDOW);
        public const string MAKEAVMD = Endpoint.PREFIX + nameof(MAKEAVMD);
    }
}
