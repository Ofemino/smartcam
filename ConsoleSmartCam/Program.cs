using System;
using System.Timers;



namespace ConsoleSmartCam
{
    public class Program
    {
        static MessageProcessor mp = new MessageProcessor();
        static JournalParser jp = new JournalParser();
        private static Timer _messageProcessTimer;

        static ServerSocket _serverSocket = new ServerSocket();
        static ImageServerSocket _imageserverSocket = new ImageServerSocket();

        static void Main(string[] args)
        {

            _messageProcessTimer = new Timer { Interval = 2 * 60 * 1000 };
            _messageProcessTimer.Elapsed += HandleProcessTimer;
            _messageProcessTimer.Enabled = true;

            _serverSocket.Bind(20215);
            _serverSocket.Listen(500);
            _serverSocket.Accept();

            //socket for image
            _imageserverSocket.Bind(20216);
            _imageserverSocket.Listen(500);
            _imageserverSocket.Accept();

            while (true)
            {
                Console.ReadLine();
            }

        }

        private static void HandleProcessTimer(object sender, ElapsedEventArgs e)
        {
            //mp.GetUnParsedMessage();
            jp.GetUnParsedMessage();
        }
    }
}
