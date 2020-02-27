using QueueLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DispatcherService
{

    [ServiceContract(Namespace = Constants.Namespace)]
    public interface IDispatcherOperations
    {
        [OperationContract(IsOneWay = true)]
        void RegisterJob(Job job);

        [OperationContract(IsOneWay = true)]
        void RegisterWorker(Worker worker);

        [OperationContract(IsOneWay = true)]
        void RequestJob(Worker worker);

        [OperationContract(IsOneWay = true)]
        void DeRegisterWorker(Worker worker);

    }
}
