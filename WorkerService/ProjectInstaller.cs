using Microsoft.Win32;
using System.ComponentModel;

namespace WorkerService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                RegistryKey servicesKey = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\");
                RegistryKey serviceKey = servicesKey.OpenSubKey(serviceInstaller1.ServiceName, true);
                serviceKey.SetValue("ImagePath", (string)serviceKey.GetValue("ImagePath") + " dispatcher= " + Context.Parameters["dispatcher"]);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Failed to update service");
            }
        }

    }
}
