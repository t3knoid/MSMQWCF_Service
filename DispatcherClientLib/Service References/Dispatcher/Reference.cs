﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DispatcherClientLib.Dispatcher {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://nuix-x/services/", ConfigurationName="Dispatcher.IDispatcherOperations")]
    public interface IDispatcherOperations {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/RegisterJob")]
        void RegisterJob(QueueLib.Job job);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/RegisterJob")]
        System.Threading.Tasks.Task RegisterJobAsync(QueueLib.Job job);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/RegisterWorker")]
        void RegisterWorker(QueueLib.Worker worker);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/RegisterWorker")]
        System.Threading.Tasks.Task RegisterWorkerAsync(QueueLib.Worker worker);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/RequestJob")]
        void RequestJob(QueueLib.Worker worker);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/RequestJob")]
        System.Threading.Tasks.Task RequestJobAsync(QueueLib.Worker worker);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/DeRegisterWorker")]
        void DeRegisterWorker(QueueLib.Worker worker);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://nuix-x/services/IDispatcherOperations/DeRegisterWorker")]
        System.Threading.Tasks.Task DeRegisterWorkerAsync(QueueLib.Worker worker);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDispatcherOperationsChannel : DispatcherClientLib.Dispatcher.IDispatcherOperations, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DispatcherOperationsClient : System.ServiceModel.ClientBase<DispatcherClientLib.Dispatcher.IDispatcherOperations>, DispatcherClientLib.Dispatcher.IDispatcherOperations {
        
        public DispatcherOperationsClient() {
        }
        
        public DispatcherOperationsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DispatcherOperationsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DispatcherOperationsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DispatcherOperationsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void RegisterJob(QueueLib.Job job) {
            base.Channel.RegisterJob(job);
        }
        
        public System.Threading.Tasks.Task RegisterJobAsync(QueueLib.Job job) {
            return base.Channel.RegisterJobAsync(job);
        }
        
        public void RegisterWorker(QueueLib.Worker worker) {
            base.Channel.RegisterWorker(worker);
        }
        
        public System.Threading.Tasks.Task RegisterWorkerAsync(QueueLib.Worker worker) {
            return base.Channel.RegisterWorkerAsync(worker);
        }
        
        public void RequestJob(QueueLib.Worker worker) {
            base.Channel.RequestJob(worker);
        }
        
        public System.Threading.Tasks.Task RequestJobAsync(QueueLib.Worker worker) {
            return base.Channel.RequestJobAsync(worker);
        }
        
        public void DeRegisterWorker(QueueLib.Worker worker) {
            base.Channel.DeRegisterWorker(worker);
        }
        
        public System.Threading.Tasks.Task DeRegisterWorkerAsync(QueueLib.Worker worker) {
            return base.Channel.DeRegisterWorkerAsync(worker);
        }
    }
}
