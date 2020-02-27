namespace ServicesCPanel
{
    partial class ServicesControlPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesControlPanel));
            this.checkBoxStartWorker = new System.Windows.Forms.CheckBox();
            this.checkBoxStartDispatcher = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.rbStartProcessorService = new System.Windows.Forms.RadioButton();
            this.rbStartProcessorConsole = new System.Windows.Forms.RadioButton();
            this.groupBoxProcessorStartAs = new System.Windows.Forms.GroupBox();
            this.groupBoxProcessor = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusProcessor = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDispatcherHostname = new System.Windows.Forms.Label();
            this.textBoxDispatcherHostname = new System.Windows.Forms.TextBox();
            this.groupBoxDispatcherStartAs = new System.Windows.Forms.GroupBox();
            this.rbStartDispatcherConsole = new System.Windows.Forms.RadioButton();
            this.rbStartDispatcherService = new System.Windows.Forms.RadioButton();
            this.gbDispatcher = new System.Windows.Forms.GroupBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusDispatcher = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripToolTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxProcessorStartAs.SuspendLayout();
            this.groupBoxProcessor.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxDispatcherStartAs.SuspendLayout();
            this.gbDispatcher.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.contextMenuStripToolTray.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxStartProcessor
            // 
            this.checkBoxStartWorker.AutoSize = true;
            this.checkBoxStartWorker.Checked = true;
            this.checkBoxStartWorker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartWorker.Location = new System.Drawing.Point(19, 60);
            this.checkBoxStartWorker.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxStartWorker.Name = "checkBoxStartProcessor";
            this.checkBoxStartWorker.Size = new System.Drawing.Size(98, 17);
            this.checkBoxStartWorker.TabIndex = 0;
            this.checkBoxStartWorker.Text = "Start Worker";
            this.toolTip1.SetToolTip(this.checkBoxStartWorker, "Check to start the worker in this machine.");
            this.checkBoxStartWorker.UseVisualStyleBackColor = true;
            this.checkBoxStartWorker.CheckedChanged += new System.EventHandler(this.checkBoxStartProcessor_CheckedChanged);
            // 
            // checkBoxStartDispatcher
            // 
            this.checkBoxStartDispatcher.AutoSize = true;
            this.checkBoxStartDispatcher.Checked = true;
            this.checkBoxStartDispatcher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartDispatcher.Location = new System.Drawing.Point(18, 59);
            this.checkBoxStartDispatcher.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxStartDispatcher.Name = "checkBoxStartDispatcher";
            this.checkBoxStartDispatcher.Size = new System.Drawing.Size(102, 17);
            this.checkBoxStartDispatcher.TabIndex = 1;
            this.checkBoxStartDispatcher.Text = "Start Dispatcher";
            this.toolTip1.SetToolTip(this.checkBoxStartDispatcher, "Check to start the dispatcher in this machine.");
            this.checkBoxStartDispatcher.UseVisualStyleBackColor = true;
            this.checkBoxStartDispatcher.CheckedChanged += new System.EventHandler(this.checkBoxStartDispatcher_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(144, 340);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(80, 25);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "&Start";
            this.toolTip1.SetToolTip(this.buttonStart, "Starts services.");
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // rbStartProcessorService
            // 
            this.rbStartProcessorService.AutoSize = true;
            this.rbStartProcessorService.Checked = true;
            this.rbStartProcessorService.Location = new System.Drawing.Point(16, 23);
            this.rbStartProcessorService.Margin = new System.Windows.Forms.Padding(2);
            this.rbStartProcessorService.Name = "rbStartProcessorService";
            this.rbStartProcessorService.Size = new System.Drawing.Size(61, 17);
            this.rbStartProcessorService.TabIndex = 3;
            this.rbStartProcessorService.TabStop = true;
            this.rbStartProcessorService.Text = "Service";
            this.toolTip1.SetToolTip(this.rbStartProcessorService, "Installs and runs the worker service in this machine.");
            this.rbStartProcessorService.UseVisualStyleBackColor = true;
            // 
            // rbStartProcessorConsole
            // 
            this.rbStartProcessorConsole.AutoSize = true;
            this.rbStartProcessorConsole.Location = new System.Drawing.Point(16, 43);
            this.rbStartProcessorConsole.Margin = new System.Windows.Forms.Padding(2);
            this.rbStartProcessorConsole.Name = "rbStartProcessorConsole";
            this.rbStartProcessorConsole.Size = new System.Drawing.Size(63, 17);
            this.rbStartProcessorConsole.TabIndex = 4;
            this.rbStartProcessorConsole.Text = "Console";
            this.toolTip1.SetToolTip(this.rbStartProcessorConsole, "Uninstalls the worker service in this machine and runs it as a console applica" +
        "tion.");
            this.rbStartProcessorConsole.UseVisualStyleBackColor = true;
            // 
            // groupBoxProcessorStartAs
            // 
            this.groupBoxProcessorStartAs.Controls.Add(this.rbStartProcessorConsole);
            this.groupBoxProcessorStartAs.Controls.Add(this.rbStartProcessorService);
            this.groupBoxProcessorStartAs.Location = new System.Drawing.Point(260, 16);
            this.groupBoxProcessorStartAs.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxProcessorStartAs.Name = "groupBoxProcessorStartAs";
            this.groupBoxProcessorStartAs.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxProcessorStartAs.Size = new System.Drawing.Size(121, 65);
            this.groupBoxProcessorStartAs.TabIndex = 5;
            this.groupBoxProcessorStartAs.TabStop = false;
            this.groupBoxProcessorStartAs.Text = "Start As";
            // 
            // groupBoxProcessor
            // 
            this.groupBoxProcessor.Controls.Add(this.statusStrip1);
            this.groupBoxProcessor.Controls.Add(this.label1);
            this.groupBoxProcessor.Controls.Add(this.labelDispatcherHostname);
            this.groupBoxProcessor.Controls.Add(this.checkBoxStartWorker);
            this.groupBoxProcessor.Controls.Add(this.textBoxDispatcherHostname);
            this.groupBoxProcessor.Controls.Add(this.groupBoxProcessorStartAs);
            this.groupBoxProcessor.Location = new System.Drawing.Point(14, 37);
            this.groupBoxProcessor.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxProcessor.Name = "groupBoxProcessor";
            this.groupBoxProcessor.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxProcessor.Size = new System.Drawing.Size(395, 151);
            this.groupBoxProcessor.TabIndex = 6;
            this.groupBoxProcessor.TabStop = false;
            this.groupBoxProcessor.Text = "Worker";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusProcessor});
            this.statusStrip1.Location = new System.Drawing.Point(2, 127);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(391, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusProcessor
            // 
            this.toolStripStatusProcessor.Name = "toolStripStatusProcessor";
            this.toolStripStatusProcessor.Size = new System.Drawing.Size(135, 17);
            this.toolStripStatusProcessor.Text = "toolStripStatusProcessor";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Check this checkbox to start the Worker Service.";
            // 
            // labelDispatcherHostname
            // 
            this.labelDispatcherHostname.AutoSize = true;
            this.labelDispatcherHostname.Location = new System.Drawing.Point(16, 97);
            this.labelDispatcherHostname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDispatcherHostname.Name = "labelDispatcherHostname";
            this.labelDispatcherHostname.Size = new System.Drawing.Size(102, 13);
            this.labelDispatcherHostname.TabIndex = 8;
            this.labelDispatcherHostname.Text = "Dispatcher Location";
            // 
            // textBoxDispatcherHostname
            // 
            this.textBoxDispatcherHostname.Location = new System.Drawing.Point(122, 94);
            this.textBoxDispatcherHostname.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDispatcherHostname.Name = "textBoxDispatcherHostname";
            this.textBoxDispatcherHostname.Size = new System.Drawing.Size(261, 20);
            this.textBoxDispatcherHostname.TabIndex = 7;
            this.textBoxDispatcherHostname.Text = "localhost";
            this.toolTip1.SetToolTip(this.textBoxDispatcherHostname, "Enter the hostname of the dispatcher service. This is typically in a remote serve" +
        "r.");
            this.textBoxDispatcherHostname.TextChanged += new System.EventHandler(this.textBoxDispatcherHostname_TextChanged);
            // 
            // groupBoxDispatcherStartAs
            // 
            this.groupBoxDispatcherStartAs.Controls.Add(this.rbStartDispatcherConsole);
            this.groupBoxDispatcherStartAs.Controls.Add(this.rbStartDispatcherService);
            this.groupBoxDispatcherStartAs.Location = new System.Drawing.Point(260, 16);
            this.groupBoxDispatcherStartAs.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxDispatcherStartAs.Name = "groupBoxDispatcherStartAs";
            this.groupBoxDispatcherStartAs.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxDispatcherStartAs.Size = new System.Drawing.Size(121, 65);
            this.groupBoxDispatcherStartAs.TabIndex = 6;
            this.groupBoxDispatcherStartAs.TabStop = false;
            this.groupBoxDispatcherStartAs.Text = "Start As";
            // 
            // rbStartDispatcherConsole
            // 
            this.rbStartDispatcherConsole.AutoSize = true;
            this.rbStartDispatcherConsole.Location = new System.Drawing.Point(16, 43);
            this.rbStartDispatcherConsole.Margin = new System.Windows.Forms.Padding(2);
            this.rbStartDispatcherConsole.Name = "rbStartDispatcherConsole";
            this.rbStartDispatcherConsole.Size = new System.Drawing.Size(63, 17);
            this.rbStartDispatcherConsole.TabIndex = 4;
            this.rbStartDispatcherConsole.Text = "Console";
            this.toolTip1.SetToolTip(this.rbStartDispatcherConsole, "Uninstalls the dispatcher service from this machine and runs it as a console appl" +
        "ication.");
            this.rbStartDispatcherConsole.UseVisualStyleBackColor = true;
            // 
            // rbStartDispatcherService
            // 
            this.rbStartDispatcherService.AutoSize = true;
            this.rbStartDispatcherService.Checked = true;
            this.rbStartDispatcherService.Location = new System.Drawing.Point(16, 23);
            this.rbStartDispatcherService.Margin = new System.Windows.Forms.Padding(2);
            this.rbStartDispatcherService.Name = "rbStartDispatcherService";
            this.rbStartDispatcherService.Size = new System.Drawing.Size(61, 17);
            this.rbStartDispatcherService.TabIndex = 3;
            this.rbStartDispatcherService.TabStop = true;
            this.rbStartDispatcherService.Text = "Service";
            this.toolTip1.SetToolTip(this.rbStartDispatcherService, "Installs and runs the dispatcher service in this machine.");
            this.rbStartDispatcherService.UseVisualStyleBackColor = true;
            // 
            // gbDispatcher
            // 
            this.gbDispatcher.Controls.Add(this.statusStrip2);
            this.gbDispatcher.Controls.Add(this.label2);
            this.gbDispatcher.Controls.Add(this.checkBoxStartDispatcher);
            this.gbDispatcher.Controls.Add(this.groupBoxDispatcherStartAs);
            this.gbDispatcher.Location = new System.Drawing.Point(15, 205);
            this.gbDispatcher.Margin = new System.Windows.Forms.Padding(2);
            this.gbDispatcher.Name = "gbDispatcher";
            this.gbDispatcher.Padding = new System.Windows.Forms.Padding(2);
            this.gbDispatcher.Size = new System.Drawing.Size(395, 120);
            this.gbDispatcher.TabIndex = 7;
            this.gbDispatcher.TabStop = false;
            this.gbDispatcher.Text = "Dispatcher";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusDispatcher});
            this.statusStrip2.Location = new System.Drawing.Point(2, 96);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(391, 22);
            this.statusStrip2.TabIndex = 11;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusDispatcher
            // 
            this.toolStripStatusDispatcher.Name = "toolStripStatusDispatcher";
            this.toolStripStatusDispatcher.Size = new System.Drawing.Size(140, 17);
            this.toolStripStatusDispatcher.Text = "toolStripStatusDispatcher";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 35);
            this.label2.TabIndex = 10;
            this.label2.Text = "Check this checkbox to start the Dispatcher Service.";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(330, 340);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 25);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "&Cancel";
            this.toolTip1.SetToolTip(this.buttonCancel, "Exits out of the control panel.");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(238, 340);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(80, 25);
            this.buttonStop.TabIndex = 10;
            this.buttonStop.Text = "&Stop";
            this.toolTip1.SetToolTip(this.buttonStop, "Stops running services.");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Double-click to restore control panel window";
            this.notifyIcon1.BalloonTipTitle = "Nuix-X Services";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripToolTray;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Double-click to open Nuix-X Services Control Panel";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClosed += new System.EventHandler(this.notifyIcon1_BalloonTipClosed);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStripToolTray
            // 
            this.contextMenuStripToolTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExit,
            this.toolStripMenuItemRestore});
            this.contextMenuStripToolTray.Name = "contextMenuStripToolTray";
            this.contextMenuStripToolTray.Size = new System.Drawing.Size(114, 48);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItemExit.Text = "E&xit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // toolStripMenuItemRestore
            // 
            this.toolStripMenuItemRestore.Name = "toolStripMenuItemRestore";
            this.toolStripMenuItemRestore.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItemRestore.Text = "&Restore";
            this.toolStripMenuItemRestore.Click += new System.EventHandler(this.toolStripMenuItemRestore_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(424, 24);
            this.menuStripMain.TabIndex = 12;
            this.menuStripMain.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ServicesControlPanel
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(424, 378);
            this.ControlBox = false;
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.gbDispatcher);
            this.Controls.Add(this.groupBoxProcessor);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServicesControlPanel";
            this.ShowIcon = false;
            this.Text = "Nuix-X Services Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServicesControlPanel_FormClosing);
            this.Load += new System.EventHandler(this.ServicesControlPanel_Load);
            this.Shown += new System.EventHandler(this.ServicesControlPanel_Shown);
            this.groupBoxProcessorStartAs.ResumeLayout(false);
            this.groupBoxProcessorStartAs.PerformLayout();
            this.groupBoxProcessor.ResumeLayout(false);
            this.groupBoxProcessor.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxDispatcherStartAs.ResumeLayout(false);
            this.groupBoxDispatcherStartAs.PerformLayout();
            this.gbDispatcher.ResumeLayout(false);
            this.gbDispatcher.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.contextMenuStripToolTray.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxStartWorker;
        private System.Windows.Forms.CheckBox checkBoxStartDispatcher;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.RadioButton rbStartProcessorService;
        private System.Windows.Forms.RadioButton rbStartProcessorConsole;
        private System.Windows.Forms.GroupBox groupBoxProcessorStartAs;
        private System.Windows.Forms.GroupBox groupBoxProcessor;
        private System.Windows.Forms.GroupBox groupBoxDispatcherStartAs;
        private System.Windows.Forms.RadioButton rbStartDispatcherConsole;
        private System.Windows.Forms.RadioButton rbStartDispatcherService;
        private System.Windows.Forms.GroupBox gbDispatcher;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDispatcherHostname;
        private System.Windows.Forms.TextBox textBoxDispatcherHostname;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripToolTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRestore;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProcessor;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDispatcher;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

