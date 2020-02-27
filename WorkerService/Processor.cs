using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorService
{
    class Processor
    {
        private void CreateBatchFile(string batchFile, string settingFile, bool pause)
        {
            try
            {
                string tempDir = settings.SETTINGS_TEMP_DIR;
                String batchCmd = null;
                String javaCmd = Path.Combine(settings.SETTINGS_NUIX_PATH, "jre\\bin\\java");
                String licenseServer = GetLicenseServer();
                String classPath = String.Format(".;{0};{1}", Path.Combine(settings.NUIX_SDK_PATH, "lib\\*"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Nuix-Processing.jar"));

                // Use a different batch command depending on whether or not the licenseServer has a value
                if (!String.IsNullOrEmpty(licenseServer))
                    batchCmd = String.Format("\"{0}\" -Xms{1} -Xmx{2} -Djava.io.tmpdir=\"{3}\" -Dnuix.loglevel={4} -Dnuix.registry.servers={5} -cp \"{6}\" com.aon.sf.processing.ProcessController \"{7}\"", javaCmd, settings.BATCH_JAVA_XMS, settings.BATCH_JAVA_XMX, tempDir, settings.BATCH_JAVA_LOGLEVEL, licenseServer, classPath, settingFile);
                else
                    batchCmd = String.Format("\"{0}\" -Xms{1} -Xmx{2} -Djava.io.tmpdir=\"{3}\" -Dnuix.loglevel={4} -cp \"{5}\" com.aon.sf.processing.ProcessController \"{6}\"", javaCmd, settings.BATCH_JAVA_XMS, settings.BATCH_JAVA_XMX, tempDir, settings.BATCH_JAVA_LOGLEVEL, classPath, settingFile);

                if (pause)
                {
                    batchCmd += System.Environment.NewLine + "pause";
                }

                Debug.WriteLine("Command = " + batchCmd, 3);
                Debug.WriteLine("Writing batch file, " + batchFile, 3);
                File.WriteAllText(batchFile, batchCmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error was detected trying to create the batch command. " + ex.Message + "\n\nThe application will exit when you click OK.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("An error was detected trying to create the batch command. " + ex.Message, 10);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Enumerates the license server from the SETTINGS_LICENCESERVER setting.
        /// </summary>
        /// <returns>A string containing the license server</returns>
        private String GetLicenseServer()
        {
            String licenseServer = null;
            String licenseServerARX = null;
            String licenseServerProduction = null;

            String licenseServerSetting = settings.SETTINGS_LICENCESERVER;

            String licenseServerSettingARX = "";
            if (!string.IsNullOrEmpty(settings.SETTINGS_ARX_LICENCESERVER))
                licenseServerSettingARX = settings.SETTINGS_ARX_LICENCESERVER;

            String licenseServerSettingProduction = "";
            if (!string.IsNullOrEmpty(settings.SETTINGS_PRODUCTION_LICENCESERVER))
                licenseServerSettingProduction = settings.SETTINGS_PRODUCTION_LICENCESERVER;

            try
            {
                // SETTINGS_LICENCESERVER expected to have a value such as "licencesourcetype=server;licencetype=enterprise-workstation;licencesourcelocation=devnuix01:27443"
                String[] temp = licenseServerSetting.Split(';');
                foreach (var property in temp)
                {
                    if (property.StartsWith("licencesourcelocation"))
                        // Set license server to string that starts with "licensesourcelocation"
                        // Parse out the hostname without the port
                        licenseServer = Regex.Split(property, @"[=|]")[1].Split(':')[0];
                }
                if (licenseServerSettingARX.Length > 0)
                {
                    temp = licenseServerSettingARX.Split(';');
                    foreach (var property in temp)
                    {
                        if (property.StartsWith("licencesourcelocation"))
                            // Set license server to string that starts with "licensesourcelocation"
                            // Parse out the hostname without the port
                            licenseServerARX = Regex.Split(property, @"[=|]")[1].Split(':')[0];
                    }
                    licenseServer = licenseServer + " " + licenseServerARX;
                }
                if (licenseServerSettingProduction.Length > 0)
                {
                    temp = licenseServerSettingProduction.Split(';');
                    foreach (var property in temp)
                    {
                        if (property.StartsWith("licencesourcelocation"))
                            // Set license server to string that starts with "licensesourcelocation"
                            // Parse out the hostname without the port
                            licenseServerProduction = Regex.Split(property, @"[=|]")[1].Split(':')[0];
                    }
                    licenseServer = licenseServer + " " + licenseServerProduction;
                }

                licenseServer = "\"" + licenseServer + "\"";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Failed to parse license server hostname from License Server setting, {0}.", licenseServerSetting), 9);
                Debug.WriteLine(ex.Message, 9);
            }

        }
    }
