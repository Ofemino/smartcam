using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ConsoleSmartCam.ConsoleSmartCamTableAdapters;
using SmartCamLibrary;

namespace ConsoleSmartCam
{
    public class JournalParser
    {
        public string AmountStr = string.Empty;
        public string Remark = string.Empty;
        public string CardNo = string.Empty;
        public string TransId = string.Empty;
        public string TerminalId = string.Empty;
        public string TransDate = string.Empty;
        public string TransTime = string.Empty;
        public string CashTaken = string.Empty;
        public string CashPresented = string.Empty;
        public string CardTaken = string.Empty;
        public string CardEntered = string.Empty;
        public string CardEjected = string.Empty;
        public string JournalPart = string.Empty;
        public string MsgValue = string.Empty;
        public int MsgType = 0;
        public string[] ReturnParsed;
        int bp = 0, ct = 0, ce = 0;
        private DataTable _dt;
        private RecievedDataTableAdapter _rdTa;

        TransSession trans = new TransSession();
        NoParseJournal _noParse = new NoParseJournal();
        private TransSessionTableAdapter _sessTa;

        public DataTable GetUnParsedMessage()
        {
            int recId = 0;
            int msgType = 0;
            string unparsed = String.Empty;
            _dt = new DataTable();
            _rdTa = new RecievedDataTableAdapter();
            _dt = _rdTa.GetData();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                recId = Convert.ToInt32(_dt.Rows[i][0]);
                //split and deserialize xml string
                unparsed = _dt.Rows[i][1].ToString();
                string[] unparsed1 = StringSplit(unparsed, "<EOF>");

                foreach (var s in unparsed1)
                {
                    if (s != "")
                    {
                        string[] s1 = s.Split('|');
                        if (s1.Any())
                        {
                            MsgType = Convert.ToInt32(s1[0]);
                            MsgValue = s1[1].ToString();
                        }
                        switch (MsgType)
                        {
                            case 1:
                                //TODO: Images message
                                ///ProcessImagesMsg(msg);
                                break;
                            case 2:
                                //TODO: Session message
                                ProcessSessionMsg(MsgValue, recId);
                                break;
                            case 3:
                                //TODO: camera message
                                // ProcessCameraMsg(msg);
                                break;
                            case 4:
                                //TODO: Terminal Provision Message
                                // ProcessTerminalProvisionMsg(msg);
                                break;
                            case 5:
                                //TODO: ProcessCallMaintenanceMessage
                                // ProcessMaintenanceMessage(msg);
                                break;

                            default:
                                Console.WriteLine(@"Unknown function recieved!");
                                break;
                        }
                    }
                }
            }



            return null;
        }

        void ParseDiebold(string unp)
        {
            unp = unp.Remove(0, 9);
            string[] upArr = unp.Split('\n');
            if (upArr.Any())
            {
                string fst = upArr[0];
                char[] ch1 = new[] { ' ', '\t' };
                string[] sp1 = fst.Split(ch1);
                char[] dtch = new[] { '\\' };
                string[] dtSp = sp1[4].Split(dtch);
                string year = dtSp[2], month = dtSp[1], day = dtSp[0];
                string time = sp1[9];
                DateTime dDate = DateTime.Parse(year + "-" + month + "-" + day + " " + time);
                trans.SessionStartTime = dDate.Date.ToString();
                trans.TranDate = dDate.ToString();
                trans.TerminalId = sp1[14];

                string snd = upArr[1];
                trans.CardNo = snd.Remove(0, 13).Trim();

                string trd = upArr[2];
                char[] ch2 = new[] { ' ', '\t' };
                string[] sp2 = trd.Split(ch2);
                trans.TransId = sp2[0];

                string fth = upArr[3];
                char[] ch3 = new[] { ' ', '\t' };
                string[] sp3 = fth.Split(ch3);
                int cSp3 = sp3.Count();
                if (cSp3 > 1)
                {
                    string oAmount = sp3[cSp3 - 1];
                    trans.Amount = sp3[cSp3 - 1].Remove(0, 3);
                    decimal d;
                    if (decimal.TryParse(trans.Amount, out d))
                    {
                        trans.Amount = trans.Amount;
                        trans.AmountDouble = Convert.ToDouble(trans.Amount);
                    }
                    else
                    {
                        trans.Amount = null;
                        trans.AmountDouble = 0.0;
                    }
                    Remark = fth.ToString().Remove(fth.Length - oAmount.Length);
                    trans.TransType = Remark.Trim();
                }
                else
                {
                    trans.TransType = sp3[0].ToString();
                }
                //trans.TransType = Remark;
                if (!Remark.Contains("WITHDRAW") || !Remark.Contains("INQUIRY") || !Remark.Contains("TRANSFER"))
                {
                    trans.Remark = sp3[0].ToString();
                }


            }
            //return null;
        }
        void ParseWincor(string unp)
        {
            //unp = unp.Remove(0, 9);
            string[] upArr = unp.Split('\n');
            if (upArr.Any())
            {
               
                trans.TerminalType = _noParse.Mtype;
                if (unp.Contains("CASH PRESENTED"))
                {
                   trans.BillPresented = 1;
                }
                if (unp.Contains("CASH")&& unp.Contains("TAKEN"))
                {
                    trans.BillTaken = 1;
                }
                if (unp.Contains("CARD") && unp.Contains("TAKEN"))
                {
                    //trans.CardTaken = 1;
                }
                if (unp.Contains("INQUIRY"))
                {
                    trans.TransType = "INQUIRY";
                }
                if (unp.Contains("INQUIRY"))
                {
                    trans.TransType = "WITHDRAW";
                }


                //note bills
                trans.NoteBills = upArr[2].Trim().Remove(0, 14);

                //trans date and time
                char[] dtch = new[] { ' ', '\t' };
                char[] dtch1 = new[] { ' ', '\\' };
                string[] dtStr = upArr[4].Split(dtch);
                string[] dtSp = dtStr[4].Split(dtch1);
                string year = dtSp[2], month = dtSp[1], day = dtSp[0];
                string time = dtStr[9];
                DateTime dDate = DateTime.Parse(year + "-" + month + "-" + day + " " + time);
                trans.SessionStartTime = dDate.Date.ToString();
                trans.TranDate = dDate.ToString();

                trans.TerminalId = dtStr[14];

            }



            trans.TerminalType = _noParse.Mtype;


        }
        void ParseHyosung(string unp)
        {
            unp = unp.Remove(0, 9);
            string[] upArr = unp.Split('\n');
            if (upArr.Any())
            {
                string fst = upArr[0];
                char[] ch = new[] { ' ', '\t' };
                string[] sp = fst.Split(ch);
                TransDate = sp[0].ToString();
                TransTime = sp[1].ToString();
                TerminalId = sp[2].ToString();



            }
        }

        public void ProcessSessionMsg(string msg, int recId)
        {
            _noParse = DeSerializeObject(msg);
            if (_noParse != null)
            {
                if (_noParse.IsCashPresented == "Yes")
                {
                    bp = 1;
                }
                if (_noParse.IsCashtaken == "Yes")
                {
                    ct = 1;
                }
                if (_noParse.IsCardEjected == "Yes")
                {
                    ce = 1;
                }
                if (_noParse.IsCardEjected == "Yes")
                {
                    ce = 1;
                }


                if (_noParse.Mtype.Contains("DIEBOLD"))
                {
                    trans.TerminalType = _noParse.Mtype;
                    trans.BillPresented = bp;
                    trans.BillTaken = ct;
                    trans.NoteBills = _noParse.NoteBills;
                    //trans = null;
                    ParseDiebold(_noParse.Jpart);
                }
                else if (_noParse.Mtype.Contains("WINCOR"))
                {
                    trans = new TransSession();
                    ParseWincor(_noParse.Jpart);


                }
            }

            _sessTa = new TransSessionTableAdapter();
            int insert = _sessTa.Insert(trans.TerminalId, trans.TerminalType, null, null, trans.TransType,
                trans.NoteBills, null, null, trans.TransId, trans.BillTaken, trans.BillPresented, trans.CardNo, null,
                null, trans.Amount, null, null, trans.JournalPart, trans.TranDate, null, null, DateTime.Now,
                trans.Remark, null, null, null, null, null, null, null, null, null, null, null, Convert.ToDecimal(trans.AmountDouble));

            trans = new TransSession();

            if (insert > 0)
            {
                Console.WriteLine("Record insert successfull...");
                //do delete of record from parent table
                DeleteRecordFromTable(recId);
            }


        }

        private void DeleteRecordFromTable(int recId)
        {
            _sessTa = new TransSessionTableAdapter();
            int d = _sessTa.DeleteById(recId);
            if (d > 0)
            {
                Console.WriteLine("Record deleted from parent table...");
            }
        }

        public NoParseJournal DeSerializeObject(string str)
        {
            try
            {
                using (TextReader textReader = (TextReader)new StringReader(str))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(NoParseJournal));
                    return (NoParseJournal)ser.Deserialize(textReader);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (NoParseJournal)null;
            }
        }


        public string[] StringSplit(string s, string separator)
        {
            return s.Split(new string[] { separator }, StringSplitOptions.None);
        }
    }

    public class JournalMessage
    {
        public string Mtype { get; set; }
        public string IsCashtaken { get; set; }
        public string IsCashPresented { get; set; }
        public string IsCardTaken { get; set; }
        public string IsCardEjected { get; set; }
        public string Jpart { get; set; }

    }
    public class NoParseJournal
    {
        public string Mtype { get; set; }
        public string NoteBills { get; set; }
        public string Jpart { get; set; }
        public string IsCashtaken { get; set; }
        public string IsCashPresented { get; set; }
        public string IsCardEjected { get; set; }
        public string IsCardTaken { get; set; }

    }
}
