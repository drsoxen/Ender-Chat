using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System;
using System.Text;

namespace NetZ
{
    public class TCPClient
    {
        #region Variables
        public int m_Port;
        public string m_Endpoint;        

        Thread m_recieveThread;
        Thread m_SendThread;

        TcpClient m_Client;
        NetworkStream m_Stream;

        public List<string> m_ReceiveQueue = new List<string>();
        public List<string> m_SendQueue = new List<string>();

        bool m_readyToReceive;
        public bool isAllowedToSend;
        public bool isAllowedToReceive;

        #endregion

        #region Constructor
        public TCPClient(string port = "", string Endpoint = "")
        {
            m_Endpoint = Endpoint == "" ? UsefullToolz.GetIP() : Endpoint;
            m_Port = port == "" ? 3000 : int.Parse(port);

            m_Client = new TcpClient();
            m_Client.Connect(m_Endpoint, m_Port);
            m_Stream = m_Client.GetStream();
        }

        public void Start(bool doSend = true, bool doReceive = true)
        {
            m_readyToReceive = false;

            isAllowedToReceive = true;
            m_recieveThread = new Thread(new ThreadStart(ReceiveMessage));
            m_recieveThread.IsBackground = true;
            if(doReceive)
            m_recieveThread.Start();

            isAllowedToSend = true;
            m_SendThread = new Thread(new ThreadStart(SendMessage));
            m_SendThread.IsBackground = true;
            if(doSend)
            m_SendThread.Start();
        }
        #endregion

        #region Send
        void SendMessage()
        {
            while (true)
            {
                if (isAllowedToSend)
                {
                    if (m_readyToReceive)
                    {
                        if (m_SendQueue.Count > 0)
                        {
                            try
                            {
                                m_Client = new TcpClient();
                                m_Client.Connect(m_Endpoint, m_Port);
                                m_Stream = m_Client.GetStream();

                                byte[] Message = ASCIIEncoding.ASCII.GetBytes(m_SendQueue[0]);

                                byte[] header = new byte[sizeof(int)];
                                int length = (int)Message.Length;
                                header = BitConverter.GetBytes(length);
                                m_Stream.Write(header, 0, header.Length);
                                m_Stream.Flush();
                                m_Stream.Write(Message, 0, Message.Length);
                                m_Stream.Close();

                                m_SendQueue.RemoveAt(0);
                            }
                            catch
                            {

                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region Receive
        public void ReceiveMessage()
        {
            byte[] data = new byte[1024];
            int recv;
            string stringData;

            m_readyToReceive = true;

            while (true)
            {
                if (isAllowedToReceive)
                {
                    try
                    {
                        //recv = m_Socket.ReceiveFrom(data, ref ep);
                        //stringData = Encoding.ASCII.GetString(data, 0, recv);
                        //m_ReceiveQueue.Add(stringData);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
        }
        #endregion
    }
}
