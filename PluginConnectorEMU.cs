using XBEVMDGEN.UI;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using RTCV.UI;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace XBEVMDGEN
{
    /// <summary>
    /// This lies on the Emulator(Client) side
    /// </summary>
    internal class PluginConnectorEMU : IRoutable
    {
        
        public PluginConnectorEMU()
        {
            LocalNetCoreRouter.registerEndpoint(this, Endpoint.EMU_SIDE);
        }
        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;

            switch (message.Type)
            {
                case Commands.SHOW_WINDOW:
                    try
                    {
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            if (((Control)S.GET<PluginForm>()).IsDisposed)
                            {
                                S.SET<PluginForm>(new PluginForm());
                            }
                            ((Control)S.GET<PluginForm>()).Show();
                            ((Form)S.GET<PluginForm>()).Activate();
                        });
                        break;
                    }
                    catch
                    {
                        Logging.GlobalLogger.Error($"Template command {Commands.SHOW_WINDOW} failed. Reason:\r\n" + e.ToString());
                        break;
                    }
            }
            return e.returnMessage;
        }
    }
}
