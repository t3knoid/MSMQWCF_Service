using System.Diagnostics;

namespace ServicesCPanel
{
    internal class Console 
    {
        internal int Id { get; set; }
        internal bool Running
        {
            get
            {
                try
                {
                    Process p = Process.GetProcessById(Id);
                    if (p.ProcessName != "Idle")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch 
                {
                    return false;
                }
            }
        }

        internal string Command { get; set; }
        internal string Param { get; set; }
        internal Console(string command, string param)
        {
            Command = command;
            Param = param;
        }

        internal Console()
        {
        }

        internal void Run()
        {
            Process process = new Process();
            process.StartInfo.FileName = Command;
            process.StartInfo.Arguments = Param;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            Id = process.Id;
        }

        internal void Stop()
        {
            try
            {
                Process p = Process.GetProcessById(Id);
                p.Kill();
                Id = 0;
            }
            catch { }
        }
    }
}
