using NLog;
using XBEVMDGEN.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace XBEVMDGEN
{
    [Export(typeof(IPlugin))]
    public class Loader : IPlugin, IDisposable
    {
        public static RTCSide CurrentSide = RTCSide.Both;
        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;

        public string Name => "XBEVMDGEN";
        public string Description => "Automatically make VMDs from loaded XBOX executables";

        public string Author => "ChrisNonyminus";

        public Version Version => new Version(1, 0, 0);

        public RTCSide SupportedSide => RTCSide.Both;

        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            Logging.GlobalLogger.Info($"{Name} v{Version} initializing.");
            if (side == RTCSide.Client)
            {
                connectorEMU = new PluginConnectorEMU();
                //S.SET<PluginForm>(new PluginForm());
            }
            if (side == RTCSide.Server)
            {
                //Uncomment if needed
                connectorRTC = new PluginConnectorRTC();
                S.GET<RTCV.UI.OpenToolsForm>().RegisterTool("XBEVMDGEN", "Open XBEVMDGEN", () => { 
                    //This is the method you use to route commands between the RTC side and the Emulator side
                    LocalNetCoreRouter.Route(Endpoint.EMU_SIDE, Commands.SHOW_WINDOW, true); 
                });
            }
            Logging.GlobalLogger.Info($"{Name} v{Version} initialized.");
            CurrentSide = side;
            return true;
        }

        public bool StopPlugin()
        {
            if (Loader.CurrentSide == RTCSide.Client && !S.ISNULL<PluginForm>() && !S.GET<PluginForm>().IsDisposed)
            {
                S.GET<PluginForm>().Close();
            }
            return true;
        }
    }
}
