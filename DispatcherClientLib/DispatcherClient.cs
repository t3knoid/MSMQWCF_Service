using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using DispatcherClientLib;
using QueueLib;
using DispatcherClientLib.Dispatcher;

namespace DispatcherClientLib
{
    public class DispatcherClient
    {
        string dispatcherHost;

        DispatcherOperationsClient client;

        public DispatcherClient(string dispatcher)
        {
            dispatcherHost = dispatcher;
            Initialize();
        }

        /// <summary>
        /// Initializes the endpoint address of the Dispatcher
        /// </summary>
        void Initialize()
        {
            NetMsmqBinding binding = new NetMsmqBinding();
            NetMsmqSecurity security = binding.Security;
            binding.Security.Mode = NetMsmqSecurityMode.None;
            binding.Name = "NetMsmqBinding_IDispatcherOperations";
            EndpointAddress address = new EndpointAddress("net.msmq://" + dispatcherHost + "/private/dispatcher");
            ServiceEndpoint se = new ServiceEndpoint(ContractDescription.GetContract(typeof(IDispatcherOperations)), binding, address);
            client = new DispatcherOperationsClient(binding, address);
        }
        /// <summary>
        /// Registers a given server hostname serving a worker to the Dispatcher service.
        /// </summary>
        /// <param name="hostname">hostname or IP of server</param>
        /// <returns>Sets Success to true and sets the message to the registration GUID if the operation is successful. Otherwise, Success is set to false and sets message with the exception message.</returns>
        public DispatcherResult RegisterWorker(Worker worker)
        {
            Guid guid = Guid.NewGuid();
            worker.RegistrationID = guid;

            try
            {
                client.RegisterWorker(worker);
                client.Close();
            }
            catch (Exception ex)
            {
                return new DispatcherResult(false, ex.Message);
            }

            return new DispatcherResult(true, guid.ToString());
        }
        /// <summary>
        /// De-registers a worker from the Dispatcher service. The guid is 
        /// used as a confirmation to de-register the correct worker instance.
        /// </summary>
        /// <param name="hostname">The hostname to be de-registered</param>
        /// <param name="guid">The guid that identifies a specific registration</param>
        /// <returns>Sets Success to true if the operation is successful. Otherwise, Success is set to false and sets message with the exception message.</returns>
        public DispatcherResult DeregisterProcessor(string hostname, Guid guid)
        {
            Worker worker = new Worker()
            {
                Hostname = hostname,
                RegistrationID = guid,
                Sent = DateTime.Now,
            };
            try 
            {
                client.DeRegisterWorker(worker);
                client.Close();
            }
            catch (Exception ex)
            {
                return new DispatcherResult(false, ex.Message);
            }

            return new DispatcherResult(true);
        }

        /// <summary>
        /// Registers a job with the given parameters. By default the job priority is set to NORMAL.
        /// </summary>
        /// <param name="project">The project name</param>
        /// <param name="batchfile">The batch file to execute</param>
        /// <param name="requestor">The hostname of the machine the request came from</param>
        /// <param name="priority">The job priority (defaults to NORMAL)</param>
        /// <returns>Sets Success to true and sets the message to the registration GUID if the operation is successful. Otherwise, Success is set to false and sets message with the exception message.</returns>
        public DispatcherResult RegisterJob(string investigator, string project, string settingsFileContent, string jobFileContent, List<QueueLib.Property<SettingsKeys, string>> appSettings, JobPriority priority = JobPriority.NORMAL)
        {
            Guid guid = Guid.NewGuid();
            string requestingHost = System.Net.Dns.GetHostName();

            Job job = new Job()
            {
                Investigator = investigator,
                Project = project,          
                RequestingHost = requestingHost,
                SettingsFileContent = settingsFileContent,
                JobFileContent = jobFileContent,
                JobID = guid,
                Sent = DateTime.Now,
            };
            try
            {
                client.RegisterJob(job);
                client.Close();
            }
            catch (Exception ex)
            {
                return new DispatcherResult(false, ex.Message);
            }

            return new DispatcherResult(true, guid.ToString()); ;
        }
        /// <summary>
        /// Registers a Job using a job object. Typically used to return a job to the job queue.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public DispatcherResult RegisterJob(Job job)
        {
            try
            {
                client.RegisterJob(job);
                client.Close();
            }
            catch (Exception ex)
            {
                return new DispatcherResult(false, ex.Message);
            }

            return new DispatcherResult(true, "Registered");
        }
        /// <summary>
        /// Request a job from the Dispatcher. When the dispatcher receives a request
        /// from the specific worker, it adds a job to the worker's work queue.
        /// </summary>
        /// <param name="hostname">The worker hostname</param>
        /// <param name="registrationid">The guid</param>
        /// <returns>Sets Success to true if the operation is successful. Otherwise, Success is set to false and sets message with the exception message.</returns>
        public DispatcherResult RequestJob(Worker worker)
        {
            worker.Sent = DateTime.Now;
            try
            {
                client.RequestJob(worker);
                client.Close();
            }
            catch (Exception ex)
            {
                return new DispatcherResult(false, ex.Message);
            }

            return new DispatcherResult(true); ;
        }

    }
    /// <summary>
    /// Return value of Dispatcher client API calls
    /// </summary>
    public struct DispatcherResult
    {
        /// <summary>
        /// This is set to true if the call was successful. Otherwise,
        /// this is set to false if the call was a failure.
        /// </summary>
        public bool Success;
        /// <summary>
        /// Relevant message to return. This can be an exception message if the 
        /// call was a failure or expected data if the call was successful.
        /// </summary>
        public string Message;
        public DispatcherResult(bool s, string m="")
        {
            Message = m;
            Success = s;
        }
    }
}
