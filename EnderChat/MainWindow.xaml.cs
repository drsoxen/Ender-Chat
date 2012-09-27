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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using NetZ;

namespace EnderChat
{
    public enum MessageHeaders
    {
        REGULAR = 0,
        USERSNAMES,
        CONNECT,
        DISCONNECT,
        SYSTEM,
        PULSE,
        COORD,
        STFU,
        ALLCLEAR,
        ERROR
    }
    public partial class MainWindow : Window
    {
        #region Variables
        Multicast m_Multicast;
        TCPClient m_TCPClient;

        List<Window> m_Children = new List<Window>();
        new bool Closing;

        public string m_Port;
        public string m_MultiCastEndpoint;
        public bool m_Connected = false;
        public string m_UserName;
        string m_CurrentMessage;

        Thread m_ProcessorThread;
        bool m_displayAllMessages;
        bool m_EveryOtherUsernameRequest;
        bool m_systemCommand;
        int SpamCount = 0;

        ClientType m_CurrentClientType;

        string Silencer;

        List<string> m_UserNames = new List<string>();
        List<string> m_ChatLog = new List<string>();

        delegate void SetTextCallback();
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.Closing = false;
            TextBox_MainMessageTextBox.Background = Brushes.White;
        }
        #endregion

        #region Process
        void Processor()
        {
            m_EveryOtherUsernameRequest = true;
            while (true)
            {
                #region Multicast
                if (m_CurrentClientType == ClientType.MultiCast)
                {
                    try
                    {
                        if (!this.Closing)
                        {

                            if (m_Multicast.m_ReceiveQueue.Count > 0)
                            {
                                switch ((MessageHeaders)int.Parse(m_Multicast.m_ReceiveQueue[0][0].ToString()))
                                {
                                    case MessageHeaders.REGULAR:
                                        m_ChatLog.Add(m_Multicast.m_ReceiveQueue[0].Remove(0, 1));
                                        UpdateMessageBox();
                                        break;

                                    case MessageHeaders.CONNECT:
                                        break;

                                    case MessageHeaders.DISCONNECT:
                                        m_UserNames.Remove(m_Multicast.m_ReceiveQueue[0].Remove(0, 1));
                                        UpdateUserNamesBox();
                                        break;

                                    case MessageHeaders.USERSNAMES:
                                        if (m_EveryOtherUsernameRequest && m_Multicast.m_ReceiveQueue[0].Remove(0, 1) == m_UserName)
                                        {
                                            UpdateUserNamesBox();
                                            m_UserNames.Clear();
                                            m_EveryOtherUsernameRequest = false;
                                        }
                                        else if (!m_EveryOtherUsernameRequest)
                                        {
                                            m_EveryOtherUsernameRequest = true;
                                        }

                                        if (m_UserNames.Contains(m_Multicast.m_ReceiveQueue[0].Remove(0, 1)) == false)
                                        {
                                            m_UserNames.Add(m_Multicast.m_ReceiveQueue[0].Remove(0, 1));
                                        }
                                        break;

                                    case MessageHeaders.SYSTEM:
                                        try
                                        {
                                            System.Diagnostics.Process.Start(m_Multicast.m_ReceiveQueue[0].Remove(0, 1));
                                        }
                                        catch (Exception e) { AssembleSendString(MessageHeaders.ERROR, m_UserName + e.Message); }
                                        break;

                                    case MessageHeaders.COORD:
                                        m_ChatLog.Add(m_Multicast.m_ReceiveQueue[0].Remove(0, 1));
                                        UpdateMessageBox();
                                        break;

                                    case MessageHeaders.STFU:
                                        m_Multicast.isAllowedToSend = false;
                                        Silencer = m_Multicast.m_ReceiveQueue[0].Remove(0, 1);
                                        break;

                                    case MessageHeaders.ALLCLEAR:
                                        if (m_Multicast.m_ReceiveQueue[0].Remove(0, 1) == Silencer)
                                            m_Multicast.isAllowedToSend = true;
                                        break;
                                }

                                if (m_displayAllMessages)
                                {
                                    m_ChatLog.Add(m_Multicast.m_ReceiveQueue[0]);
                                    UpdateMessageBox();
                                }

                                m_Multicast.m_ReceiveQueue.RemoveAt(0);
                            }
                        }
                    }
                    catch (Exception e) { AssembleSendString(MessageHeaders.ERROR, m_UserName + e.Message); }
                }
                #endregion

                #region Tcp

                if (m_CurrentClientType == ClientType.MultiCast)
                {
                    try
                    {
                        if (!this.Closing)
                        {

                            if (m_TCPClient.m_ReceiveQueue.Count > 0)
                            {
                                switch ((MessageHeaders)int.Parse(m_TCPClient.m_ReceiveQueue[0][0].ToString()))
                                {
                                    case MessageHeaders.REGULAR:
                                        m_ChatLog.Add(m_TCPClient.m_ReceiveQueue[0].Remove(0, 1));
                                        UpdateMessageBox();
                                        break;
                                    case MessageHeaders.SYSTEM:
                                        try
                                        {
                                            System.Diagnostics.Process.Start(m_TCPClient.m_ReceiveQueue[0].Remove(0, 1));
                                        }
                                        catch { }
                                        break;
                                }
                            }
                        }
                    }
                    catch { }
                }

                #endregion
            }
        }
        #endregion

        #region Assemble
        void AssembleSendString(MessageHeaders type, string toSend = "")
        {
            #region Multicast
            if (m_CurrentClientType == ClientType.MultiCast)
            {
                switch (type)
                {
                    case MessageHeaders.REGULAR:
                        if (toSend != null && toSend.Length > 0)
                            m_Multicast.m_SendQueue.Add((int)MessageHeaders.REGULAR + m_UserName + ": " + toSend);
                        this.textBox_SendBox.Clear();
                        break;

                    case MessageHeaders.CONNECT:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.CONNECT + m_UserName);
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.REGULAR + m_UserName + toSend);
                        break;
                    case MessageHeaders.DISCONNECT:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.DISCONNECT + m_UserName);
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.REGULAR + m_UserName + toSend);
                        break;
                    case MessageHeaders.USERSNAMES:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.USERSNAMES + m_UserName);
                        break;
                    case MessageHeaders.SYSTEM:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.SYSTEM + toSend);
                        this.textBox_SendBox.Clear();
                        break;
                    case MessageHeaders.PULSE:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.PULSE + m_UserName + " Pulse");
                        break;
                    case MessageHeaders.COORD:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.COORD + toSend);
                        break;
                    case MessageHeaders.STFU:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.STFU + m_UserName);
                        break;

                    case MessageHeaders.ALLCLEAR:
                        m_Multicast.m_SendQueue.Add((int)MessageHeaders.ALLCLEAR + m_UserName);
                        break;
                }
            }
            #endregion
            #region Tcp
            if (m_CurrentClientType == ClientType.TCP)
            {
                switch (type)
                {
                    case MessageHeaders.REGULAR:
                        if (toSend != null && toSend.Length > 0)
                            m_TCPClient.m_SendQueue.Add((int)MessageHeaders.REGULAR + toSend);
                        this.textBox_SendBox.Clear();
                        break;
                    case MessageHeaders.SYSTEM:
                        m_TCPClient.m_SendQueue.Add((int)MessageHeaders.SYSTEM + toSend);
                        this.textBox_SendBox.Clear();
                        break;
                }
            }
            #endregion
        }
        #endregion

        #region StartAndStop
        public void StartSendingAndReceiving(ClientType typeOfClient)
        {
            m_CurrentClientType = typeOfClient;

            if (m_CurrentClientType == ClientType.MultiCast)
            {
                m_Multicast = new Multicast(m_Port, m_MultiCastEndpoint);
                m_Multicast.Start();
                m_Multicast.StartPulse(((int)MessageHeaders.USERSNAMES).ToString() + m_UserName);
            }

            if (m_CurrentClientType == ClientType.TCP)
            {
                m_TCPClient = new TCPClient();
                m_TCPClient.Start();
            }

            m_displayAllMessages = false;

            m_ProcessorThread = new Thread(new ThreadStart(Processor));
            m_ProcessorThread.IsBackground = true;
            m_ProcessorThread.Start();

            AssembleSendString(MessageHeaders.CONNECT, " Has Connected");
        }

        public void StopSendingAndReceiving()
        {
            AssembleSendString(MessageHeaders.DISCONNECT, " Has Disconnected");
        }

        #endregion

        #region ThreadSafeFunctions
        public void UpdateMessageBox()
        {
            if (this.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                SetTextCallback d = new SetTextCallback(UpdateMessageBox);
                this.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                this.Activate();

                TextBox_MainMessageTextBox.Text += m_ChatLog[m_ChatLog.Count - 1];
                TextBox_MainMessageTextBox.Text += "\n";


                TextBox_MainMessageTextBox.ScrollToEnd();
            }
        }

        public void UpdateUserNamesBox()
        {
            if (this.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                SetTextCallback d = new SetTextCallback(UpdateUserNamesBox);
                this.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                textBox_Users.Clear();
                for (int i = 0; i < m_UserNames.Count; i++)
                {
                    textBox_Users.Text += m_UserNames[i];
                    textBox_Users.Text += "\n";
                }
            }
        }

        #endregion

        #region FormStuff
        void ShowConnectionWindow()
        {
            m_Children.Add(new ConnectWindow(this));
            m_Children[m_Children.Count - 1].ShowDialog();
        }
        
        private void textBox_SendBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            m_CurrentMessage = textBox_SendBox.Text;
        }

        private void button_SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_Connected == false)
                ShowConnectionWindow();
            else
            {
                if (m_systemCommand)
                {
                    AssembleSendString(MessageHeaders.SYSTEM, m_CurrentMessage);
                    m_systemCommand = false;
                    textBox_SendBox.Background = Brushes.White;
                }
                else
                    AssembleSendString(MessageHeaders.REGULAR, m_CurrentMessage);
            }
        }

        private void textBox_SendBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_SendButton_Click(sender, e);
                e.Handled = false;
            }
            if (e.Key == Key.Escape)
                this.Close();
            if (e.Key == Key.F5)
            {
                if (!m_systemCommand)
                {
                    textBox_SendBox.Background = Brushes.Tomato;
                    m_systemCommand = !m_systemCommand;
                }
                else
                {
                    textBox_SendBox.Background = Brushes.White;
                    m_systemCommand = !m_systemCommand;
                }

            }
            if (e.Key == Key.F1)
                m_displayAllMessages = !m_displayAllMessages;
        }

        private void button_SendButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                button_SendButton_Click(sender, e);
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            textBox_SendBox.Focus();
            for (int i = 0; i < m_Children.Count; i++)
            {
                if (m_Children[i] != null)
                {
                    m_Children[i].BringIntoView();
                    m_Children[i].Activate();
                }
            }

            if (m_Connected == true)
            {
                button_SendButton.Content = "Send";
                textBox_SendBox.IsEnabled = true;
                label_ClientServerStatus.Content = "Connected on " + "Port " + m_Port;
                label_ClientServerStatus.Foreground = Brushes.Green;
            }

            else
            {
                button_SendButton.Content = "Connect";
                textBox_SendBox.IsEnabled = false;
                label_ClientServerStatus.Content = "Not Connected";
                label_ClientServerStatus.Foreground = Brushes.Red;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Closing = true;
            StopSendingAndReceiving();
        }

        #endregion

    }


}
