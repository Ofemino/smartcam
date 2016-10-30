using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartCamUI
{
    public partial class frmManager : Form
    {
        public frmManager()
        {
            InitializeComponent();
        }

        private void newProvisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig config =new frmConfig(1);
            config.ShowDialog();
        }

        private void modifyProvisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig config = new frmConfig(2);
            config.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
