using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleSmartCam.ConsoleSmartCamTableAdapters;

namespace ConsoleSmartCam
{
    public class SetupServerSocket
    {

        public byte[] Buffer = new byte[1024 * 1024 * 20];
        //public byte[] _buffer;
        public List<Socket> ClientSockets = new List<Socket>();
        public Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static RecievedDataTableAdapter _ta = new RecievedDataTableAdapter();

        public void SetupServer()
        {

            ////Console.Title(@"SmartCam Server");
            Console.WriteLine(@"Setting up server...");
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 800));
            ServerSocket.Listen(1000);
            Console.WriteLine(@"Server ready for connection...");
            Console.WriteLine(@"Server is listening for connection...");
            Console.WriteLine(@"Do not close console!!!");

            ServerSocket.BeginAccept(AcceptCallBack, null);

        }

        public void AcceptCallBack(IAsyncResult ar)
        {
            try
            {
                Socket socket = ServerSocket.EndAccept(ar);
                ClientSockets.Add(socket);
                Console.WriteLine(@"Client Connected...");
                Buffer = new byte[socket.ReceiveBufferSize * 10];
                socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, RecieveCallBack, socket);
                //socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveCallBack, null);
                ServerSocket.BeginAccept(AcceptCallBack, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("AcceptCallBack err: " + ex.Message);
            }

        }

        public void RecieveCallBack(IAsyncResult ar)
        {
            try
            {
                Socket socket = ar.AsyncState as Socket;
                if (socket != null)
                {
                    SocketError sockErr;
                    int received = socket.EndReceive(ar, out sockErr);
                    if (sockErr != SocketError.Success)
                    {
                        received = 0;
                    }

                    if (received > 0)
                    {
                        // _buffer = new byte[received];
                        //byte[] dataBuff = new byte[received];
                        //Array.Copy(_buffer, dataBuff, received);
                        Array.Resize(ref Buffer, received);
                        var text = Encoding.ASCII.GetString(Buffer);

                        _ta.Insert(text, DateTime.Now);


                        //Array.Clear(_buffer, 0, _buffer.Length);
                        //Array.Clear(dataBuff, 0, dataBuff.Length);


                        socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), socket);
                        ServerSocket.BeginAccept(AcceptCallBack, null);
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(@"SocketException RecieveCallBack Err: " + ex.Message);
            }
        }

        public void SendCallBack(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }

        #region process recieved messages
        //TODO:process session message packetType 2
        public void ProcessSessionMessage(byte[] receiveBytes)
        {
            try
            {
                string returnData = Encoding.ASCII.GetString(receiveBytes);

                //string returnData = receiveBytes;
                char[] mChar = { '\0', ' ' };
                returnData = returnData.Trim('\0').TrimStart(mChar);
                byte[] byteArray = Encoding.Unicode.GetBytes(returnData);

            }
            catch (Exception ex)
            {

                Console.WriteLine(@"Session error: " + ex.Message);
            }
        }

        #endregion

    }

}
