using System;
using System.Configuration.Install;
using System.Reflection;

namespace DispatcherService
{
    public static class Installer
    {
        public static InstallerOutput Install()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                // Check for user privileges
                return new InstallerOutput() { Status=false, Message=ex.Message + " " + ex.InnerException.Message};
            }
            return new InstallerOutput() { Status = true, Message = "Succesfully installed" }; 
        }

        public static InstallerOutput Uninstall()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { "/u", Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                return new InstallerOutput() { Status = false, Message = ex.Message + " " + ex.InnerException.Message };
            }
            return new InstallerOutput() { Status = true, Message = "Succesfully uninstalled" };
        }
    }

    public class InstallerOutput
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
