using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace UDPServer
{
    public partial class UDPServer : Form
    {
        delegate void updateMessages(string data);

        public UDPServer()
        {
            InitializeComponent();
        }

        private void StartServer_Click(object sender, EventArgs e)
        {
            ThreadStart thStart = new ThreadStart(ServerThread);
            Thread thUDPServer = new Thread(thStart);
            thUDPServer.Start();

        }
        private void ServerThread()
        {
            UdpClient udpClient = new UdpClient(8080);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receivedBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receivedBytes);
                UpdateListMessages(returnData);
            }
        }
        private void UpdateListMessages(string data)
        {
            if (this.InvokeRequired)
            {
                updateMessages delUpdate = new updateMessages(UpdateListMessages);
                this.Invoke(delUpdate, new object[] { data });
            }
            else
            {
                UDPClientMessages.Items.Add(data);
                UDPClientMessages.SelectedIndex = UDPClientMessages.Items.Count - 1;
            }
        }
    }
}
