using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QueueLib
{

    [DataContract]
    public enum ProcessTypes
    {
        [EnumMember]
        Ingest,
        [EnumMember]
        Classify,
        [EnumMember]
        Export,
        [EnumMember]
        OCR,
        [EnumMember]
        QC,
        [EnumMember]
        TAG,
    }

    [DataContract]
    public enum JobPriority
    {
        [EnumMember]
        Highest = 7,        // Highest message priority
        [EnumMember]
        VeryHigh = 6,       // Between Highest and High message priority
        [EnumMember]
        High = 5,           // High message priority.
        [EnumMember]
        AboveNormal = 4,    // Between High and Normal message priority
        [EnumMember]  
        NORMAL = 3,         // 	Normal message priority
        [EnumMember]
        Low = 2,            // Low message priority
        [EnumMember]
        VeryLow = 1,        // Between Low and Lowest message priority
        [EnumMember]
        Lowest = 0,         // Lowest message priority
    }

    [DataContract]
    public enum SettingsKeys
    {
        [EnumMember]
        SETTINGS_SETTINGS_PATH,
        [EnumMember]
        SETTINGS_TEMP_DIR,
        [EnumMember]
        NUIX_SDK_PATH,
        [EnumMember]
        SETTINGS_LICENCESERVER,
        [EnumMember]
        SETTINGS_ARX_LICENCESERVER,
        [EnumMember]
        SETTINGS_PRODUCTION_LICENCESERVER,
        [EnumMember]
        BATCH_JAVA_XMS,
        [EnumMember]
        BATCH_JAVA_XMX,
        [EnumMember]
        BATCH_JAVA_LOGLEVEL,
        [EnumMember]
        SETTINGS_NUIX_PATH,
        [EnumMember]
        BATCH_JAVA_OTHER_OPTIONS,
    }

    [Serializable]
    [DataContract]
    public struct Property<K, V>
    {
        [DataMember]
        public K Key { get; set; }
        [DataMember]
        public V Value { get; set; }
        public Property(K key, V value) : this()
        {
            this.Key = key;
            this.Value = value;
        }
    }

    /// <summary>
    /// Message for a given job in the job queue
    /// </summary>
    [DataContract]
    public class Job 
    {
        /// <summary>
        /// Date and time message was sent
        /// </summary>
        [DataMember]
        public DateTime Sent { get; set; }
        /// <summary>
        /// None-serialized unique job identifier. 
        /// </summary>
        [DataMember]
        public Guid JobID { get; set; }
        /// <summary>
        /// The name of the project the job belongs to.
        /// </summary>
        [DataMember]
        public string Project { get; set; }
        /// <summary>
        /// The hostname of where the request came from
        /// </summary>
        [DataMember]
        public string Investigator { get; set; }
        /// <summary>
        /// The hostname of where the request comes from.
        /// </summary>
        [DataMember]
        public string RequestingHost { get; set; }
        /// <summary>
        /// The job priority. Set from 1-5 with 5 being the highest priority.
        /// </summary>
        [DataMember]
        public JobPriority Priority { get; set; }
        /// <summary>
        /// This is where the settings file gets created.
        /// </summary>
        [DataMember]
        public string SettingsFileContent { get; set; }
        /// <summary>
        /// This is the content of the job file.
        /// </summary>
        [DataMember]
        public string JobFileContent { get; set; }
        /// <summary>
        /// This contains the path to the project. This is typically located in the SQLite 
        /// </summary>
        [DataMember]
        public string ProjectPath { get; set; }
        /// <summary>
        /// Identifies what type of process this is.
        /// </summary>
        [DataMember]
        public ProcessTypes JobType { get; set; }
        /// <summary>
        /// This is a collection of application settings. Use key names with the corresponding properties in the 
        /// application settings. See Properties.Settings.settings for reference. It expects the following 
        /// keys with it's corresponding values:
        /// 
        /// SETTINGS_SETTINGS_PATH
        /// SETTINGS_TEMP_DIR
        /// NUIX_SDK_PATH
        /// SETTINGS_LICENCESERVER
        /// SETTINGS_ARX_LICENCESERVER
        /// SETTINGS_PRODUCTION_LICENCESERVER
        /// BATCH_JAVA_XMS
        /// BATCH_JAVA_XMX
        /// BATCH_JAVA_LOGLEVEL
        /// BATCH_JAVA_OTHER_OPTIONS
        /// </summary>
        [DataMember]
        [XmlArray("Settings")]
        [XmlArrayItem("Property")]
        public List<Property<SettingsKeys,string>> AppSettings { get; set; }
    }
}
