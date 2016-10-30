using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleSmartCam.ConsoleSmartCamTableAdapters;

namespace ConsoleSmartCam
{
    public class ImageServerSocket
    {
        private Socket _socket;
        private byte[] _buffer = new byte[1024 * 1024 * 2];
        public IList ConnectedClients = null;
        private static RecievedDataTableAdapter _ta = new RecievedDataTableAdapter();

        public ImageServerSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Bind(int port)
        {
            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        public void Listen(int backLog)
        {
            _socket.Listen(backLog);
        }

        public void Accept()
        {
            Console.WriteLine("Waiting for image connection...");
            _socket.BeginAccept(AcceptedCallBack, null);
        }

        private void AcceptedCallBack(IAsyncResult ar)
        {
            Socket clientSocket = _socket.EndAccept(ar);
            if (clientSocket != null)
            {
                _buffer = new byte[1024 * 1024 * 2];
                clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceivedCallBack, clientSocket);
                Accept();
            }

        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                // This is how you can determine whether a socket is still connected.
                bool blockingState = clientSocket.Blocking;
                Console.WriteLine("In Image ReceivedCallBack...");
                try
                {
                    SocketError se;
                    int bufferSize = clientSocket.EndReceive(ar, out se);
                    Console.WriteLine("image buffer size: " + bufferSize.ToString());

                    if (se != SocketError.Success)
                    {
                        Console.WriteLine("Connection not successfull...");
                    }
                    else if (se == SocketError.ConnectionRefused)
                    {
                        Console.WriteLine("image Connection refused...");
                    }
                    else if (se == SocketError.IsConnected)
                    {
                        if (bufferSize > 0)
                        {
                            var clientIp = clientSocket.RemoteEndPoint.ToString();
                            Console.WriteLine(clientIp + " image Connection established...");
                            Array.Resize(ref _buffer, bufferSize);

                            //handle packet
                            string imagetext = Encoding.ASCII.GetString(_buffer);
                            if (imagetext != String.Empty || imagetext.Length > 0)
                            {
                                _ta.Insert(imagetext, DateTime.Now);
                            }

                            //begin listen again 
                            _buffer = new byte[1024 * 1024 * 2];
                            clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceivedCallBack, clientSocket);
                        }
                    }
                }
                catch (SocketException e)
                {
                    // 10035 == WSAEWOULDBLOCK
                    if (e.NativeErrorCode.Equals(10035))
                        Console.WriteLine("Image ReceivedCallBack : Still Connected, but the Send would block");
                    else
                    {
                        Console.WriteLine("Image ReceivedCallBack : Disconnected: error code {0}!", e.NativeErrorCode);
                    }
                }
                finally
                {
                    clientSocket.Blocking = blockingState;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Image ReceivedCallBack err : " + ex.Message);
            }


        }
    }
}
