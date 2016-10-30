namespace SmartCamUI
{
    partial class frmLogin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txbusername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txbpassword = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(11, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 49);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(163, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Login";
            // 
            // txbusername
            // 
            this.txbusername.Location = new System.Drawing.Point(140, 20);
            this.txbusername.Name = "txbusername";
            this.txbusername.Size = new System.Drawing.Size(194, 20);
            this.txbusername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txbpassword);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txbusername);
            this.panel2.Location = new System.Drawing.Point(11, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 112);
            this.panel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // txbpassword
            // 
            this.txbpassword.Location = new System.Drawing.Point(140, 57);
            this.txbpassword.Name = "txbpassword";
            this.txbpassword.PasswordChar = '*';
            this.txbpassword.Size = new System.Drawing.Size(194, 20);
            this.txbpassword.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btncancel);
            this.panel3.Controls.Add(this.btnlogin);
            this.panel3.Location = new System.Drawing.Point(11, 179);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 48);
            this.panel3.TabIndex = 4;
            // 
            // btncancel
            // 
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.Location = new System.Drawing.Point(49, 6);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(112, 35);
            this.btncancel.TabIndex = 1;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(220, 6);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(112, 35);
            this.btnlogin.TabIndex = 0;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnlogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btncancel;
            this.ClientSize = new System.Drawing.Size(389, 235);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login UI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbusername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbpassword;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnlogin;
    }
}