using QueueLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Timers;

namespace DispatcherService
{

    public partial class DispatcherService : ServiceBase
    {
        ServiceHost serviceHost;
        bool Interactive = false;

        System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();

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
     
        public DispatcherService()
        {
            InitializeComponent();
            string eventSourceName = "Dispatcher Service";
            string logName = "Dispatcher Service Log";

            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        eventSourceName, logName);
                }
                eventLog.Source = eventSourceName;
                eventLog.Log = logName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            serviceHost = new ServiceHost(typeof(DispatcherOperations)); // Instantiate the Dispatcher Operation WCF services

        }
        /// <summary>
        /// This method is executed when the service is started interactively (e.g. Windows console). It simulates
        /// running of the service from the command-line by executin gthe OnStart and OnStop service method overrides.
        /// </summary>
        /// <param name="args">Command-line parameters</param>
        internal void Run(string[] args)
        {
            Interactive = true;
            this.OnStart(args);
            Console.WriteLine("Running interactive mode. Press any key to stop service.");
            Console.ReadLine();
            this.OnStop();
        }


        #region Windows Service methods
        /// <summary>
        /// This method is called when the service starts
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // Start services here
            Console.WriteLine("Starting Dispatcher service.");
            eventLog.WriteEntry("Starting Dispatcher service.");

            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Create Job queues
            try
            {
                CreateJobQueue();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Job queue creation failure. Shutting down Dispatcher service. " + ex.Message);
                OnStop();
                return;
            }

            eventLog.WriteEntry("Dispatcher starting.");
            Console.WriteLine("Dispatcher starting");

            try
            {
                serviceHost.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            eventLog.WriteEntry("Dispatcher Service is ready.");
            Console.WriteLine("Dispatcher Service is ready.");
            
            // Start dispatcher thread
            //if (backgroundWorkerDispatcher.IsBusy != true)
            //{
            //    // Start the asynchronous operation.
            //    backgroundWorkerDispatcher.RunWorkerAsync();
            //}

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }
        /// <summary>
        /// This method is called when the service stops.
        /// </summary>
        protected override void OnStop()
        {
            Console.WriteLine("Stopping Dispatcher service.");
            if (backgroundWorkerDispatcher.WorkerSupportsCancellation == true)
            {
                // Cancel the Dispatcher background worker operation.
                backgroundWorkerDispatcher.CancelAsync();
            }

            if (!Interactive)
            {
                // Update the service state to Stop Pending.
                ServiceStatus serviceStatus = new ServiceStatus();
                serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
                serviceStatus.dwWaitHint = 100000; // Give it 10 seconds to stop
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);

                // Update the service state to Stopped.
                serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            }

            eventLog.WriteEntry("Dispatcher service stopped.");
            serviceHost.Close();
            eventLog.WriteEntry("Dispatcher service stopped.");

        }

        #endregion

        #region Dispatcher thread methods
        /// <summary>
        /// Event handler that starts the Dispatcher thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerDispatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (!worker.CancellationPending)
            {
                // Do this work unti we get a cancellation pending signal

                if (worker.CancellationPending)
                {
                    Console.WriteLine("CancellationPending signal detected. Perform Dispatcher service clean up before exiting out of thread first.");

                }
            }
            
        }
        /// <summary>
        /// Event handler that runs when the dispatcher stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerDispatcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string message;

            if (e.Error != null)
            {
                message = "Processor background thread error: " + e.Error.Message + " Dispatcher exiting.";
            }
            else if (e.Cancelled == true)
            {
                message = "Processor background thread stopped. Dispatcher exiting.";
            }
            else
            {
                message = "Processor background thread exited. Dispatcher exiting.";
            }

            eventLog.WriteEntry(message);
            Console.WriteLine(message);

        }

        #endregion

        /// <summary>
        /// A method to create job queues. There are five priority queues that
        /// are created. 
        /// </summary>
        private void CreateJobQueue()
        {
            eventLog.WriteEntry("Creating job queue");
            Console.WriteLine("Creating job queue");
            JobQueue jobqueue = new JobQueue();
            try
            {
                jobqueue.Create(false);
                jobqueue.SetPermissions();
                Console.WriteLine("Creating job queue " + " ok");
                eventLog.WriteEntry("Creating job queue " + " ok");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Creating job queue " + " error. " + ex.Message);
                Console.WriteLine("Creating job queue " + " error. " + ex.Message);
                throw (ex);
            }
        }
    }

}
