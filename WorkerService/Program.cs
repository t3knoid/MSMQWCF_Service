using System;
using System.ServiceProcess;

namespace WorkerService
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
                WorkerService workerService = new WorkerService();
                workerService.Run(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new WorkerService()                            
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

    }


}
