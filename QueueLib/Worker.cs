using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueLib
{
    [DataContract]
    public class Worker
    {
        /// <summary>
        /// A unique non-serialized string to identify the registration. 
        /// </summary>
        [DataMember]
        public Guid RegistrationID { get; set; }
        /// <summary>
        /// The hostname of the machine where the worker is hosted.
        /// </summary>
        [DataMember]
        public string Hostname { get; set; }
        /// <summary>
        /// Date and time of when the message was sent
        /// </summary>
        [DataMember]
        public DateTime Sent { get; set; }
    }
}
