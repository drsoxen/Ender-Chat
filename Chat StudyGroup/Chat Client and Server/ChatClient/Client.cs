using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ChatClient
{
    public partial class Client : Form
    {
        string IP;
        string Port;

        bool Connected;

        Thread m_SendThread;

        TcpClient m_Client;
        NetworkStream m_NetStream;

        List<string> m_SendQueue = new List<string>();

        public Client()
        {
            InitializeComponent();

            m_SendThread = new Thread(new ThreadStart(SendMessage));
            m_SendThread.IsBackground = true;
            m_SendThread.Start();

            Connected = false;

            IP = GetIP();
            textBox_IP.Text = IP;

            Port = "3000";
            textBox_Port.Text = Port;
        }

        public string GetIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipa in host.AddressList)
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa.ToString();
            return "UNKNOWN_IP";
        }

        void SendMessage()
        {
            while (true)
            {
                if (Connected)
                {
                    if (m_SendQueue.Count > 0)
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(m_SendQueue[0]);
                        m_NetStream.Write(buffer, 0, buffer.Length);
                        m_SendQueue.RemoveAt(0);
                    }
                }
            }
        }

        private void textBox_IP_TextChanged(object sender, EventArgs e)
        {
            IP = textBox_IP.Text;
        }

        private void textBox_Port_TextChanged(object sender, EventArgs e)
        {
            Port = textBox_Port.Text;
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            m_Client = new TcpClient(IP, int.Parse(Port));
            m_NetStream = m_Client.GetStream();
            Connected = true;
        }

        private void button_Disconnect_Click(object sender, EventArgs e)
        {
            m_NetStream.Close();
            m_Client.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_SendQueue.Add(textBox_Message.Text);
        }

        private void textBox_Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }
    }
}
