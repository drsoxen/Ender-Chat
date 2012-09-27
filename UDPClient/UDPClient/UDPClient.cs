using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace UDPClient
{
    public partial class UDPClient : Form
    {
        public UDPClient()
        {
            InitializeComponent();
        }

        private void SendDataButton_Click(object sender, EventArgs e)
        {
            if (RemoteIP.Text.ToString() != string.Empty)
            {
                UdpClient udpClient = new UdpClient();
                udpClient.Connect(RemoteIP.Text.ToString(), 8080);
                Byte[] sendBytes = Encoding.ASCII.GetBytes("HelloWorld"); //will return hello world in byte format
                udpClient.Send(sendBytes, sendBytes.Length);
            }
            else
            {
                MessageBox.Show("Please enter an IP Address to connect to ");
            }
        }

  
    }
}
