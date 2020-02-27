using System;
using System.ServiceProcess;

namespace DispatcherService
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            if (Environment.UserInteractive)
            {
                Console.WriteLine("Running interactive mode");
                DispatcherService DispatcherService = new DispatcherService();
                DispatcherService.Run(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new DispatcherService()                            
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

    }


}
