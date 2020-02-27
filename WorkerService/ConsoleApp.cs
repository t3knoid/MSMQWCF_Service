using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkerService
{
    public partial class ConsoleApp : Form
    {
        /// <summary>
        /// The StartInfo.Command property is set to this value
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// The StartInfo.Arguments property is set to this value
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// Set to true to run ConsoleApp window in modal mode
        /// </summary>
        public bool Modal { get; set; }
        /// <summary>
        /// This provides the exit code of the application that was executed
        /// </summary>
        public int ExitCode { get; set; }
        /// <summary>
        /// When set to true, this will make the ConsoleApp window to close automatically
        /// </summary>
        public bool AutoClose { get; set; }
        /// <summary>
        /// When set to true, the user is prompted when the close button is clicked 
        /// </summary>
        public bool ClosePrompt { get; set; }
        private Process process = null;
        private int processID = 0;
        private bool hasExited = false;
        private bool closeButtonClicked = false;

        /// <summary>
        /// Make sure to set the command if using this default contructor.
        /// </summary>
        public ConsoleApp()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Set the command and arguments using this constructor
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="arguments">The argument to pass to the command</param> 
        /// <param name="modal">Set to true to create a modal dialog</param>
        /// <param name="checkCommandPath">Set to true if the command path exists</param>
        /// <param name="closeprompt">Set to true to prompt user after clicking close button</param>
        public ConsoleApp(string command, string arguments = "", bool autoclose = false, bool modal = false, bool checkCommandPath = true, bool closeprompt = false)
        {
            this.Command = command;
            this.Arguments = arguments;
            this.Modal = modal;
            this.AutoClose = autoclose;
            this.ClosePrompt = closeprompt;
            InitializeComponent();
            if (modal)
                Start(checkCommandPath);
        }
        /// <summary>
        /// Set the command and modal setting
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="modal">Set to true to create a modal dialog</param>
        /// <param name="checkCommandPath">Set to true if the command path exists</param>
        /// <param name="closeprompt">Set to true if the user is prompted after clicking close button</param>
        public ConsoleApp(string command, bool autoclose = false, bool modal = false, bool checkCommandPath = true, bool closeprompt = false)
        {
            this.Command = command;
            this.Modal = modal;
            this.AutoClose = autoclose;
            this.ClosePrompt = closeprompt;
            InitializeComponent();
            if (this.Modal)
                Start(checkCommandPath);
        }
        /// <summary>
        /// Starts the specified command
        /// </summary>
        /// <returns>true if the command completed successfully</returns>
        public bool Start(bool checkCommandPath = true)
        {
            if (checkCommandPath && !File.Exists(this.Command))
                throw new FileNotFoundException(String.Format("{0} not found.", this.Command));

            this.process = new Process();
            try
            {
                process.StartInfo.FileName = this.Command;
                process.StartInfo.Arguments = this.Arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true; // Is a MUST!
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceivedHandler);
                process.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_ErrorDataReceivedHandler);
                process.Exited += new EventHandler(process_ExitedHandler);
                Console.WriteLine(String.Format("Starting command {0}.", process.StartInfo.FileName), 9);
                Console.WriteLine(String.Format("Command arguments: {0}", process.StartInfo.Arguments), 9);
                process.Start();
                processID = process.Id;
                Console.WriteLine(String.Format("Process ID = {0}.", processID), 9);
                // Asynchronously read the standard output of the spawned process. 
                // This raises OutputDataReceived events for each line of output.
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                KillProcess();
                Debug.WriteLine(String.Format("Error launching {0}. {1}", this.Command, ex.Message), 9);
                throw new Exception(String.Format("Error launching {0}. {1}", this.Command, ex.Message));
            }

            // Use a separate thread to look for the console exit
            // This will allow the ConsoleApp textbox to be responsive
            new Thread(delegate()
            {
                WaitforConsoleAppExit();
            }).Start();

            return true;
        }
        /// <summary>
        /// This method is executed in a separate thread to wait for the 
        /// process to exit.
        /// </summary>
        /// <param name="process"></param>
        private void WaitforConsoleAppExit()
        {
            Console.WriteLine("WaitforConsoleAppExit starting", 9);
            process.WaitForExit();
            while (!process.HasExited) ;
            if (process != null && hasExited)
            {
                try
                {
                    this.ExitCode = process.ExitCode;
                    Console.WriteLine(String.Format("Process with PID={0} has exit code of {1}.", this.processID, this.ExitCode), 9);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(String.Format("Failed to set ExitCode property in ConsoleApp dialog. The process has not exited or the process handle is invald. {0}", ex.Message), 9);
                }
                process.Close();
                Console.WriteLine(String.Format("Process with PID={0} closed", this.processID), 9);
            }
            // Need to use Dispatcher.Invoke to access control from calling ConsoleApp main thread
            this.BeginInvoke((Action)(() =>
            {
                this.btClose.Enabled = true;
                if (AutoClose)
                {
                    this.Close();
                }
            }));
            Console.WriteLine("WaitforConsoleAppExit exiting", 9);
        }
        /// <summary>
        /// A handler that will be called win the process exits.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void process_ExitedHandler(object sender, System.EventArgs e)
        {
            hasExited = true;
        }
        /// <summary>
        /// Error data receiver handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void process_ErrorDataReceivedHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        tbConsoleOutput.AppendText(string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Data) + Environment.NewLine);
                    }));
                }
                
            }
        }
        /// <summary>
        /// Output data receiver handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void process_OutputDataReceivedHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        //tbConsoleOutput.AppendText(string.Format("[{0}][{1}] {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), process.ProcessName, e.Data) + Environment.NewLine);
                        tbConsoleOutput.AppendText(string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Data) + Environment.NewLine);
                    }));
                }
            }
        }
        /// <summary>
        /// Helper method for the DataReceivedEventHandler which updates the updatetbConsoleOutput textbox.
        /// </summary>
        /// <param name="s">String from the DataReceivedEventHandler</param>
        private void updateConsoleOutput(string s, bool isErrorOutput)
        {
            if (isErrorOutput)
            {
                Console.WriteLine(String.Format("Command line process message: {0}", s), 9);
            }

            tbConsoleOutput.AppendText(s + Environment.NewLine);
        }
        /// <summary>
        /// Close button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClose_Click(object sender, EventArgs e)
        {
            this.closeButtonClicked = true;
            this.Close();
        }

        /// <summary>
        /// Checks if the associated process exists
        /// </summary>
        /// <returns></returns>
        private bool ProcessExists()
        {
            return Process.GetProcesses().Any(x => x.Id == processID);
        }
        /// <summary>
        /// Kills the associated process
        /// </summary>
        private void KillProcess()
        {
            if (ProcessExists())
            {
                Process p = Process.GetProcessById(processID);
                p.Kill();
                Console.WriteLine(String.Format("Forcibly killed PID {0}.", this.processID), 9);
            }
        }

        private void ConsoleApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void ConsoleApp_Load(object sender, EventArgs e)
        {

        }

    }
}
