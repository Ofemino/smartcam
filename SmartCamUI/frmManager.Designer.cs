namespace SmartCamUI
{
    partial class frmManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provisioningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProvisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyProvisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSmartCamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.provisioningToolStripMenuItem,
            this.maintenanceToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(727, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // provisioningToolStripMenuItem
            // 
            this.provisioningToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProvisionToolStripMenuItem,
            this.modifyProvisionToolStripMenuItem});
            this.provisioningToolStripMenuItem.Name = "provisioningToolStripMenuItem";
            this.provisioningToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.provisioningToolStripMenuItem.Text = "Provisioning";
            // 
            // newProvisionToolStripMenuItem
            // 
            this.newProvisionToolStripMenuItem.Name = "newProvisionToolStripMenuItem";
            this.newProvisionToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.newProvisionToolStripMenuItem.Text = "New Provision";
            this.newProvisionToolStripMenuItem.Click += new System.EventHandler(this.newProvisionToolStripMenuItem_Click);
            // 
            // modifyProvisionToolStripMenuItem
            // 
            this.modifyProvisionToolStripMenuItem.Name = "modifyProvisionToolStripMenuItem";
            this.modifyProvisionToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.modifyProvisionToolStripMenuItem.Text = "Modify Provision";
            this.modifyProvisionToolStripMenuItem.Click += new System.EventHandler(this.modifyProvisionToolStripMenuItem_Click);
            // 
            // maintenanceToolStripMenuItem
            // 
            this.maintenanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newIssueToolStripMenuItem,
            this.viewIssuesToolStripMenuItem});
            this.maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            this.maintenanceToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.maintenanceToolStripMenuItem.Text = "Maintenance";
            // 
            // newIssueToolStripMenuItem
            // 
            this.newIssueToolStripMenuItem.Name = "newIssueToolStripMenuItem";
            this.newIssueToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.newIssueToolStripMenuItem.Text = "New Issue";
            // 
            // viewIssuesToolStripMenuItem
            // 
            this.viewIssuesToolStripMenuItem.Name = "viewIssuesToolStripMenuItem";
            this.viewIssuesToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.viewIssuesToolStripMenuItem.Text = "View Issues";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSmartCamToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutSmartCamToolStripMenuItem
            // 
            this.aboutSmartCamToolStripMenuItem.Name = "aboutSmartCamToolStripMenuItem";
            this.aboutSmartCamToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.aboutSmartCamToolStripMenuItem.Text = "About SmartCam";
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 443);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmManager";
            this.Text = "Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManager_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provisioningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProvisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyProvisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maintenanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewIssuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSmartCamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
    }
}