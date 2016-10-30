using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SmartCamUI
{
    public static class SetupClientSocket
    {
        private static Socket _clientSocket;
        public static int ServerPort = 20215;
        public static string ServerIp = "127.0.0.1";

        //public SetupClientSocket()
        //{
        //    if (!(ServerIp != string.Empty) || ServerPort <= 0 || _clientSocket.Connected)
        //        return;
        //    _clientSocket.Connect(ServerIp, ServerPort);
        //    Console.WriteLine("Client connected...");
        //}
        //public SetupClientSocket(string serverip, int port)
        //{
        //    if (!(serverip != string.Empty) || port <= 0)
        //        return;
        //    _clientSocket.Connect(serverip, port);
        //    Console.WriteLine("Client connected...");
        //}

        public static bool SendMessage(int msgtype, string _msg)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(ServerIp, ServerPort);
            Console.WriteLine("Client connected...");

            byte[] bytes = Encoding.ASCII.GetBytes(string.Format("{0}|{1}<EOF>", msgtype.ToString(), (object)_msg).Trim());
            try
            {
                if (_clientSocket.Send(bytes, 0, bytes.Length, SocketFlags.None) > 0)
                    Console.WriteLine(bytes.Length + " data bytes sent...");

                Close(_clientSocket);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public static void Close(Socket sock)
        {
            _clientSocket.Close();
        }
    }
}
