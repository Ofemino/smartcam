using System;
using System.Data;
using System.IO;
using System.Linq;
using SmartCamWincor.DataSet1TableAdapters;

namespace SmartCamWincor
{
    public class JournalProcessor
    {
        private TextLineCountTableAdapter _lineNumTa = new TextLineCountTableAdapter();
        private static DataTable _dt;
        private string _journalPath;
        private SessionsTableAdapter _sessTa;



        public string NewTransaction;
        public string CashTaken;
        public string NoteBills;
        public string CashPresented;
        public string CardTaken;
        public string CardEntered;
        public string CardEjected;
        public string JournalPart = "";
        public string PrevPinEntered;
        public string PresPinEntered;
        private static AppSettingsTableAdapter _appSetTa;


        public string GetLatestJournalFile()
        {
            try
            {
                string journalDir = String.Empty;
                _dt = new DataTable();
                _appSetTa = new AppSettingsTableAdapter();
                _dt = _appSetTa.GetData();
                string path = null;
                foreach (DataRow row in _dt.Rows)
                {
                    path = row["JournalPath"].ToString();
                }

                string pattern = "*.jrn";
                if (path != String.Empty || path != null || path != "")
                {
                    journalDir = path;
                }
                var dirInfo = new DirectoryInfo(journalDir);
                var file = (from f in dirInfo.GetFiles(pattern)
                    orderby f.LastWriteTime descending
                    select f)
                    .First();
                return file.FullName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Read journal path err : " + ex.Message);
            }
            return null;
        }

        public string CopyLatestJournalToReadDirectory(string latestFileFullName)
        {
            string destination = String.Empty;
            try
            {
                var source = latestFileFullName;
                var dir = new DirectoryInfo(latestFileFullName);
                var name = dir.Name;
                destination = Path.Combine("c:\\SmartCamClient\\toread\\", name);
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }
                File.Copy(source, destination);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return destination;
        }

        public int GetLineCount(string fileFullName)
        {
            try
            {
                var lineCount = 0;
                using (var reader = File.OpenText(fileFullName))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }
                return lineCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLineCount err : " + ex.Message);
            }
            return 0;
        }

        public bool CheckAndProcess(int lineCount)
        {
            try
            {
                int prevLine = 0;
                int dtRow = 0;
                _dt = new DataTable();
                _dt = _lineNumTa.GetData();
                foreach (DataRow row in _dt.Rows)
                {
                    dtRow = Convert.ToInt32(row[0].ToString());
                    prevLine = Convert.ToInt32(row["PrevLineCount"].ToString());
                }
                if (lineCount > prevLine)
                {
                    _lineNumTa.UpdateQuery(lineCount, DateTime.Now, dtRow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        static DataTable GetTerminalParams()
        {
            try
            {
                _dt = new DataTable();
                _appSetTa = new AppSettingsTableAdapter();
                _dt = _appSetTa.GetData();
                int c = _dt.Rows.Count;
                if (c > 0)
                {
                    return _dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTerminalParams err : " + ex.Message);
            }

            return null;
        }

        public void DoParsingFlow(string fileFullName)
        {
            int lineCount = 0;
            try
            {
                using (FileStream fs = File.Open(fileFullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineCount++;

                        if (line.Contains("PIN ENTERED"))
                        {
                            JournalPart = "";
                            CardTaken = "No";
                            CashTaken = "No";
                            CashPresented = "No";
                            CardEjected = "No";
                        }

                       
                        if (line.Contains("TVR:"))
                        {
                            _journalPath = line + Environment.NewLine;
                            while (!line.Contains("TRANSACTION END") || !line.Contains("PIN ENTERED"))
                            {
                                string nextLine = sr.ReadLine();

                                _journalPath += nextLine + Environment.NewLine;
                                if (nextLine != null && nextLine.Contains("PIN ENTERED"))
                                {
                                    break;
                                }
                            }
                            JournalPart = _journalPath;

                            if (JournalPart != "")
                            {
                                try
                                {
                                    _sessTa = new SessionsTableAdapter();
                                    int a = _sessTa.Insert("WINCOR", JournalPart, CardTaken, CashTaken, NoteBills, CashPresented, CardEjected);

                                    JournalPart = "";
                                    CardTaken = "No";
                                    CashTaken = "No";
                                    NoteBills = "";
                                    CashPresented = "No";
                                    CardEjected = "No";

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }

                        //Thread.Sleep(2000);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DoParsingFlow err : " + ex.Message);
            }

        }

        static void DeleteFileFromDirectory()
        {

        }
    }
}