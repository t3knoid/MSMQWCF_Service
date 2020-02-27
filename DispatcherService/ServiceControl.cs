using System;
using System.ServiceProcess;

namespace DispatcherService
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
                DispatcherService dispatcherService = new DispatcherService();
                ServiceController sc = new ServiceController();
                sc.ServiceName = dispatcherService.ServiceName;
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Stops the Worker Service
        /// </summary>
        public static void Stop()
        {
            try
            {
                DispatcherService dispatcherService = new DispatcherService();
                ServiceController sc = new ServiceController(dispatcherService.ServiceName);
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
                DispatcherService dispatcherService = new DispatcherService();
                foreach (ServiceController sc in ServiceController.GetServices())
                {
                    if (sc.ServiceName == dispatcherService.ServiceName)
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
                    DispatcherService dispatcherService = new DispatcherService();
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = dispatcherService.ServiceName;
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
                    DispatcherService dispatcherService = new DispatcherService();
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = dispatcherService.ServiceName;
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
