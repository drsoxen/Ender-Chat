using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using NetZ;

namespace DZChatClient
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

    public partial class MainForm : Form
    {
        #region Variables
        Multicast m_Multicast;
        TCPClient m_TCPClient;
        
        List<Form> m_Children = new List<Form>();
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
        public MainForm()
        {
            InitializeComponent();
            MainMessageTextBox.ReadOnly = true;
            TextBox_Users.ReadOnly = true;
            this.Closing = false;
            MainMessageTextBox.BackColor = Color.White;
        }                   
        #endregion

        #region Process
        void Processor()
        {
            m_EveryOtherUsernameRequest = true;
            while (true)
            {
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
            }
        }
        #endregion

        #region Assemble
        void AssembleSendString(MessageHeaders type, string toSend = "")
        {
            if (m_CurrentClientType == ClientType.MultiCast)
            {
                switch (type)
                {
                    case MessageHeaders.REGULAR:
                        if (toSend != null && toSend.Length > 0)
                            m_Multicast.m_SendQueue.Add((int)MessageHeaders.REGULAR + m_UserName + ": " + toSend);
                        SendBox.Clear();
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
                        SendBox.Clear();
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
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateMessageBox);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Activate();
                MainMessageTextBox.Text += m_ChatLog[m_ChatLog.Count - 1];
                MainMessageTextBox.Text += "\n";

                MainMessageTextBox.SelectionStart = MainMessageTextBox.Text.Length;
                MainMessageTextBox.ScrollToCaret();
            }
        }

        public void UpdateUserNamesBox()
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateUserNamesBox);
                this.Invoke(d, new object[] { });
            }
            else
            {
                TextBox_Users.Clear();
                for (int i = 0; i < m_UserNames.Count; i++)
                {
                    TextBox_Users.Text += m_UserNames[i];
                    TextBox_Users.Text += "\n";
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
        private void SendBox_TextChanged(object sender, EventArgs e)
        {
            m_CurrentMessage = SendBox.Text;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (m_Connected == false)
                ShowConnectionWindow();
            else
            {
                if (m_systemCommand)
                {
                    AssembleSendString(MessageHeaders.SYSTEM, m_CurrentMessage);
                    m_systemCommand = false;
                    SendBox.BackColor = Color.White;
                }
                else
                    AssembleSendString(MessageHeaders.REGULAR, m_CurrentMessage);
            }
        }

        private void SendBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Escape)
                this.Close();
            if (e.KeyCode == Keys.F5)
            {
                if (!m_systemCommand)
                {
                    SendBox.BackColor = Color.Tomato;
                    m_systemCommand = !m_systemCommand;
                }
                else
                {
                    SendBox.BackColor = Color.White;
                    m_systemCommand = !m_systemCommand;
                }

            }
            if (e.KeyCode == Keys.F1)
                m_displayAllMessages = !m_displayAllMessages;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            SendBox.Select();
            for (int i = 0; i < m_Children.Count; i++)
            {
                if (m_Children[i] != null)
                {
                    m_Children[i].BringToFront();
                    m_Children[i].Activate();
                }
            }

            if (m_Connected == true)
            {
                SendButton.Text = "Send";
                SendBox.Enabled = true;
                ClientServerStatus.Text = "Connected on " + "Port " + m_Port;
                ClientServerStatus.ForeColor = Color.Green;
            }

            else
            {
                SendButton.Text = "Connect";
                SendBox.Enabled = false;
                ClientServerStatus.Text = "Not Connected";
                ClientServerStatus.ForeColor = Color.Red;
            }
        }

        private void SendButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendButton_Click(sender, e);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Closing = true;
            StopSendingAndReceiving();
        }


        private void SendBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                SendBox.Text += file;
                //filesListBox.Items.Add(file + " (" + FormatedFileSize(file) + ")");
            }

            //SendBox.Text.Text = appTitle + " (DragEnter)";
        }

        private void SendBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private string FormatedFileSize(string fileName)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            double fileSize = (fileInfo.Length / 1024.0);

            if (fileSize < 1024)
                return fileSize.ToString("F01") + " KB";
            else
            {
                fileSize /= 1024.0;

                if (fileSize < 1024)
                    return fileSize.ToString("F01") + " MB";
                else
                {
                    fileSize /= 1024;
                    return fileSize.ToString("F01") + " GB";
                }
            }
        }
        #endregion
    }
}
