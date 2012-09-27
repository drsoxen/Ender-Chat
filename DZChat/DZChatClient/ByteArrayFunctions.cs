        byte[] BuildMessage(MessageHeaders type, string userName, string message = "")
        {
            return addFinalHeader(mergeByteArrays(mergeByteArrays(buildHeadAndBody((int)type), buildHeadAndBody(userName)),buildHeadAndBody(message)));
        }
        byte[] buildHeadAndBody(int BodyContents)
        {
            byte[] TempBody;
            byte[] TempHeader;
            MemoryStream memoryStream;

            TempBody = ASCIIEncoding.ASCII.GetBytes(((int)BodyContents).ToString());
            TempHeader = new byte[sizeof(int)];
            int first = (int)TempBody.Length;
            TempHeader = BitConverter.GetBytes(first);
            int size = (int)TempHeader.Length + (int)TempBody.Length;
            memoryStream = new MemoryStream(new byte[size], 0, size, true, true);
            memoryStream.Write(TempHeader, 0, TempHeader.Length);
            memoryStream.Write(TempBody, 0, TempBody.Length);
            byte[] merged = memoryStream.GetBuffer();

            return merged;
        }
        byte[] buildHeadAndBody(string BodyContents)
        {
            byte[] TempBody;
            byte[] TempHeader;
            MemoryStream memoryStream;

            TempBody = ASCIIEncoding.ASCII.GetBytes(BodyContents);
            TempHeader = new byte[sizeof(int)];
            int first = (int)TempBody.Length;
            TempHeader = BitConverter.GetBytes(first);
            int size = (int)TempHeader.Length + (int)TempBody.Length;
            memoryStream = new MemoryStream(new byte[size], 0, size, true, true);
            memoryStream.Write(TempHeader, 0, TempHeader.Length);
            memoryStream.Write(TempBody, 0, TempBody.Length);
            byte[] merged = memoryStream.GetBuffer();

            return merged;
        }
        byte[] mergeByteArrays(byte[] one, byte[] two)
        {
            MemoryStream memoryStream;
            int size = one.Length + two.Length;
            memoryStream = new MemoryStream(new byte[size], 0, size, true, true);
            memoryStream.Write(one, 0, one.Length);
            memoryStream.Write(two, 0, two.Length);
            byte[] merged = memoryStream.GetBuffer();

            return merged;
        }
        byte[] addFinalHeader(byte[] Final)
        {
            int size = (int)Final.Length;
            byte[] Header = new byte[sizeof(int)];
            Header = BitConverter.GetBytes(size);
            return mergeByteArrays(Header, Final);
        }
		
		                            //int currentOffset = 0;
                            //byte[] TempQueue = m_ReceiveQueue[0];

                            //int OverallSize = BitConverter.ToInt32(TempQueue, currentOffset);
                            //currentOffset += sizeof(int);

                            //int FirstSize = BitConverter.ToInt32(TempQueue, currentOffset);
                            //currentOffset += sizeof(int);

                            //MessageHeaders type = (MessageHeaders)int.Parse(ASCIIEncoding.ASCII.GetString(TempQueue, currentOffset, FirstSize));
                            //MessageBox.Show(type.ToString());
                            //currentOffset += sizeof(MessageHeaders);

                            //int secondSize = BitConverter.ToInt32(TempQueue, currentOffset);
                            //currentOffset += sizeof(int);

                            //string Username = ASCIIEncoding.ASCII.GetString(TempQueue, currentOffset, secondSize);
                            //MessageBox.Show(Username);
                            //currentOffset += secondSize;

                            //int thirdSize = BitConverter.ToInt32(TempQueue, currentOffset);
                            //currentOffset += sizeof(int);

                            //string message = BitConverter.ToString(TempQueue, currentOffset);
                            //currentOffset += thirdSize;