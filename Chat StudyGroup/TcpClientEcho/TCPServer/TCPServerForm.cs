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
using ThreadFarm;

namespace TCPServer
{
    public partial class TCPServerForm : Form
    {
        private const int BUFFSIZE = 32;
        private delegate void UpdateConnectionsListDelegate(string str);
        TcpListener listener = null;
        ThreadObject thObject = null;

        public TCPServerForm()
        {
            InitializeComponent();
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            int PortNumber = 8080;
            
            try
            {
                listener = new TcpListener(IPAddress.Any, PortNumber);
                listener.Start();

                thObject = ThreadMaster.Instance.GetNewThread("ListenerThread");
                thObject.ThreadTimeout = 0;
                thObject.threadDelegateObject = new ThreadObject.ThreadDelegate(
                            ThreadListener );
                thObject.Start( );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }

        }

        private void ThreadListener( ThreadObject thObj )
        {
            try
            {
                TcpClient tcpClient = null;
                NetworkStream netStream = null;


                tcpClient = listener.AcceptTcpClient();
                netStream = tcpClient.GetStream();
                UpdateConnectionList( 
                    tcpClient.Client.RemoteEndPoint.ToString() + 
                    " Logging in...");
                //Reading From Client
                byte[] rcvBuffer = new byte[BUFFSIZE];
                int bytesReceived = 0;

                while ((bytesReceived = netStream.Read(
                            rcvBuffer, 0, rcvBuffer.Length)) > 0)
                {
                    UpdateConnectionList(
                        tcpClient.Client.RemoteEndPoint.ToString() +
                        Encoding.ASCII.GetString(rcvBuffer, 0, bytesReceived));

                }
                            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateConnectionList(string text)
        {
            if (this.InvokeRequired)
            {
                UpdateConnectionsListDelegate updateDel = new
                    UpdateConnectionsListDelegate(UpdateConnectionList);

                this.Invoke(updateDel, new object[] { text });
            }
            else
            {
                connectionList.Items.Add(text);
            }
        }

        private void StopServerButton_Click(object sender, EventArgs e)
        {
            thObject.Stop();
            listener.Stop();
            this.Close();
        }
    }
}
