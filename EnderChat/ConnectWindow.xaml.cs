using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using NetZ;

namespace EnderChat
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        string m_Port;
        string m_MultiCastEndpoint;
        string m_UserName;
        Window m_Parent;

        TCPServerGUI m_server;

        bool m_MulticastClient;
        bool m_TCPClient;

        public ConnectWindow(Window parent)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            m_Parent = parent;
            textBox_UserName.Text = "Cool Dude";
            textBox_Endpoint.Text = "224.100.0.1";
            textBox_Port.Text = "1337";
            m_MulticastClient = false;
            m_TCPClient = false;
            button_Connect.IsEnabled = false;
            button_StartServer.IsEnabled = false;
        }

        private void button_Connect_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_UserName.Text.Length > 0 && textBox_Port.Text.Length > 0 && textBox_Endpoint.Text.Length > 0)
            {
                m_UserName = textBox_UserName.Text;
                m_MultiCastEndpoint = textBox_Endpoint.Text;
                m_Port = textBox_Port.Text;


                if (m_Parent is MainWindow)
                {
                    ((MainWindow)m_Parent).m_Connected = true;
                    ((MainWindow)m_Parent).m_Port = m_Port;
                    ((MainWindow)m_Parent).m_MultiCastEndpoint = m_MultiCastEndpoint;
                    ((MainWindow)m_Parent).m_UserName = m_UserName;
                }

                
                if (m_MulticastClient)
                    ((MainWindow)m_Parent).StartSendingAndReceiving(ClientType.MultiCast);
                else if (m_TCPClient)
                    ((MainWindow)m_Parent).StartSendingAndReceiving(ClientType.TCP);

                this.Close();
            }
            else
            {
                MessageBox.Show("You need to fill out a username");
            }
        }

        private void textBox_Port_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                button_Connect_Click(sender, e);
        }

        private void button_StartServer_Click(object sender, RoutedEventArgs e)
        {

                m_server = new TCPServerGUI();
                m_server.Closed += new EventHandler(GetServerButtonStatus);
                button_StartServer.IsEnabled = false;
            
        }

        private void Radio_Multicast_Checked(object sender, RoutedEventArgs e)
        {
            m_TCPClient = false;
            m_MulticastClient = true;

            button_Connect.IsEnabled = true;    

            
            GetServerButtonStatus(sender, e);

            
            textBox_Endpoint.Clear();
            textBox_Endpoint.Text = "224.100.0.1";
            textBox_Port.Clear();
            textBox_Port.Text = "1337";
        }

        private void radioButtonRadio_Radio_TCP_Checked(object sender, RoutedEventArgs e)
        {
            m_MulticastClient = false;
            m_TCPClient = true;

            button_Connect.IsEnabled = true;

  
            GetServerButtonStatus(sender,e);


            textBox_Endpoint.Clear();
            textBox_Endpoint.Text = UsefullToolz.GetIP();
            textBox_Port.Clear();
            textBox_Port.Text = "3000";
        }

        private void GetServerButtonStatus(object sender, EventArgs e)
        {
            if (m_MulticastClient == true)
            {
                button_StartServer.IsEnabled = false;
                return;
            }

            if (m_server == null || m_server.m_server == null)
                button_StartServer.IsEnabled = true;
            else
                button_StartServer.IsEnabled = false;
        }

    }
}
