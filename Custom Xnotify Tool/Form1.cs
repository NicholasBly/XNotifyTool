using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using XDevkit;
using JRPC_Client;

namespace JRPC_Example
{
    public partial class Form1 : Form
    {
        IXboxConsole Console;

        public Form1()
        {
            InitializeComponent();
            button1_Click(null, (EventArgs)null);// connect
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Console.Connect(out Console))
                button1.Text = "Re-Connect";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Title ID is " + Console.XamGetCurrentTitleId().ToString("X"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kernal version is " + Console.GetKernalVersion());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CPU Key is \"" + Console.GetCPUKey() + "\"");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Consoles local IP address is " + Console.XboxIP());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CPU Temperature is " + Console.GetTemperature(JRPC.TemperatureType.CPU) + " degrees F");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("XamGetCurrentTitleId's address is 0x" + Console.ResolveFunction("xam.xex", 463).ToString("X"));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            uint pAddress = Console.ResolveFunction("xam.xex", 2601) + 0x3000;// free xam memory
            int LocalClient = 0;
            if (Console.Call<uint>("xam.xex", 522, LocalClient, 7, pAddress) == 0)// XamUserGetXUID
            {
                // XUID of local client is in pAddress
                MessageBox.Show("XUID of client 0 is " + Console.ReadUInt64(pAddress).ToString("X16"));
            }
            else
                MessageBox.Show("Error!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Console.XNotify(textBox1.Text);
            //here is an RPC example:
            Console.CallVoid(JRPC.ThreadType.Title, "xam.xex", 656, 34, 0xFF, 2, textBox1.Text.ToWCHAR(), 1);
        }
    }
}
