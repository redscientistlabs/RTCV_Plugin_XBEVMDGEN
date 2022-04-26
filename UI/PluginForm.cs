using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Components.Controls;
using RTCV.UI.Modular;

namespace XBEVMDGEN.UI
{
    public partial class PluginForm : Form
    {
        public MemoryInterface[] miarray;
        int magicnumber = 0x48454258;
        //public MemoryInterface xboxsdram;
        public static MemoryDomainProxy xboxsdram = MemoryDomains.GetProxy("Virtual Memory", 0);
        public static MemoryInterface mi = MemoryDomains.MemoryInterfaces["Virtual Memory"];
        static string vmdnameText = "";
        static string gamename = "";
        static long xbestartaddress = 0x10000;
        public PluginForm()
        {
            InitializeComponent();
            xboxsdram = MemoryDomains.GetProxy("Virtual Memory", 0);
            mi = MemoryDomains.MemoryInterfaces["Virtual Memory"];
            //xboxsdram = MemoryDomains.MemoryInterfaces["Virtual Memory"];
            //if(MemoryDomains.GetProxy("Virtual Memory", 0).MD == null)
            //{
            //    MemoryDomains.GetProxy("Virtual Memory",0).MD = MemoryDomains.
            //}
            //PluginConnectorEMU.RefreshDomains();

        }

        private void btnMakeVMD_Click(object sender, EventArgs e)
        {

            RefreshDomains();
            bool xbeisatexpectedaddress = false;
            //string mbox = $"Memory Interface is {xboxsdram.ToString()}. \nIts name is {xboxsdram.Name}.\nMD is{MemoryDomains.GetProxy("Virtual Memory", 0).ToString()}";
            //MessageBox.Show(mbox);
            //MessageBox.Show($"Value at {xbestartaddress} is {BitConverter.ToString(xboxsdram.PeekBytes(xbestartaddress, 4))}\n");
            //if (xboxsdram.PeekBytes(xbestartaddress, 4) == magicnumber)
            //{
            //    xbeisatexpectedaddress = true;
            //    GenerateVMDS(xbestartaddress);
            //}
            /*else*//* if (xbeisatexpectedaddress == false)*/
            //{
            int i = 0;
            while (i < xboxsdram.Size)
            {
                if (xboxsdram.PeekByte(i) == 0x58)
                {
                    if (xboxsdram.PeekByte(i + 1) == 0x42)
                    {
                        if(xboxsdram.PeekByte(i+2) == 0x45)
                        {
                            if(xboxsdram.PeekByte(i+3) == 0x48)
                            {
                                //MessageBox.Show("Found an XBE!");
                                GenerateVMDS(i);
                            }
                        }
                    }
                }
                i++;
            }
            //}
        }
        public static void RefreshDomains()
        {
            xboxsdram = MemoryDomains.GetProxy("Virtual Memory", 0);
            mi = MemoryDomains.MemoryInterfaces["Virtual Memory"];
        }
        private static void GenerateVMDS(long xbestart)
        {
            int headerlength = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + 0x0108, xbestart + 0x0108+0x4, true), 0);
            if(headerlength > 4*4096)
            {
                return;
            }
            int xbesize = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + 0x010C, xbestart + 0x010C+0x4, true), 0);
            int numberofsections = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + 0x011C, xbestart + 0x011C+0x4, true), 0);
            int sectionheadersaddress = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + 0x0120, xbestart + 0x0120+ 0x4, true), 0) - 0x10000;
            int currentsectionsize = 0;
            int currentsectionaddress = 0x0;
            int currentsection = 0;
            int currentsectionnameaddress = 0x0;
            int currentsectionendaddr = 0x0;
            string currentsectionname = "";
            int certificateaddress = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + 0x0118, xbestart + 0x0118+0x4, true), 0) - 0x10000;
            gamename = System.Text.Encoding.ASCII.GetString(xboxsdram.PeekBytes(xbestart + certificateaddress + 0xC, xbestart + certificateaddress + 0xC+0x50, true)).Replace("\0", "");
            gamename = gamename.Trim().Replace(" ", "").Replace("-", "").Replace(":","").Substring(0, 9); //trim down the game name to just 9 characters and remove seperators
            int addresstoread = sectionheadersaddress;
            string firstsectionname = "";
            int firstsectionnameaddress = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + sectionheadersaddress + 0x14, xbestart + sectionheadersaddress + 0x14 + 0x4, true), 0) - 0x10000;
            firstsectionname = System.Text.Encoding.ASCII.GetString(xboxsdram.PeekBytes(xbestart + firstsectionnameaddress, xbestart + firstsectionnameaddress + 10, true));
            int i = 0;
            while (i < numberofsections)
            {
                currentsection = i;
                currentsectionnameaddress = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + addresstoread + 0x14, xbestart + addresstoread + 0x14+0x4, true), 0) - 0x10000;
                currentsectionname = System.Text.Encoding.ASCII.GetString(xboxsdram.PeekBytes(xbestart + currentsectionnameaddress, xbestart + currentsectionnameaddress +10, true));

                if (!firstsectionname.StartsWith(".text"))
                {
                    currentsectionnameaddress = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + addresstoread + 0x14, xbestart + addresstoread + 0x14 + 0x4, true), 0);
                    currentsectionname = System.Text.Encoding.ASCII.GetString(xboxsdram.PeekBytes(currentsectionnameaddress, currentsectionnameaddress + 10, true));
                }
                currentsectionname = currentsectionname.Trim().Replace("\t", " ").Replace("\0", "").Replace(" ", "").Replace("-", "").Replace(":", "");
                currentsectionaddress = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + addresstoread + 0x4, xbestart + addresstoread + 0x4+0x4, true), 0);
                currentsectionsize = BitConverter.ToInt32(xboxsdram.PeekBytes(xbestart + addresstoread + 0x8, xbestart + addresstoread + 0x8+0x4, true), 0);
                currentsectionendaddr = currentsectionaddress + currentsectionsize;
                vmdnameText = gamename + " ~ " + currentsectionname + " (0x" + BitConverter.ToString(BitConverter.GetBytes(currentsectionaddress).Reverse().ToArray()).Replace("-", "") + ")";
                long[] range = new long[2];
                range[0] = currentsectionaddress;
                range[1] = currentsectionendaddr;
                if (currentsectionaddress >= currentsectionendaddr)
                {
                    return;
                }
                List<long[]> ranges = new List<long[]>();
                ranges.Add(range);
                VmdPrototype vmdPrototype = new VmdPrototype();
                vmdPrototype.GenDomain = "Virtual Memory";
                vmdPrototype.BigEndian = mi.BigEndian;
                vmdPrototype.AddRanges = ranges;
                vmdPrototype.WordSize = mi.WordSize;
                vmdPrototype.VmdName = vmdnameText;
                vmdPrototype.PointerSpacer = 1;
                if (currentsectionaddress != 0) LocalNetCoreRouter.Route(Endpoint.RTC_SIDE, Commands.MAKEAVMD, (object)vmdPrototype, true);
                /*RTCV.CorruptCore.MemoryDomains.AddVMD(new VmdPrototype()
                {
                    AddRanges = ranges,
                    GenDomain = "Virtual Memory",
                    VmdName = vmdnameText,
                    BigEndian = mi.BigEndian,
                    WordSize = mi.WordSize,
                    //PointerSpacer = 1,
                }.Generate());*/
                addresstoread += (0x0014 + 0x0024);
                //S.GET<VmdPoolForm>().RefreshVMDs();
                //S.GET<MemoryDomainsForm>().RefreshDomains();
                i++;
            }
        }
        public static void VMDButtonClicked()
        {
        }

        private void cbMemoryDomain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
