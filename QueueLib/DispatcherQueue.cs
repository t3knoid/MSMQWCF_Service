using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QueueLib
{
    public class DispatcherQueue : Queue
    {
        string pathFormat = @"{0}\Private$\Dispatcher";
        string description = "Dispatcher Queue";
        public DispatcherQueue()
        {
            Path = string.Format(pathFormat, ".");
            Description = description;
        }

    }
}
