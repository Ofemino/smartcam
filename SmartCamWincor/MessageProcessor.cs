using System.Data;
using System.IO;
using System.Xml.Serialization;
using SmartCamWincor.DataSet1TableAdapters;

namespace SmartCamHyosung
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
}