using System;
using System.Linq;
using System.ServiceProcess;

namespace WorkerService
{
    public class ServiceControl
    {
        /// <summary>
        /// Starts the Worker Service
        /// </summary>
        public static void Start()
        {
            try
            {
                WorkerService workerService = new WorkerService();
                ServiceController sc = new ServiceController();
                sc.ServiceName = workerService.ServiceName;
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Stops the Worker Service
        /// </summary>
        public static void Stop()
        {
            try
            {
                WorkerService workerService = new WorkerService();
                ServiceController sc = new ServiceController(workerService.ServiceName);
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Installed
        {
            get
            {
                bool serviceExists = false;
                WorkerService workerService = new WorkerService();
                foreach (ServiceController sc in ServiceController.GetServices())
                {
                    if (sc.ServiceName == workerService.ServiceName)
                    {
                        serviceExists = true;
                        break;
                    }
                }
                return serviceExists;
            }
        }

        public static bool Running
        {
            get
            {
                if (Installed)
                {
                    WorkerService workerService = new WorkerService();
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = workerService.ServiceName;
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Stopped
        {
            get
            {
                if (Installed)
                {
                    WorkerService workerService = new WorkerService();
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = workerService.ServiceName;
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
