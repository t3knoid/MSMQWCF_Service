namespace ServicesCPanel
{
    partial class SettingsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDlg));
            this.comboBoxStartupType = new System.Windows.Forms.ComboBox();
            this.gbLogonAs = new System.Windows.Forms.GroupBox();
            this.lbConfirmPassword = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbConfirmPassword = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.rbThisAccount = new System.Windows.Forms.RadioButton();
            this.cbInteractWithDesktop = new System.Windows.Forms.CheckBox();
            this.rbLocalSystemAccount = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.gbLogonAs.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxStartupType
            // 
            this.comboBoxStartupType.FormattingEnabled = true;
            this.comboBoxStartupType.Items.AddRange(new object[] {
            "Automatic (Delayed Start)",
            "Automatic",
            "Manual",
            "Disabled"});
            this.comboBoxStartupType.Location = new System.Drawing.Point(99, 16);
            this.comboBoxStartupType.Name = "comboBoxStartupType";
            this.comboBoxStartupType.Size = new System.Drawing.Size(323, 21);
            this.comboBoxStartupType.TabIndex = 0;
            this.comboBoxStartupType.SelectedIndexChanged += new System.EventHandler(this.comboBoxStartupType_SelectedIndexChanged);
            // 
            // gbLogonAs
            // 
            this.gbLogonAs.Controls.Add(this.lbConfirmPassword);
            this.gbLogonAs.Controls.Add(this.lbPassword);
            this.gbLogonAs.Controls.Add(this.btBrowse);
            this.gbLogonAs.Controls.Add(this.tbConfirmPassword);
            this.gbLogonAs.Controls.Add(this.tbPassword);
            this.gbLogonAs.Controls.Add(this.tbUsername);
            this.gbLogonAs.Controls.Add(this.rbThisAccount);
            this.gbLogonAs.Controls.Add(this.cbInteractWithDesktop);
            this.gbLogonAs.Controls.Add(this.rbLocalSystemAccount);
            this.gbLogonAs.Location = new System.Drawing.Point(23, 53);
            this.gbLogonAs.Name = "gbLogonAs";
            this.gbLogonAs.Size = new System.Drawing.Size(399, 176);
            this.gbLogonAs.TabIndex = 1;
            this.gbLogonAs.TabStop = false;
            this.gbLogonAs.Text = "Log on as";
            // 
            // lbConfirmPassword
            // 
            this.lbConfirmPassword.AutoSize = true;
            this.lbConfirmPassword.Enabled = false;
            this.lbConfirmPassword.Location = new System.Drawing.Point(27, 131);
            this.lbConfirmPassword.Name = "lbConfirmPassword";
            this.lbConfirmPassword.Size = new System.Drawing.Size(93, 13);
            this.lbConfirmPassword.TabIndex = 7;
            this.lbConfirmPassword.Text = "Confirm password:";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Enabled = false;
            this.lbPassword.Location = new System.Drawing.Point(27, 105);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(56, 13);
            this.lbPassword.TabIndex = 6;
            this.lbPassword.Text = "Password:";
            // 
            // btBrowse
            // 
            this.btBrowse.Enabled = false;
            this.btBrowse.Location = new System.Drawing.Point(311, 74);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 5;
            this.btBrowse.Text = "Browse...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tbConfirmPassword
            // 
            this.tbConfirmPassword.Enabled = false;
            this.tbConfirmPassword.Location = new System.Drawing.Point(140, 128);
            this.tbConfirmPassword.MaxLength = 127;
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.PasswordChar = '*';
            this.tbConfirmPassword.Size = new System.Drawing.Size(165, 20);
            this.tbConfirmPassword.TabIndex = 4;
            this.tbConfirmPassword.UseSystemPasswordChar = true;
            // 
            // tbPassword
            // 
            this.tbPassword.Enabled = false;
            this.tbPassword.Location = new System.Drawing.Point(140, 102);
            this.tbPassword.MaxLength = 127;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(165, 20);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // tbUsername
            // 
            this.tbUsername.Enabled = false;
            this.tbUsername.Location = new System.Drawing.Point(140, 76);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(165, 20);
            this.tbUsername.TabIndex = 2;
            // 
            // rbThisAccount
            // 
            this.rbThisAccount.AutoSize = true;
            this.rbThisAccount.Location = new System.Drawing.Point(11, 76);
            this.rbThisAccount.Name = "rbThisAccount";
            this.rbThisAccount.Size = new System.Drawing.Size(87, 17);
            this.rbThisAccount.TabIndex = 2;
            this.rbThisAccount.Text = "This account";
            this.rbThisAccount.UseVisualStyleBackColor = true;
            this.rbThisAccount.CheckedChanged += new System.EventHandler(this.rbThisAccount_CheckedChanged);
            // 
            // cbInteractWithDesktop
            // 
            this.cbInteractWithDesktop.AutoSize = true;
            this.cbInteractWithDesktop.Location = new System.Drawing.Point(30, 53);
            this.cbInteractWithDesktop.Name = "cbInteractWithDesktop";
            this.cbInteractWithDesktop.Size = new System.Drawing.Size(201, 17);
            this.cbInteractWithDesktop.TabIndex = 1;
            this.cbInteractWithDesktop.Text = "Allow service to interact with desktop";
            this.cbInteractWithDesktop.UseVisualStyleBackColor = true;
            this.cbInteractWithDesktop.CheckedChanged += new System.EventHandler(this.cbInteractWithDesktop_CheckedChanged);
            // 
            // rbLocalSystemAccount
            // 
            this.rbLocalSystemAccount.AutoSize = true;
            this.rbLocalSystemAccount.Checked = true;
            this.rbLocalSystemAccount.Location = new System.Drawing.Point(11, 30);
            this.rbLocalSystemAccount.Name = "rbLocalSystemAccount";
            this.rbLocalSystemAccount.Size = new System.Drawing.Size(130, 17);
            this.rbLocalSystemAccount.TabIndex = 0;
            this.rbLocalSystemAccount.TabStop = true;
            this.rbLocalSystemAccount.Text = "Local System account";
            this.rbLocalSystemAccount.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Startup type";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(185, 245);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 3;
            this.btOK.Text = "O&K";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(266, 245);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "&Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btApply
            // 
            this.btApply.Location = new System.Drawing.Point(347, 245);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.TabIndex = 5;
            this.btApply.Text = "&Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // SettingsDlg
            // 
            this.AcceptButton = this.btApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(442, 285);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbLogonAs);
            this.Controls.Add(this.comboBoxStartupType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsDlg";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDlg_Load);
            this.gbLogonAs.ResumeLayout(false);
            this.gbLogonAs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxStartupType;
        private System.Windows.Forms.GroupBox gbLogonAs;
        private System.Windows.Forms.Label lbConfirmPassword;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TextBox tbConfirmPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.RadioButton rbThisAccount;
        private System.Windows.Forms.CheckBox cbInteractWithDesktop;
        private System.Windows.Forms.RadioButton rbLocalSystemAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btApply;
    }
}