using System;
using System.Diagnostics;
using System.Threading;

namespace WorkerService
{
    public class RunScript
    {
        /// <summary>
        /// Set to the batch command to execute
        /// </summary>
        public string Command { set; get; }
        /// <summary>
        /// Set to the parameter to send to the batch command
        /// </summary>
        public string Param { set; get; }
        /// <summary>
        /// This property is set to the exit code returned
        /// from the batch commmand
        /// </summary>
        public int ExitCode { set; get; }

        int exitCode = 0;
        public RunScript()
        { }
        /// <summary>
        /// Executes the given command. Make sure to initialize the
        /// Command and Params properties.
        /// </summary>
        private void Run(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Command))
            {
                throw new ArgumentException("Command is not initialized.");
            }
            if (!System.IO.File.Exists(@Command))
            {
                throw new ArgumentException("Command not found.");
            }
            Thread thread = new Thread(Start);
            thread.Start();
        }
        /// <summary>
        /// Start the process
        /// </summary>
        private void Start()
        {
            Process process;
            try
            {
                process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + Command + " \"" + Param + "\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;

                process.ErrorDataReceived += process_DataReceived;
                process.OutputDataReceived += process_DataReceived;
                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
                exitCode = process.ExitCode;
                process.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data + Environment.NewLine);
        }
    }

}
