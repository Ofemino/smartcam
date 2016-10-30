using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleSmartCam.ConsoleSmartCamTableAdapters;

namespace ConsoleSmartCam
{
    public class SetupServerSocket1
    {
        private byte[] buffer = new byte[5000];
        private Socket _clientSocket;
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static RecievedDataTableAdapter _ta = new RecievedDataTableAdapter();


        public void SetupServer()
        {
            try
            {
                Console.WriteLine("Setting up Server.");
                Console.WriteLine("Waiting for connection.");
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 20215));
                //_serverSocket.Bind(new IPEndPoint(IPAddress.Parse("192.168.43.171"), 800));
                _serverSocket.Listen(1000);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setupserver Err: " + ex.Message);
            }

        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            try
            {
                _clientSocket = _serverSocket.EndAccept(ar);

                Console.WriteLine("Client connected.");

                buffer = new byte[_clientSocket.ReceiveBufferSize * 100]; // correction
                _clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception errorException)
            {
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
                Console.WriteLine(errorException.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int receied = _clientSocket.EndReceive(ar);
                if (receied == 0)
                {
                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
                    return;
                }
                Array.Resize(ref buffer, receied);
                string text = Encoding.ASCII.GetString(buffer);

                _ta.Insert(text, DateTime.Now);

                buffer = new byte[_clientSocket.ReceiveBufferSize * 100];
                _clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                buffer = new byte[_clientSocket.ReceiveBufferSize * 100];
                _clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }

        }
    }

}
