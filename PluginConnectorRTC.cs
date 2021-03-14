using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBEVMDGEN;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using XBEVMDGEN.UI;
using RTCV.Common;
using System.Windows.Forms;

namespace XBEVMDGEN
{
    /// <summary>
    /// This lies on the RTC(Server) side
    /// </summary>
    class PluginConnectorRTC : IRoutable
    {
        public PluginConnectorRTC()
        {
            LocalNetCoreRouter.registerEndpoint(this, Endpoint.RTC_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;
            switch (message.Type)
            {
                case Commands.MAKEAVMD:
                    {
                        //MessageBox.Show($"Making a VMD named \"{((VmdPrototype)message.objectValue).VmdName}\".");
                        RTCV.CorruptCore.MemoryDomains.AddVMD(((VmdPrototype) message.objectValue).Generate());
                        S.GET<VmdPoolForm>().RefreshVMDs();
                        S.GET<MemoryDomainsForm>().RefreshDomains();
                        break;
                    }
            }
            return e.returnMessage;
        }
    }
}
