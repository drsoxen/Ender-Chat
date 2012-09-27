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
using System.Threading;
using NetZ;
namespace EnderChat
{
    /// <summary>
    /// Interaction logic for TCPServer.xaml
    /// </summary>
    public partial class TCPServerGUI : Window
    {
        public TCPServer m_server;
        Thread m_Prosessor;
        List<string> m_ChatLog = new List<string>();
        delegate void SetTextCallback();
        public TCPServerGUI()
        {
            InitializeComponent();
            this.Show();
            m_server = new TCPServer();
            m_server.Start();

            m_Prosessor = new Thread(new ThreadStart(Prosessor));
            m_Prosessor.IsBackground = true;
            m_Prosessor.Start();

        }

        public void Prosessor()
        {
            
            while (true)
            {
                if (m_server != null)
                {
                    if (m_server.m_ReceiveQueue.Count > 0)
                    {
                        m_ChatLog.Add(m_server.m_ReceiveQueue[0]);
                        UpdateMessageBox();

                        m_server.m_ReceiveQueue.RemoveAt(0);
                    }
                }

            }
        }

        public void UpdateMessageBox()
        {
            if (this.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                SetTextCallback d = new SetTextCallback(UpdateMessageBox);
                this.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                //this.Activate();

                textBox_Main.Text += m_ChatLog[m_ChatLog.Count - 1];
                textBox_Main.Text += "\n";
                textBox_Main.ScrollToEnd();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_server.Stop();
            m_server = null;
        }
    }
}
