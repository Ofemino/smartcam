using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using SmartCamClient.smartcamdbDataSet1TableAdapters;

namespace SmartCamClient
{
    public class MessageProcessor
    {
        private DataTable _dt;
        private SessionsTableAdapter _sessTa;

        public DataTable GetUnParseJournal()
        {
            _dt = new DataTable();
            _sessTa = new SessionsTableAdapter();
            _dt = _sessTa.GetData();
            return _dt;
        }

        public void DeleteRowFromTable(int id)
        {
            _sessTa = new SessionsTableAdapter();
            _sessTa.DeleteById(id);
        }

        public string SerializeObject(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize((TextWriter)stringWriter, obj);
            stringWriter.Close();
            return stringWriter.ToString();
        }

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
