using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleSmartCam.ConsoleSmartCamTableAdapters;

namespace ConsoleSmartCam
{
    public class SetupServerSocket2
    {

        public byte[] Buffer = new byte[1024 * 5];
        //public byte[] _buffer;
        public List<Socket> ClientSockets = new List<Socket>();
        public Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static RecievedDataTableAdapter _ta = new RecievedDataTableAdapter();


        string _text = String.Empty;
        public int PortNumber = 20215;

        public void SetupServer()
        {
            Console.WriteLine(@"Setting up server...");
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, PortNumber));
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
                socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, RecieveCallBack, socket);
                ServerSocket.BeginAccept(AcceptCallBack, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"AcceptCallBack err : " + ex.Message);

                Socket socket = ServerSocket.EndAccept(ar);
                socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, RecieveCallBack, socket);
                ServerSocket.BeginAccept(AcceptCallBack, null);
            }

        }

        public void RecieveCallBack(IAsyncResult ar)
        {
            try
            {
                Socket socket = ar.AsyncState as Socket;
                SocketError sockErr;
                if (socket != null)
                {
                    int received = socket.EndReceive(ar, out sockErr);
                    if (sockErr != SocketError.Success)
                    {
                        received = 0;
                    }

                    if (received > 0)
                    {
                        byte[] dataBuff = new byte[received];
                        Array.Copy(Buffer, dataBuff, received);

                        _text = Encoding.ASCII.GetString(dataBuff, 0, dataBuff.Length);
                        //dump text into received data table
                        _ta.Insert(_text, DateTime.Now);


                        socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, RecieveCallBack, socket);
                        ServerSocket.BeginAccept(AcceptCallBack, null);
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(@"SocketException RecieveCallBack Err: " + ex.Message);

                Socket socket = ar.AsyncState as Socket;
                socket?.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, RecieveCallBack, socket);
                ServerSocket.BeginAccept(AcceptCallBack, null);
            }
        }

        public void SendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendCallBack err : " + ex.Message);
                Socket socket = (Socket)ar.AsyncState;
                socket.EndSend(ar);
            }

        }

    }
}
