using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

namespace NetZ
{
    public class TCPServer
    {
        #region Variables

        public string m_Port;
        public string m_Endpoint;

        Thread m_recieveThread;
        Thread m_SendThread;
        Thread m_ListenThread;

        TcpListener m_Listener;

        TcpClient m_Client;

        bool m_ReadyToReceive;

        bool m_Listen;
        bool m_Receive;
        bool m_Send;

        public List<string> m_ReceiveQueue = new List<string>();
        public List<string> m_SendQueue = new List<string>();

        #endregion

        #region Constructor

        public TCPServer(string port = "", string Endpoint = "")
        {
            m_Endpoint = Endpoint == "" ? UsefullToolz.GetIP() : Endpoint;
            m_Port = port == "" ? "3000" : port;
        }

        public void Start(bool doSend = true, bool doReceive = true, bool doListen = true)
        {
            m_ReadyToReceive = false;

            m_recieveThread = new Thread(new ThreadStart(ReceiveMessage));
            m_recieveThread.IsBackground = true;
            if (doReceive)
                m_recieveThread.Start();

            m_SendThread = new Thread(new ThreadStart(SendMessage));
            m_SendThread.IsBackground = true;
            if (doSend)
                m_SendThread.Start();

            m_Listener = new TcpListener(IPAddress.Any, int.Parse(m_Port));

            m_ListenThread = new Thread(new ThreadStart(ListenForClients));
            m_ListenThread.IsBackground = true;
            if (doListen)
                m_ListenThread.Start();

            
        }

        public void Stop()
        {
            m_recieveThread.Abort();
            m_SendThread.Abort();
            m_ListenThread.Abort();
            m_Listener.Stop();
            m_Listen = false;
        }

        #endregion

        #region Listen

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

        #endregion

        #region HandleCommunications

        public void HandleCommunications(object client)
        {
            string stringData;
            TcpClient Client = client as TcpClient;
            NetworkStream stream = Client.GetStream();
            byte[] header = new byte[sizeof(int)];
            stream.Read(header, 0, sizeof(int));
            int header_parse = BitConverter.ToInt32(header, 0);

            byte[] data = new byte[header_parse];
            stream.Read(data, 0, (int)header_parse);
            stringData = Encoding.ASCII.GetString(data);

            m_ReceiveQueue.Add(stringData);
        }

        #endregion

        #region Send

        void SendMessage()
        {
            m_Send = true;
            while (m_Send)
            {
                if (m_ReadyToReceive)
                {
                    if (m_SendQueue.Count > 0)
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

        #endregion

        #region Receive

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

        #endregion
    }
}
