
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Runtime.InteropServices;
using System.Configuration;
using DispatcherClientLib;
using QueueLib;

namespace WorkerService
{
    public partial class WorkerService : ServiceBase
    {
        System.Timers.Timer processorTimer;
        System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
        bool Interactive = false;

        // Service pending statuses
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        private string dispatcherHostname = "localhost";
        private Worker worker;
        //private DispatcherClient client; // connection to Dispatcher
        Guid workerRegistrationID = Guid.Empty; // This tracks the worker

        public WorkerService()
        {
            InitializeComponent();
            string eventSourceName = "Worker Service";
            string logName = "Worker Service Log";

            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    eventSourceName, logName);
            }
            eventLog.Source = eventSourceName;
            eventLog.Log = logName;
        }

        internal void Run(string[] args)
        {
            Interactive = true;
            this.OnStart(args);
            Console.WriteLine("Running interactive mode. Press any key to stop service.");
            Console.ReadKey();
            OnStop();
        }
        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Starting Worker service.");
            eventLog.WriteEntry("Starting Worker service.");
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Use Timer.Elapsed to perform worker work at the interval value 
            // specified in processorTimer.Interval
            processorTimer = new System.Timers.Timer();
            processorTimer.Elapsed += new ElapsedEventHandler(RunWorkerTask);
            processorTimer.Interval = 5000; // Trigger every 5 seconds

            // Have the timer fire repeated events (true is the default)
            processorTimer.AutoReset = true;

            if (CommandLineArgsParser.HasArgument(args, "dispatcher"))
            {
                dispatcherHostname = CommandLineArgsParser.GetArgumentValue(args, "dispatcher");
                eventLog.WriteEntry("Setting Dispatcher setting to " + dispatcherHostname);
                Console.WriteLine("Setting Dispatcher setting to " + dispatcherHostname);
            }
            // Start services here

            // Register to the Dispatcher
            eventLog.WriteEntry("Registering Worker into the worker queue.");
            Console.WriteLine("Registering Worker into the worker queue.");
            worker = new Worker()
            {
                Hostname = System.Net.Dns.GetHostName(),
                Sent = DateTime.Now,
            };

            DispatcherClient client = new DispatcherClient(dispatcherHostname);  // Initialize client connection to Dispatcher service
            DispatcherResult result = client.RegisterWorker(worker);
            
            if (result.Success)
            {
                // The registration was successful. Grab the registration
                Guid.TryParse(result.Message, out workerRegistrationID);
                worker.RegistrationID = workerRegistrationID; // Set the RegistrationID here. This is used to created/identify the worker queue.
                eventLog.WriteEntry("Successfully registered worker to Dispatcher service. ProcessorRegistrationID=" + workerRegistrationID.ToString());
                Console.WriteLine("Successfully registered worker to Dipatcher service. ProcessorRegistrationID=" + workerRegistrationID.ToString());
            }
            else
            {
                eventLog.WriteEntry("Failed to register worker to Dispatcher service. Exiting.", EventLogEntryType.Error);
                Console.WriteLine("Failed to register worker to Dispatcher service. Exiting.");
                return;
            }

            // Start the timer
            Console.WriteLine("Starting timer event.");
            eventLog.WriteEntry("Starting timer event.");
            processorTimer.Enabled = true;

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stopping Worker service.");
            Console.WriteLine("Stopping Worker service.");

            ServiceStatus serviceStatus = new ServiceStatus();
            if (!Interactive)
            {
                // Update the service state to Stop Pending.
                serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
                serviceStatus.dwWaitHint = 100000; // Give it 10 seconds to stop
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            }

            // Stop Worker thread if running
            Console.WriteLine("Stopping worker worker thread");
            eventLog.WriteEntry("Stopping worker worker thread");
            if (backgroundWorker.IsBusy == true)
                backgroundWorker.CancelAsync();

            // Stop the worker timer
            processorTimer.Stop();
            processorTimer.Dispose();

            if (!Interactive)
            {
                // Update the service state to Stopped.
                serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            }
            Console.WriteLine("Worker service stopped.");
            eventLog.WriteEntry("Worker service stopped.");
        }

        protected override void OnContinue()
        {
            eventLog.WriteEntry("In OnContinue.");
        }

        protected override void OnShutdown()
        {
            // When shutting down a worker, there needs a way
            // to reassign jobs that are still in a worker's queue
            eventLog.WriteEntry("In OnShutdown.");
            // Stop Worker thread if running
            if (backgroundWorker.IsBusy == true)
                backgroundWorker.CancelAsync();
        }

        /// <summary>
        /// This method runs worker task when the worker timer elapsed event is triggered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunWorkerTask(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(String.Format("The Elapsed event was raised at {0}", e.SignalTime));
            eventLog.WriteEntry(String.Format("The Elapsed event was raised at {0}", e.SignalTime));
            eventLog.WriteEntry("Running Worker task");
            Console.WriteLine("Running Worker task");

            if (backgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker.RunWorkerAsync();
            }

        }
        /// <summary>
        /// Event handler that starts the worker background thread event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            if (bw.CancellationPending == true) return;

            Console.WriteLine("Stopping timer event.");
            eventLog.WriteEntry("Stopping timer event.");
            processorTimer.Stop();  // Stop the timer so that no other job can be triggered
            DispatcherClient client = new DispatcherClient(dispatcherHostname);  // Initialize client connection to Dispatcher service
            DispatcherResult result = client.RequestJob(this.worker);
            if (result.Success)
            {
                Console.WriteLine(String.Format("Job request acknowledged. Should now check worker queue."));
                eventLog.WriteEntry(String.Format("Job request acknowledged. Should now check worker queue."));

                if (dispatcherHostname == "localhost" || dispatcherHostname == "127.0.0.1")
                {
                    dispatcherHostname = ".";  // Need to make sure to use "." if the queue is local
                }

                Job job = null;
                WorkerQueue processorQueue;

                try 
                {
                    processorQueue = new WorkerQueue(worker, dispatcherHostname);
                    // Check if there is something in the worker queue
                    if (processorQueue.Count != 0)
                    {
                        job = processorQueue.ReadMessage();
                        if (job != null)
                        {
                            Console.WriteLine(String.Format("Job {0} found of type {1} from project {2} requested by {3} using workstation {4}. Processing.",job.JobID, job.JobType, job.Project, job.Investigator, job.RequestingHost));
                            eventLog.WriteEntry(String.Format("Job {0} found of type {1} from project {2} requested by {3} using workstation {4}. Processing.", job.JobID, job.JobType, job.Project, job.Investigator, job.RequestingHost));
                            
                            // Convert app settings to a dictionary
                            var settings = job.AppSettings.ToDictionary(x => x.Key, y => y.Value);

                            // Files that need to be created before processing can execute
                            string settingsFile = settings[SettingsKeys.SETTINGS_SETTINGS_PATH] + "\\" + job.Project + "\\" + job.Project + "\\" + job.JobType + "_" + string.Format("{0:yyyyMMdd_HHmmss}_", job.Sent) + job.JobID + ".txt";
                            string jobFile = string.Format(settings[SettingsKeys.SETTINGS_SETTINGS_PATH] + "\\{0:yyyyMMdd_HHmmss}_" + job.JobID + "_NUIXJOBS.txt", job.Sent);
                            string batchFile = string.Format(settings[SettingsKeys.SETTINGS_SETTINGS_PATH] + "\\{0:yyyyMMdd_HHmmss}_" + job.JobID + "_NUIXJOBS.bat", job.Sent);
                            
                            ProcessJob processJob = new ProcessJob()
                            {
                                ProcessSettingsFile = settingsFile,
                                Settings = settings,
                                ProjectName = job.Project,
                                JobFile = jobFile,
                                BatchFile = batchFile,
                                ProcessType = job.JobType,
                                ProjectPath = job.ProjectPath,
                            };

                            // Create all necessary files

                            Console.WriteLine(string.Format("Writing settings file {0}.",settingsFile));
                            eventLog.WriteEntry("Writing settings file.");
                            try
                            {
                                processJob.CreateSettingsFile(job.SettingsFileContent);
                                Console.WriteLine(string.Format("Writing settings file {0}.", settingsFile));
                                eventLog.WriteEntry("Writing settings file OK.");
                                Console.WriteLine("Writing settings file OK.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(string.Format("Failed writing settings file. {0}",ex.Message));
                                eventLog.WriteEntry(string.Format("Failed writing settings file. {0}", ex.Message), EventLogEntryType.Error);
                                return;
                            }
                            Console.WriteLine(string.Format("Writing job file {0}.", jobFile));
                            eventLog.WriteEntry("Writing job file.");
                            try
                            {
                                processJob.CreateJobFile(job.JobFileContent);
                                eventLog.WriteEntry("Writing job file OK.");
                                Console.WriteLine("Writing job file OK.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(string.Format("Failed writing job file. {0}", ex.Message));
                                eventLog.WriteEntry(string.Format("Failed writing job file. {0}", ex.Message), EventLogEntryType.Error);
                                return;
                            }
                            Console.WriteLine(string.Format("Writing batch file {0}.", batchFile));
                            eventLog.WriteEntry("Writing batch file.");
                            try
                            {
                                processJob.CreateBatchFile();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(string.Format("Failed  writing batch file. {0}", ex.Message));
                                eventLog.WriteEntry(string.Format("Failed writing batch file. {0}", ex.Message), EventLogEntryType.Error);
                                return;
                            }

                            // Execute batch file
                            bool autoClose = true;
                            bool modal = true;
                            ConsoleApp consoleapp = new ConsoleApp(batchFile, autoClose, modal);
                            consoleapp.ShowDialog();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No jobs available in worker queue.");
                        eventLog.WriteEntry("No jobs available in worker queue.");
                    }
                }
                catch (Exception ex)
                {
                    eventLog.WriteEntry(String.Format("Error reading from worker queue. Worker Hostname = {0}, ProcessorID={1}. {2}", worker.Hostname, worker.RegistrationID.ToString(), ex.Message), EventLogEntryType.Error);
                    Console.Write(String.Format("Error reading from worker queue. Worker Hostname = {0}, ProcessorID={1}. {2}", worker.Hostname, worker.RegistrationID.ToString(), ex.Message));
                    if (job != null)
                    {
                        // put job back into the queue
                        eventLog.WriteEntry(String.Format("Adding job {0} back to job queue", job.JobID), EventLogEntryType.Error);
                        Console.WriteLine(String.Format("Adding job {0} back to job queue", job.JobID));
                        client = new DispatcherClient(dispatcherHostname);  // Initialize client connection to Dispatcher service
                        result = client.RegisterJob(job);
                    }
                }
                
            }
            else
            {
                eventLog.WriteEntry(String.Format("Job request failed with the message {0}", result.Message), EventLogEntryType.Error);
                Console.WriteLine(String.Format("Job request failed with the message {0}", result.Message));
            }
                     
        }
        /// <summary>
        /// Event handler that runs when the worker stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string message;

            if (e.Error != null)
            {
                message = "Worker Error: " + e.Error.Message + " Worker exiting.";
            }
            else if (e.Cancelled == true)
            {
                message = "Worker stopped. Worker exiting.";
            }
            else
            {
                message = "Worker exited. Worker exiting.";
            }

            eventLog.WriteEntry(message);
            Console.WriteLine(message);

            // Start timer again
            Console.WriteLine("Starting timer event.");
            eventLog.WriteEntry("Starting timer event.");
            processorTimer.Enabled = true;
        }

    }

    public static class CommandLineArgsParser
    {
        public static bool HasArgument(this IEnumerable<string> args, string arg)
        {
            return args.Any(a => a.StartsWith(arg));
        }

        public static string GetArgumentValue(this IEnumerable<string> args, string argumentName)
        {
            var arg = args.FirstOrDefault(a => a.StartsWith(argumentName));
            if (arg == null)
            {
                return null;
            }

            return arg.Split('=').Last();
        }
    }

}
