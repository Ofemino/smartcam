using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SmartCamLibrary;
using SmartCamUI.DataSet1TableAdapters;

namespace SmartCamUI
{
    public partial class frmConfig : Form
    {
        private int savemode;
        private int id;
        private AppSettingsTableAdapter _appConfTa;
        private DataTable _dt;

        public frmConfig(int mode)
        {
            InitializeComponent();
            savemode = mode;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                TerminalProvision pv = new TerminalProvision();
                pv.TerminalId = txbtid.Text.ToUpper();
                pv.Address = txbuseraddress.Text;
                pv.AliasName = txbalias.Text.ToUpper();
                pv.CustodianName = txbusername.Text.ToUpper();
                pv.Email = txbuseremail.Text.ToUpper();
                pv.ImagePath = imgpath.Text;
                pv.JournalPath = jppath.Text;
                pv.Location = txbloc.Text.ToUpper();
                pv.Name = txbbank.Text.ToUpper();
                pv.HeartBeat = int.Parse(txbheartbeat.Text);
                pv.Phone = txbuserphone.Text;
                pv.RemoteIp = txbsip.Text;
                pv.Id = id;
                pv.TerminalType = cmbtmodel.SelectedItem.ToString();
                pv.TerminalIp = txbtip.Text;
                pv.TerminalPort = int.Parse(txbport.Text);

                //send to server
                //serialize object
                string objStr = SerializeObject(pv);
                bool s = SetupClientSocket.SendMessage(4, objStr);

                if (s)
                {
                    _appConfTa = new AppSettingsTableAdapter();
                    _dt = new DataTable();
                    _dt = _appConfTa.GetData();
                    int rId = 0;
                    if (_dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            rId = Convert.ToInt32(_dt.Rows[i][0]);
                        }
                        int a = _appConfTa.UpdateQuery(
                            pv.TerminalId,
                            pv.TerminalType,
                            pv.TerminalIp,
                            pv.RemoteIp,
                            pv.TerminalPort.ToString(),
                            pv.ImagePath,
                            pv.CurrentFile,
                            pv.CustodianName,
                            pv.Phone,
                            pv.Email,
                            pv.Address,
                            pv.JournalPath,
                            pv.Name, pv.AliasName, pv.Location, pv.HeartBeat,
                            rId
                            );
                        if (a > 0)
                        {
                            MessageBox.Show(@"Settings Modified");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(@"Settings Modification failed");
                        }
                    }
                    else
                    {
                        _appConfTa = new AppSettingsTableAdapter();
                        int a = _appConfTa.Insert(
                            pv.TerminalId,
                            pv.TerminalType,
                            pv.TerminalIp,
                            pv.RemoteIp,
                            pv.TerminalPort.ToString(),
                            pv.ImagePath,
                            pv.CurrentFile,
                            pv.CustodianName,
                            pv.Phone,
                            pv.Email,
                            pv.Address,
                            pv.JournalPath,
                            pv.Name, pv.AliasName, pv.Location, pv.HeartBeat
                            );
                        if (a > 0)
                        {
                            MessageBox.Show(@"Settings Created");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(@"Settings Create failed");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Required fields are missing");
            }
        }

        private void imgbrowse_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }

            imgpath.Text = folderPath;
        }
        public string SerializeObject(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize((TextWriter)stringWriter, obj);
            stringWriter.Close();
            return stringWriter.ToString();
        }
        private void jpbrowse_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }

            jppath.Text = folderPath;
        }

        bool Validate()
        {
            bool valid = false;

            if (txbalias.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            if (txbtid.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            if (txbtip.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            if (txbsip.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            if (cmbtmodel.SelectedIndex > -1)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            if (txbuseremail.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            if (txbusername.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            if (txbuserphone.Text != String.Empty)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            return valid;
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            if (savemode.Equals(2))
            {
                LoadSettings();
            }
        }

        void LoadSettings()
        {
            TerminalProvision prov = new TerminalProvision();
            prov.GetSettings();
            id = prov.Id;
            txbalias.Text = prov.AliasName;
            txbbank.Text = prov.Name;
            txbheartbeat.Text = prov.HeartBeat.ToString();
            txbloc.Text = prov.Location;
            txbsip.Text = prov.RemoteIp;
            txbtid.Text = prov.TerminalId;
            txbtip.Text = prov.TerminalIp;
            txbuseraddress.Text = prov.Address;
            txbuseremail.Text = prov.Email;
            txbusername.Text = prov.CustodianName;
            txbuserphone.Text = prov.Phone;
            imgpath.Text = prov.ImagePath;
            jppath.Text = prov.JournalPath;
            txbport.Text = prov.TerminalPort.ToString();

        }
    }
}
