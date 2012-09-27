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


namespace TcpClientEcho
{
    public partial class TcpClientForm : Form
    {
        TcpClient client = null;
        NetworkStream netStream;

        public TcpClientForm()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            int PortNumber = 0;
            PortNumber = Int32.Parse( portNumber.Text );

            try
            {
                client = new TcpClient(serverIpAddress.Text,
                                        PortNumber);
                netStream = client.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] serverBuffer = Encoding.ASCII.GetBytes(
                                        MessageText.Text);
                netStream.Write(serverBuffer, 0, serverBuffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            netStream.Close();
            client.Close();
           
            this.Close();
        }
    }
}
