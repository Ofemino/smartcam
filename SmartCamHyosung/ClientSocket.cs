using System;
using System.Net;
using System.Net.Sockets;

namespace SmartCamHyosung
{
    public class ClientSocket
    {
        private Socket _socket;
        private byte[] _buffer;

        public ClientSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ipAddress, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnecCallBack, null);
        }

        public void ConnecCallBack(IAsyncResult ar)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Connected to server...");
                _buffer = new byte[1024];
                _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceivedCallBack, null);
            }
            else
            {
                Console.WriteLine("Could not connect to server...");
            }
        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            int bufferLenght = _socket.EndReceive(ar);
            byte[] packet = new byte[bufferLenght];
            Array.Copy(_buffer, packet, packet.Length);
            //handle packet


            _buffer = new byte[1024];
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceivedCallBack, null);
        }

        public void Send(byte[] data)
        {
            SocketError se;
            _socket.Send(data);
        }
    }
}
