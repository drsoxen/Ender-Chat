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
using System.Threading;
namespace Server
{
    public partial class Form1 : Form
    {
        Thread m_recieveThread;
        Thread m_SendThread;
        Thread m_ListenThread;

        TcpListener m_Listener;
        TcpClient m_Client;

        public List<string> m_ReceiveQueue = new List<string>();

        bool m_ReadyToReceive;
        bool m_Listen;
        bool m_Receive;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            int PortNumber = 3000;

            m_ReadyToReceive = false;

            m_recieveThread = new Thread(new ThreadStart(ReceiveMessage));
            m_recieveThread.IsBackground = true;
            m_recieveThread.Start();

            m_Listener = new TcpListener(IPAddress.Any, PortNumber);

            m_ListenThread = new Thread(new ThreadStart(ListenForClients));
            m_ListenThread.IsBackground = true;
            m_ListenThread.Start();
        }

        void ListenForClients()
        {
            m_Listen = true;

            m_Listener.Start();

            while (m_Listen)
            {
                m_Client = m_Listener.AcceptTcpClient();
                Thread Communication = new Thread(new ParameterizedThreadStart(HandleCommunications));
                Communication.IsBackground = true;
                Communication.Start(m_Client);
            }

            m_Listener.Stop();
        }


        public void HandleCommunications(object client)
        {
            TcpClient tcpClient = client as TcpClient;
            NetworkStream stream = tcpClient.GetStream();

            byte[] rcvBuffer = new byte[1024];
            int bytesReceived = 0;


            while ((bytesReceived = stream.Read(rcvBuffer, 0, rcvBuffer.Length)) > 0)
            {
                UpdateMessageBox(tcpClient.Client.RemoteEndPoint.ToString() + " " + Encoding.ASCII.GetString(rcvBuffer, 0, bytesReceived));
            }

        }

        delegate void SetTextCallback(string toUpdate);
        public void UpdateMessageBox(string toUpdate)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateMessageBox);
                this.Invoke(d, new object[] { toUpdate  });
            }
            else
            {
                richTextBox1.Text += toUpdate;
                richTextBox1.Text += "\n";
            }
        }

        public void ReceiveMessage()
        {
            m_Receive = true;
            m_ReadyToReceive = true;

            while (m_Receive)
            {
                try
                {

                }
                catch (System.Exception ex)
                {

                }

            }
        }

    }
}
