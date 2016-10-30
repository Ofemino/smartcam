using System;
using System.Data;
using System.Threading;
using ConsoleSmartCam.ConsoleSmartCamTableAdapters;
using SmartCamLibrary;

namespace ConsoleSmartCam
{
    public class MessageProcessor
    {
        public RecievedDataTableAdapter _taRecievedDataTableAdapter = new RecievedDataTableAdapter();
        public TransSessionTableAdapter _taTransSessionTableAdapter = new TransSessionTableAdapter();
        public TerminalProvisionTableAdapter _TpTableAdapter = new TerminalProvisionTableAdapter();
        public SessionImagesTableAdapter _imgTableAdapter = new SessionImagesTableAdapter();
        public MovedRecievedDataTableAdapter _taMovedRecievedDataTableAdapter = new MovedRecievedDataTableAdapter();


        private DataTable _dt;
        public int RecId = 0;

        public static string[] StringSplit(string s, string separator)
        {
            return s.Split(new string[] { separator }, StringSplitOptions.None);
        }

        public string GetUnParsedMessage()
        {
            try
            {
                _dt = new DataTable();
                _dt = _taRecievedDataTableAdapter.GetData();
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        RecId = Convert.ToInt32(_dt.Rows[i]["Id"].ToString());
                        string unparsed = _dt.Rows[i]["RecievedText"].ToString();
                        if (unparsed != String.Empty)
                        {
                            var toParse = StringSplit(unparsed, "<EOF>");
                            for (int t = 0; t < toParse.Length; t++)
                            {
                                try
                                {
                                    string[] spl = toParse[t].Split(new[] { '|' });
                                    //string[] spl = unparsed.Split(new char[] { '|' });
                                    var type = Convert.ToInt16(spl[0].ToString());
                                    string msg = spl[1].ToString();
                                    switch (type)
                                    {
                                        case 1:
                                            //TODO: Images message
                                            ProcessImagesMsg(msg);
                                            break;
                                        case 2:
                                            //TODO: Session message
                                            ProcessSessionMsg(msg);
                                            break;
                                        case 3:
                                            //TODO: camera message
                                            ProcessCameraMsg(msg);
                                            break;
                                        case 4:
                                            //TODO: Terminal Provision Message
                                            ProcessTerminalProvisionMsg(msg);
                                            break;
                                        case 5:
                                            //TODO: ProcessCallMaintenanceMessage
                                            ProcessMaintenanceMessage(msg);
                                            break;

                                        default:
                                            Console.WriteLine(@"Unknown function recieved!");
                                            break;
                                    }
                                    Thread.Sleep(2000);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Split error...!" + ex.Message);
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Insert failed..." + ex.Message);
            }
            return null;
        }

        private void ProcessImagesMsg(string msg)
        {
            try
            {
                Messages messages = new Messages();
                ImageToSend img = new ImageToSend();
                img = messages.DeSerializeImage(msg);
                if (img != null)
                {
                    if (img.TerminalId != String.Empty)
                    {
                        int ins = _imgTableAdapter.Insert(img.TerminalId, null, img.TransId, img.Image1, img.Image2, img.Image3, img.Image4, img.Image5);
                        if (ins > 0)
                        {
                            Console.WriteLine("Session Image Inserted...");
                            DeleteReceivedPRocessRecord(RecId, _taRecievedDataTableAdapter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Image Insert Failed! " + ex.Message);
            }

        }

        private void ProcessMaintenanceMessage(string msg)
        {
            Messages messages = new Messages();
        }

        private void ProcessTerminalProvisionMsg(string msg)
        {
            try
            {
                Messages messages = new Messages();
                TerminalProvision terminal = new TerminalProvision();
                terminal = messages.DeSerializeProvision(msg);
                if (terminal != null)
                {
                    if (terminal.TerminalId != String.Empty)
                    {
                        int ins = _TpTableAdapter.Insert(terminal.TerminalId, terminal.TerminalType,
                            terminal.TerminalIp, terminal.RemoteIp, terminal.Name, terminal.AliasName,
                            terminal.Location, terminal.JournalPath, terminal.ImagePath, terminal.CustodianName,
                            terminal.Phone, terminal.Email, terminal.Address, terminal.EntryDate, null);

                        if (ins > 0)
                        {
                            Console.WriteLine("Terminal Provision Inserted...");
                            DeleteReceivedPRocessRecord(RecId, _taRecievedDataTableAdapter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Terminal Provisioning Failed. " + ex.Message);
            }

        }


        public void ProcessSessionMsg(string msg)
        {
            TransSession trans = new TransSession();
            trans = Parser.DoParse(msg);
            if (trans != null)
            {
                if (trans.TransId != String.Empty)
                {
                    int ins = _taTransSessionTableAdapter.Insert(trans.TerminalId, trans.TerminalType,
                        trans.TerminalIp,
                        trans.SessionId, trans.TransType, trans.NoteBills, 0, 0, trans.TransId, trans.BillTaken,
                        trans.BillPresented, trans.CardNo, trans.Ledger, trans.Avail, trans.Amount,
                        trans.SessionStartTime,
                        trans.SessionEndTime, trans.JournalPart, trans.TranDate, "", null, trans.Entry, trans.Remark,
                        null, null, 0, null, 0, null, 0, null, 0, null, 0, Convert.ToDecimal(trans.AmountToDouble(trans.Amount)));

                    if (ins > 0)
                    {
                        Console.WriteLine("Record Inserted...");
                        DeleteReceivedPRocessRecord(RecId, _taRecievedDataTableAdapter);
                    }
                    else
                    {
                        MoveUnProcessedRecord(RecId, _taRecievedDataTableAdapter);
                    }
                }
                else
                {
                    //delete row bcos transid is empty
                    DeleteReceivedPRocessRecord(RecId, _taRecievedDataTableAdapter);
                }
            }
        }

        public void ProcessCameraMsg(string text)
        {

        }

        public void DeleteReceivedPRocessRecord(int id, RecievedDataTableAdapter tableAdapter)
        {
            if (id > 0)
            {
                int d = tableAdapter.DeleteQueryById(id);
                if (d > 0)
                {
                    Console.WriteLine("Record deleted from parent table...");
                }
            }
        }

        public void MoveUnProcessedRecord(int id, RecievedDataTableAdapter tableAdapter)
        {
            if (id > 0)
            {
                _taRecievedDataTableAdapter = new RecievedDataTableAdapter();
                _taMovedRecievedDataTableAdapter = new MovedRecievedDataTableAdapter();
                _dt = new DataTable();
                _dt = _taRecievedDataTableAdapter.GetDataById(id);

                foreach (DataRow row in _dt.Rows)
                {
                    var dId = row[0].ToString();
                    var dText = row[1].ToString();
                    var dDate = Convert.ToDateTime(row[2].ToString());
                    int i = _taMovedRecievedDataTableAdapter.Insert(dText, dDate);
                    if (i > 0)
                    {
                        DeleteReceivedPRocessRecord(Convert.ToInt32(dId), _taRecievedDataTableAdapter);
                    }
                }

            }
        }

    }
}
