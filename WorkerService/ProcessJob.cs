using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QueueLib;

namespace WorkerService
{
    class ProcessJob
    {
        public IDictionary<QueueLib.SettingsKeys, string> Settings { get; set; }
        public string ProjectPath { get; set; }
        public string ProcessSettingsFile { get; set; }
        public string JobFile { get; set; }
        public string BatchFile { get; set; }
        public ProcessTypes ProcessType { get; set; }
        public string ProjectName { get; set; }
        public ProcessJob()
        {}

        public void Initialize()
        { 

        }
        public void CreateBatchFile()
        {
            try
            {
                string tempDir = Settings[SettingsKeys.SETTINGS_TEMP_DIR];
                String batchCmd = null;
                String javaCmd = Path.Combine(Settings[SettingsKeys.SETTINGS_NUIX_PATH], "jre\\bin\\java");
                String licenseServer = GetLicenseServer();
                String classPath = String.Format(".;{0};{1}", Path.Combine(Settings[SettingsKeys.NUIX_SDK_PATH], "lib\\*"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Nuix-Processing.jar"));

                // Use a different batch command depending on whether or not the licenseServer has a value
                if (!String.IsNullOrEmpty(licenseServer))
                {
                    batchCmd = String.Format("\"{0}\" -Xms{1} -Xmx{2} -Djava.io.tmpdir=\"{3}\" -Dnuix.messaging.addressSeparator=\";\" -Dnuix.loglevel={4} {5} -Dnuix.registry.servers={6} -Dnuix.data.mapi.keepTransportMessageHeader=true -Dnuix.storage.stores.useFileSystemBinaryStore=true -Dnuix.storage.stores.fileSystemBinaryStoreLocation=\"{7}\" -cp \"{8}\" com.aon.sf.processing.ProcessController \"{9}\"", javaCmd, Settings[SettingsKeys.BATCH_JAVA_XMS], Settings[SettingsKeys.BATCH_JAVA_XMX], tempDir, Settings[SettingsKeys.BATCH_JAVA_LOGLEVEL], Settings[SettingsKeys.BATCH_JAVA_OTHER_OPTIONS], licenseServer, ProjectPath, classPath, JobFile);
                }
                else
                {
                    batchCmd = String.Format("\"{0}\" -Xms{1} -Xmx{2} -Djava.io.tmpdir=\"{3}\" -Dnuix.messaging.addressSeparator=\";\" -Dnuix.loglevel={4} {5} -cp \"{6}\" com.aon.sf.processing.ProcessController \"{7}\"", javaCmd, Settings[SettingsKeys.BATCH_JAVA_XMS], Settings[SettingsKeys.BATCH_JAVA_XMX], tempDir, Settings[SettingsKeys.BATCH_JAVA_LOGLEVEL], Settings[SettingsKeys.BATCH_JAVA_OTHER_OPTIONS], classPath, JobFile);
                }
                File.WriteAllText(BatchFile, batchCmd);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void MoveLocalExportFilesToProjectDir()
        {
            //string targerFolderPath = mainWindow.tbCasePathExport.Text + "\\" + CommandLines.CASE_Export;  ("K:\\SDProjects\\FNX1010B_SMOKE\\Export")
            //string projectLocalPath = SQLiteQueries.GetProjectLocalPath(mainWindow.cbProjectsList.Text); ("E:\\NUIX-X\\Projects\\FNX1010B_SMOKE")
            //string sourceFolderPath = projectLocalPath + "\\" + CommandLines.CASE_Export;  ({E:\NUIX-X\Projects\FNX1010B_SMOKE\Export})

            //int legacyDataItemCount = SQLiteQueries.GetProjectLegacyDataItemCount();


            //DirectoryInfo sourceFolderInfo = new DirectoryInfo(sourceFolderPath); 
            //if (sourceFolderInfo.Exists && (sourceFolderInfo.GetFiles().Length > 0 || sourceFolderInfo.GetDirectories().Length > 0) && Directory.Exists(targerFolderPath))
            //{
            //    Debug.WriteLine("Robocopy exported files", 9);
            //    mainWindow.robocopyMoveDirectory(sourceFolderPath, targerFolderPath);

            //    Debug.WriteLine("Update export files database table", 9);
            //    int caseID = SQLiteQueries.GetCaseID(caseName);
            //    SQLiteQueries.UpdateExportFilePath(caseID, sourceFolderPath, targerFolderPath);

            //    if (sourceFolderInfo.Exists)
            //    {
            //        try
            //        {
            //            Debug.WriteLine("Start deleting export subdirectories", 9);
            //            foreach (DirectoryInfo dirInfo in sourceFolderInfo.GetDirectories())
            //            {
            //                try
            //                {
            //                    dirInfo.Delete(true);
            //                }
            //                catch (Exception ex)
            //                {
            //                    Debug.WriteLine("Error deleting export subdirectory: " + dirInfo.FullName + " - " + ex.Message, 9);
            //                }
            //            }
            //            Debug.WriteLine("Finish deleting export subdirectories", 9);
            //        }
            //        catch (Exception ex)
            //        {
            //            Debug.WriteLine("Error deleting export subdirectories: " + sourceFolderPath + " - " + ex.Message, 9);
            //        }
            //    }
            //}

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

            String licenseServerSetting = Settings[SettingsKeys.SETTINGS_LICENCESERVER];

            String licenseServerSettingARX = "";
            if (!string.IsNullOrEmpty(Settings[SettingsKeys.SETTINGS_ARX_LICENCESERVER]))
                licenseServerSettingARX = Settings[SettingsKeys.SETTINGS_ARX_LICENCESERVER];

            String licenseServerSettingProduction = "";
            if (!string.IsNullOrEmpty(Settings[SettingsKeys.SETTINGS_PRODUCTION_LICENCESERVER]))
                licenseServerSettingProduction = Settings[SettingsKeys.SETTINGS_PRODUCTION_LICENCESERVER];

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
                //Debug.WriteLine(String.Format("Failed to parse license server hostname from License Server setting, {0}.", licenseServerSetting), 9);
                //Debug.WriteLine(ex.Message, 9);
            }

            return licenseServer;
        }

        internal void CreateSettingsFile(string p)
        {
            try
            {
                string d = Path.GetDirectoryName(ProcessSettingsFile);
                if (!Directory.Exists(d))
                {
                    System.IO.Directory.CreateDirectory(d);
                }
                System.IO.File.WriteAllText(ProcessSettingsFile, p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CreateJobFile(string p)
        {
            try
            {
                string d = Path.GetDirectoryName(JobFile);
                
                
                if (!Directory.Exists(d))
                {
                    System.IO.Directory.CreateDirectory(d);
                }
                System.IO.File.WriteAllText(JobFile, p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
