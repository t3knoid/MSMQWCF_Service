using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueLib
{
    [DataContract]
    class JobRequest
    {
        [DataMember]
        Guid JobiD { get; set; }
        [DataMember]
        Worker worker { get; set; }

    }
}
