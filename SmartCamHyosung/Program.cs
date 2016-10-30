using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Timers;
using SmartCamHyosung.DataSet1TableAdapters;

namespace SmartCamHyosung
{
    public class Program
    {
        public static ClientSocket ClientSocket = new ClientSocket();
        //static void Main(string[] args)
        //{
        //    ClientSocket.Connect("172.27.15.30", 20215);
        //    while (true)
        //    {
        //        Console.ReadLine();
        //    }
        //}
        public int LastLineRead = 0;
        public static string LatestJournalFileFullName = String.Empty;
        public string JurnalPart = String.Empty;
        static JournalProcessor _jp = new JournalProcessor();

        private static Timer _journalProcessTimer;
        private static Timer _msgProcessTimer;
        private static DataTable _dt;
        private static AppSettingsTableAdapter _appSetTa;
        public static int Main(string[] args)
        {
            _journalProcessTimer = new Timer { Interval = 1000 * 60 * 2 };
            _journalProcessTimer.Elapsed += HandleJournalProcessTimer;
            _journalProcessTimer.Enabled = true;

            _msgProcessTimer = new Timer { Interval = 1000 * 60 * 3 };
            _msgProcessTimer.Elapsed += HandleMsgProcessTimer;
            _msgProcessTimer.Enabled = true;


            Console.ReadLine();
            return 0;
        }

        private static void HandleJournalProcessTimer(object sender, ElapsedEventArgs e)
        {
            DoProcessing();
        }

        private static void DoProcessing()
        {
            try
            {
                LatestJournalFileFullName = _jp.GetLatestJournalFile();
                string jPath = "";

                //copy Latest Journal File to read directory
                var newFilePath = _jp.CopyLatestJournalToReadDirectory(LatestJournalFileFullName);

                //read copied Latest Journal File to memory
                int countResult = _jp.GetLineCount(newFilePath);

                bool checkResult = _jp.CheckAndProcess(countResult);

                if (checkResult)
                {
                    //process new journal lines
                    _jp.DoParsingFlow(newFilePath);
                }
                //return jPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void HandleMsgProcessTimer(object sender, ElapsedEventArgs e)
        {
            MessageProcessor msgPro = new MessageProcessor();
            _dt = new DataTable();
            _dt = msgPro.GetUnParseJournal();
            ProcessDataTable(_dt);
        }

        private static void ProcessDataTable(DataTable dt)
        {
            int rId = 0;
            NoParseJournal nop = new NoParseJournal();
            MessageProcessor mp = new MessageProcessor();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rId = Convert.ToInt32(dt.Rows[i][0]);
                nop.Mtype = dt.Rows[i]["Transid"].ToString();
                nop.Jpart = dt.Rows[i]["JournalPart"].ToString();
                nop.NoteBills = dt.Rows[i]["NoteBills"].ToString();
                nop.IsCashPresented = dt.Rows[i]["IsCashPresented"].ToString();
                nop.IsCashtaken = dt.Rows[i]["IsCashtaken"].ToString();
                nop.IsCardEjected = dt.Rows[i]["IsCardEjected"].ToString();
                nop.IsCardTaken = dt.Rows[i]["IsCardTaken"].ToString();

                //serialize object
                string objStr = mp.SerializeObject(nop);


                //send message to socket
                //AsynchronousClient.StartClient(objStr);
                bool isSent = SetupClientSocket.SendMessage(2, objStr);
                if (isSent)
                {
                    //Delete record from table
                    mp.DeleteRowFromTable(rId);
                }
                //ClientSocket.Connect("127.0.0.1", 20215);
                //ClientSocket.Send(Encoding.UTF8.GetBytes(objStr));


            }


        }
    }
}
