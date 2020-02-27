using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueLib;

namespace DispatcherClientLib
{
    public interface IDispatcherClient
    {
        bool SubmitJobToDispatcher(bool useAllValues);
        StringBuilder CreateSettingsString(bool p, bool useAllValues);
    }
}
