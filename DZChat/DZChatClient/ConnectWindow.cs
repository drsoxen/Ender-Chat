using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetZ;
namespace DZChatClient
{
    public partial class ConnectWindow : Form
    {
        string m_Port;
        string m_MultiCastEndpoint;
        string m_UserName;
        Form m_Parent;

        bool m_MulticastClient;
        bool m_TCPClient;

        public ConnectWindow(Form parent)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            m_Parent = parent;
            TextBox_UserName.Text = "Cool Dude";
            TextBox_Endpoint.Text = "224.100.0.1";
            TextBox_Port.Text = "1337";
            m_MulticastClient = false;
            m_TCPClient = false;
            ConnectButton.Enabled = false;
            Button_StartServer.Enabled = false;
        }
        
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (TextBox_UserName.Text.Length > 0 && TextBox_Port.Text.Length > 0 && TextBox_Endpoint.Text.Length > 0)
            {
                m_UserName = TextBox_UserName.Text;
                m_MultiCastEndpoint = TextBox_Endpoint.Text;
                m_Port = TextBox_Port.Text;


                if (m_Parent is MainForm)
                {
                    ((MainForm)m_Parent).m_Connected = true;
                    ((MainForm)m_Parent).m_Port = m_Port;
                    ((MainForm)m_Parent).m_MultiCastEndpoint = m_MultiCastEndpoint;
                    ((MainForm)m_Parent).m_UserName = m_UserName;
                }
                
                this.Close();
                if(m_MulticastClient)
                    ((MainForm)m_Parent).StartSendingAndReceiving(ClientType.MultiCast);
                else if(m_TCPClient)
                    ((MainForm)m_Parent).StartSendingAndReceiving(ClientType.TCP);
            }
            else
            {
                MessageBox.Show("You need to fill out a username");
            }
            
       } 

        private void IPandPortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ConnectButton_Click(sender, e);
        }

        private void TextBox_Port_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ConnectButton_Click(sender, e);
        }

        private void Button_StartServer_Click(object sender, EventArgs e)
        {
            if (m_TCPClient)
            {
                //start server
            }
        }

        private void Radio_Multicast_CheckedChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = true;
            m_MulticastClient = !m_MulticastClient;
            TextBox_Endpoint.Clear();
            TextBox_Endpoint.Text = "224.100.0.1";
            TextBox_Port.Clear();
            TextBox_Port.Text = "1337";
        }

        private void Radio_TCP_CheckedChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = true;
            m_TCPClient = !m_TCPClient;
            Button_StartServer.Enabled = !Button_StartServer.Enabled;
            TextBox_Endpoint.Clear();
            TextBox_Endpoint.Text = UsefullToolz.GetIP();
            TextBox_Port.Clear();
            TextBox_Port.Text = "3000";
        }
    }
}
