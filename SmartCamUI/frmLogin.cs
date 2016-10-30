using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartCamUI.DataSet1TableAdapters;

//using SmartCamLibrary;

namespace SmartCamUI
{
    public partial class frmLogin : Form
    {
        private UsersTableAdapter _userTa;
        private DataTable _dt;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txbusername.Text != String.Empty && txbpassword.Text != String.Empty)
            {
                //do login
                _userTa = new UsersTableAdapter();
                _dt = new DataTable();
                _dt = _userTa.DoLogin(txbusername.Text, txbpassword.Text);
                if (_dt.Rows.Count > 0)
                {
                    frmManager frm = new frmManager();
                    frm.Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("Fields musst not be empty");
            }
        }
    }
}
