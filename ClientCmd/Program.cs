using DispatcherClientLib;
using QueueLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherClientCmd
{
    /// <summary>
    /// This is a sample application that simulates interaction with the Dispatcher service.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("usage: DispatcherClient.exe operation=[registerprocessor | deregisterprocessor | registerjob | requestjob] [dispatcher=hostname]");
                Environment.Exit(1);
            }           
            string operation = String.Empty;
            string dispatcher = "localhost";

            if (CommandLineArgsParser.HasArgument(args, "operation"))
            {
                operation = CommandLineArgsParser.GetArgumentValue(args, "operation");
            }
            else
            {
                Console.WriteLine("usage: DispatcherClient.exe operation=[registerworker | deregisterworker | registerjob | requestjob] [dispatcher=hostname]");
                Environment.Exit(1);
            }

            if (CommandLineArgsParser.HasArgument(args, "dispatcher"))
            {
                dispatcher = CommandLineArgsParser.GetArgumentValue(args, "dispatcher");
            }

            List<string> operations = new List<string>() { "registerworker", "deregisterworker", "registerjob", "requestjob" };
            if (operations.Exists(op => op.Equals(operation)))
            {
#region operations
                switch (operation)
                {
                    case "registerprocessor":
                        {
                            // Send Worker Registration
                            Console.WriteLine("Registering worker.");
                            DispatcherClient client = new DispatcherClient(dispatcher);
                            string thisHost = System.Net.Dns.GetHostName();
                            Worker worker = new Worker()
                            {
                                Hostname = thisHost,
                                Sent = DateTime.Now,
                            };

                            DispatcherResult result = client.RegisterWorker(worker);
                            if (result.Success)
                            {
                                Console.WriteLine("Successfully registered worker. Assigned ID of " + result.Message);
                            }
                            else
                            {
                                Console.WriteLine("Failed to register worker. " + result.Message);
                            }
                            break;
                        }
                    case "deregisterprocessor":
                        {
                            // Send Processor De-Registration
                            Console.WriteLine("De-registering worker.");
                            DispatcherClient client = new DispatcherClient(dispatcher);
                            string thisHost = System.Net.Dns.GetHostName();
                            Guid guid = Guid.NewGuid();
                            DispatcherResult result = client.DeregisterWorker(thisHost, guid);
                            if (result.Success)
                            {
                                Console.WriteLine("Successfully de-registered worker.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to de-registered processor. " + result.Message);
                            }
                            break;
                        }
                    case "registerjob":
                        {
                            // Send Job Registration
                            Console.WriteLine("Registering job.");
                            string thisHost = System.Net.Dns.GetHostName();
                            DispatcherClient client = new DispatcherClient(dispatcher);

                            // Set up application settings
                            IDictionary<SettingsKeys, string> appsettings = new Dictionary<SettingsKeys, string>();
                            appsettings.Add(SettingsKeys.BATCH_JAVA_LOGLEVEL, "DEBUG");
                            appsettings.Add(SettingsKeys.BATCH_JAVA_OTHER_OPTIONS, "");
                            appsettings.Add(SettingsKeys.BATCH_JAVA_XMS, "-xms1024");
                            appsettings.Add(SettingsKeys.BATCH_JAVA_XMX, "-xmx1024");
                            appsettings.Add(SettingsKeys.NUIX_SDK_PATH, @"C:\SDK");
                            appsettings.Add(SettingsKeys.SETTINGS_ARX_LICENCESERVER, "arx");
                            appsettings.Add(SettingsKeys.SETTINGS_LICENCESERVER, "license");
                            appsettings.Add(SettingsKeys.SETTINGS_SETTINGS_PATH, "path");
                            appsettings.Add(SettingsKeys.SETTINGS_TEMP_DIR, "tempdir");

                            var settingsList = new List<Property<SettingsKeys, string>>();

                            foreach (KeyValuePair<SettingsKeys, string> setting in appsettings)
                            {
                                Property<SettingsKeys, string> prop = new Property<SettingsKeys, string>(setting.Key, setting.Value);
                                settingsList.Add(prop);
                            }

                            DispatcherResult result = client.RegisterJob("Frank", "FR0001", "E:\\NUIX-X\\Jobs\\FNX1010A\\FNX1010A\\Ingest_20190501_102607_2181b401-26fc-46bf-8114-3bfa48307412.txt", "Settings content", "E:\\NUIX-X\\Jobs\\20190502_090325_450c45a5-126c-47f8-b8a5-d217c19210fe_NUIXJOBS.txt", "Job Content", settingsList);
                            if (result.Success)
                            {
                                Console.WriteLine("Successfully registered job. Assigned ID of " + result.Message);
                            }
                            else
                            {
                                Console.WriteLine("Failed to register job. " + result.Message);
                            }
                            break;
                        }
                    case "requestjob":
                        {
                            // Send Processor De-Registration
                            Console.WriteLine("Registering worker.");
                            string thisHost = System.Net.Dns.GetHostName();
                            Guid guid = Guid.NewGuid();  // This is just for testing. Ordinarily, the guid from a prior processor registration is used.

                            DispatcherClient client = new DispatcherClient(dispatcher);

                            Worker worker = new Worker()
                            {
                                Hostname = thisHost,
                                Sent = DateTime.Now,
                            };

                            DispatcherResult result = client.RequestJob(worker);
                            if (result.Success)
                            {
                                Console.WriteLine("Successfully requested a job from the dispatcher.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to request a job from the dispatcher. " + result.Message);
                            }

                            break;
                        }
                }
#endregion
            }
            else
            {
                Console.WriteLine("usage: DispatcherClient.exe operation=[registerprocessor | deregisterprocessor | registerjob | requestjob] [dispatcher=hostname]");
            }
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

