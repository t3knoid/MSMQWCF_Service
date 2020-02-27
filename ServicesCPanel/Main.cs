using System;
using System.Configuration;
using System.Windows.Forms;

namespace ServicesCPanel
{
    public partial class ServicesControlPanel : Form
    {
        Console WorkerConsole = new Console();
        Console DispatcherConsole = new Console();

        Properties.Settings settings = Properties.Settings.Default;
        string p = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

        public bool MinimizeToTray { get; private set; }

        public ServicesControlPanel()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Close window
            MinimizeToTray = true;
            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SaveSetttings();
            StartProcessor();
            StartDispatcher();
            SetServiceConsoleRadiobuttons();
            SetUIControlState();
        }

        private void StartDispatcher()
        {
            if (!checkBoxStartDispatcher.Checked)
            {
                return;

            }

            if (rbStartDispatcherService.Checked)
            {
                // Start Dispatcher service
                toolStripStatusDispatcher.Text = "Starting Dispatcher service.";
                if (!DispatcherService.ServiceControl.Installed)
                {
                    // Install dispatcher service
                    toolStripStatusDispatcher.Text = "Installing Dispatcher service";
                    DispatcherService.InstallerOutput output = DispatcherService.Installer.Install(); // Pass the dispatcher hostname to the installer
                    if (output.Status == false)
                    {
                        MessageBox.Show(string.Format("Failed to install Dispatcher service. {0}", output.Message), "Dispatcher Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusDispatcher.Text = "Failed to install Dispatcher service.";
                    }
                    else
                    {
                        //MessageBox.Show(string.Format("{0} Dispatcher service.", output.Message), "Dispatcher Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripStatusDispatcher.Text = "Dispatcher service installed.";
                    }
                }

                // If the Start Dispatcher checkbox is checked, start the dispatcher service
                if (checkBoxStartDispatcher.Checked)
                {
                    try
                    {
                        DispatcherService.ServiceControl.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Failed to start Dispatcher service. {0}", ex.Message), "Dispatcher Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                toolStripStatusDispatcher.Text = "Dispatcher service started.";

            }
            else // Run service as a console application
            {
                toolStripStatusDispatcher.Text = "Starting Dispatcher console.";
                if (DispatcherService.ServiceControl.Installed)
                {
                    // If the DispatcherService is set to start as a console application uninstall from services
                    toolStripStatusDispatcher.Text = "Uninstalling Dispatcher service";
                    DispatcherService.InstallerOutput output = DispatcherService.Installer.Uninstall();
                    if (output.Status == false)
                    {
                        MessageBox.Show(string.Format("Failed to uninstall Dispatcher service. {0}", output.Message), "Dispatcher Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusDispatcher.Text = "Failed to uninstall Dispatcher service.";
                    }
                    else
                    {
                        //MessageBox.Show(string.Format("{0} Dispatcher service.", output.Message), "Dispatcher Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripStatusDispatcher.Text = "Dispatcher service uninstalled.";
                    }
                }

                // If the Start Dispatcher checkbox is checked, start the dispatcher service as a console application
                if (checkBoxStartDispatcher.Checked)
                {
                    DispatcherConsole.Command = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "DispatcherService.exe");
                    DispatcherConsole.Param = "";
                    DispatcherConsole.Run();
                }
                toolStripStatusDispatcher.Text = "Dispatcher console started.";
            }

        }

        private void StartProcessor()
        {
            if (!checkBoxStartWorker.Checked)
            {
                return;
            }

            // If the ProcesserServices is set to start as a service make sure it's installed.           
            if (rbStartProcessorService.Checked)
            {

                toolStripStatusProcessor.Text = "Starting Worker service.";
                if (!WorkerService.ServiceControl.Installed)
                {
                    // Install worker service
                    toolStripStatusProcessor.Text = "Installing Worker service";
                    WorkerService.InstallerOutput output = WorkerService.Installer.Install(textBoxDispatcherHostname.Text); // Pass the dispatcher hostname to the installer
                    if (output.Status == false)
                    {
                        MessageBox.Show(string.Format("Failed to install Worker service. {0}", output.Message), "Worker Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusProcessor.Text = "Failed to install Worker service.";
                    }
                    else
                    {
                        //MessageBox.Show(string.Format("{0} Worker service.", output.Message), "Worker Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripStatusProcessor.Text = "Worker service installed.";
                    }
                }

                // If the Start Worker checkbox is checked, start the worker service
                if (checkBoxStartWorker.Checked)
                {
                    WorkerService.ServiceControl.Start();
                }
                toolStripStatusDispatcher.Text = "Worker service started.";

            }
            else // Run service as a console application
            {
                toolStripStatusProcessor.Text = "Starting Worker console.";
                if (WorkerService.ServiceControl.Installed)
                {
                    // If the ProcesserServices is set to start as a console application uninstall from services
                    toolStripStatusProcessor.Text = "Uninstalling Worker service";
                    WorkerService.InstallerOutput output = WorkerService.Installer.Uninstall();
                    if (output.Status == false)
                    {
                        MessageBox.Show(string.Format("Failed to uninstall Worker service. {0}", output.Message), "Worker Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusProcessor.Text = "Failed to uninstall Worker service.";
                    }
                    else
                    {
                        //MessageBox.Show(string.Format("{0} Worker service.", output.Message), "Worker Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripStatusProcessor.Text = "Worker service uninstalled.";
                    }
                }

                // If the Start Processor checkbox is checked, start the worker service as a console application
                if (checkBoxStartWorker.Checked)
                {
                    WorkerConsole.Command = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "nxProcessorService.exe");
                    WorkerConsole.Param = "dispatcher=" + textBoxDispatcherHostname.Text;
                    WorkerConsole.Run();
                }
                toolStripStatusProcessor.Text = "Processor console started.";
            }
        }

        private void ServicesControlPanel_Load(object sender, EventArgs e)
        {
            textBoxDispatcherHostname.Text = settings.DispatcherHostName;
            checkBoxStartDispatcher.Checked = settings.StartDispatcherService;
            checkBoxStartWorker.Checked = settings.StartProcessorService;
            SetServiceConsoleRadiobuttons();
            textBoxDispatcherHostname.Text = settings.DispatcherHostName;
            SetUIControlState();
            SaveSetttings();
        }

        private void SaveSetttings()
        {
            settings.StartProcessorService = (checkBoxStartWorker.CheckState == CheckState.Checked ? true : false);
            settings.StartDispatcherService = (checkBoxStartDispatcher.CheckState == CheckState.Checked ? true : false);
            settings.DispatcherHostName = textBoxDispatcherHostname.Text;
            settings.Save();
        }

        private void SetServiceConsoleRadiobuttons()
        {
            // Set controls to reflect whether or not the Worker service is installed
            if (WorkerService.ServiceControl.Installed )
            {
                toolStripStatusProcessor.Text = "Worker service is installed.";
                rbStartProcessorService.Checked = true;
                rbStartProcessorConsole.Checked = false;
            }
            else
            {
                toolStripStatusProcessor.Text = "Worker service is not installed.";
                rbStartProcessorService.Checked = false;
                rbStartProcessorConsole.Checked = true;
            }
        }

        private void SetUIControlState()
        {
            string notifyIconText = string.Empty;

            if (WorkerService.ServiceControl.Running || WorkerConsole.Running)
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                rbStartProcessorService.Enabled = false;
                rbStartProcessorConsole.Enabled = false;
                checkBoxStartWorker.Enabled = false;
                textBoxDispatcherHostname.Enabled = false;
                toolStripStatusProcessor.Text = "Worker service running.";
                notifyIconText = "Worker service running.\n";
            }
            else
            {
                if (DispatcherService.ServiceControl.Running || DispatcherConsole.Running)
                {
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                }
                else
                {
                    buttonStart.Enabled = true;
                    buttonStop.Enabled = false;
                }
                rbStartProcessorService.Enabled = true;
                rbStartProcessorConsole.Enabled = true;
                checkBoxStartWorker.Enabled = true;
                textBoxDispatcherHostname.Enabled = true;
                if (DispatcherService.ServiceControl.Installed)
                {
                    rbStartProcessorService.Checked = true;
                    rbStartProcessorConsole.Checked = false;
                }
                else
                {
                    rbStartProcessorService.Checked = false;
                    rbStartProcessorConsole.Checked = true;
                }
                toolStripStatusProcessor.Text = "Worker service stopped.";
                notifyIconText = notifyIconText + "Worker service stopped.\n";
            }

            if (DispatcherService.ServiceControl.Running || DispatcherConsole.Running)
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                rbStartDispatcherService.Enabled = false;
                rbStartDispatcherConsole.Enabled = false;
                textBoxDispatcherHostname.Enabled = false;
                checkBoxStartDispatcher.Enabled = false;
                toolStripStatusDispatcher.Text = "Dispatcher service running.";
                notifyIconText = notifyIconText + "Dispatcher service running.\n";
            }
            else
            {
                if (WorkerService.ServiceControl.Running || WorkerConsole.Running)
                {
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    textBoxDispatcherHostname.Enabled = false;
                }
                else
                {
                    buttonStart.Enabled = true;
                    buttonStop.Enabled = false;
                    textBoxDispatcherHostname.Enabled = true;
                }
                checkBoxStartDispatcher.Enabled = true;
                rbStartDispatcherService.Enabled = true;
                rbStartDispatcherConsole.Enabled = true;
                if (DispatcherService.ServiceControl.Installed)
                {
                    rbStartDispatcherService.Checked = true;
                    rbStartDispatcherConsole.Checked = false;
                }
                else
                {
                    rbStartDispatcherService.Checked = false;
                    rbStartDispatcherConsole.Checked = true;
                }
                toolStripStatusDispatcher.Text = "Dispatcher service stopped.";
                notifyIconText = notifyIconText + "Dispatcher service stopped.\n";
            }
            notifyIcon1.Text = notifyIconText;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Stop Processor services here
            if (WorkerService.ServiceControl.Running)
            {
                toolStripStatusProcessor.Text = "Stopping Worker service.";
                WorkerService.ServiceControl.Stop();
                SetUIControlState();
            }

            if (WorkerConsole.Running)
            {
                toolStripStatusProcessor.Text = "Stopping Processor console.";
                WorkerConsole.Stop();
                SetUIControlState();
            }

            // Stop Dispatcher services here
            if (DispatcherService.ServiceControl.Running)
            {
                toolStripStatusDispatcher.Text = "Stopping Dispatcher service.";
                DispatcherService.ServiceControl.Stop();
                SetUIControlState();
            }

            if (DispatcherConsole.Running)
            {
                toolStripStatusDispatcher.Text = "Stopping Dispatcher console.";
                DispatcherConsole.Stop();
                SetUIControlState();
            }

        }

        private void checkBoxStartProcessor_CheckedChanged(object sender, EventArgs e)
        {
            settings.StartProcessorService = (checkBoxStartWorker.CheckState ==  CheckState.Checked ? true : false);
            settings.Save();
            if (checkBoxStartWorker.Checked)
            {
                buttonStart.Enabled = true;
                groupBoxProcessorStartAs.Enabled = true;
                rbStartProcessorConsole.Enabled = true;
                rbStartProcessorService.Enabled = true;
                textBoxDispatcherHostname.Enabled = true;
            }
            else
            {
                if (checkBoxStartDispatcher.Checked)
                {
                    buttonStart.Enabled = true;
                }
                else
                {
                    buttonStart.Enabled = false;
                }
                groupBoxProcessorStartAs.Enabled = false;
                rbStartProcessorConsole.Enabled = false;
                rbStartProcessorService.Enabled = false;
                textBoxDispatcherHostname.Enabled = false;
            }
        }

        private void checkBoxStartDispatcher_CheckedChanged(object sender, EventArgs e)
        {
            settings.StartDispatcherService = (checkBoxStartDispatcher.CheckState == CheckState.Checked ? true : false);
            settings.Save();
            if (checkBoxStartDispatcher.Checked)
            {
                groupBoxDispatcherStartAs.Enabled = true;
                buttonStart.Enabled = true;
                rbStartDispatcherConsole.Enabled = true;
                rbStartDispatcherService.Enabled = true;
            }
            else
            {
                if (checkBoxStartWorker.Checked)
                {
                    buttonStart.Enabled = true;
                }
                else
                {
                    buttonStart.Enabled = false;
                }
                groupBoxDispatcherStartAs.Enabled = false;
                rbStartDispatcherConsole.Enabled = false;
                rbStartDispatcherService.Enabled = false;
            }
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            MinimizeToTray = false;
            this.Close();
        }

        private void toolStripMenuItemRestore_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void ServicesControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            switch (e.CloseReason)
            {
                case CloseReason.ApplicationExitCall:
                    break;
                case CloseReason.FormOwnerClosing:
                    break;
                case CloseReason.MdiFormClosing:
                    break;
                case CloseReason.None:
                    break;
                case CloseReason.TaskManagerClosing:
                    break;
                case CloseReason.UserClosing:
                    if (MinimizeToTray)
                    {
                        e.Cancel = true;
                        SendToTray();
                    }
                    break;
                case CloseReason.WindowsShutDown:
                    break;
                default:
                    break;
            }
        }

        private void SendToTray()
        {
            // Minimizes window and cancels form closing
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.notifyIcon1.BalloonTipText = "Double-click to restore Services control panel window.";
            this.notifyIcon1.ShowBalloonTip(1);
        }

        private void ServicesControlPanel_Shown(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            var thisIcon = (NotifyIcon)sender;
            thisIcon.Visible = false;
            thisIcon.Dispose();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                SendToTray();
            }

        }

        private void textBoxDispatcherHostname_TextChanged(object sender, EventArgs e)
        {
            settings.DispatcherHostName = textBoxDispatcherHostname.Text;
            settings.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MinimizeToTray = false;
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsDlg settingsDlg = new SettingsDlg();
            settingsDlg.StartupType = settings.StartupType;
            settingsDlg.LogonAs = settings.LogonAs;
            settingsDlg.Password = settings.Password;
            settingsDlg.InteractWithDesktop = settings.InteractWithDesktop;
            DialogResult result = settingsDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.StartupType = settingsDlg.StartupType;
                settings.LogonAs = settingsDlg.LogonAs;
                settings.Password = settingsDlg.Password;
                settings.InteractWithDesktop = settingsDlg.InteractWithDesktop;
                settings.Save();
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
