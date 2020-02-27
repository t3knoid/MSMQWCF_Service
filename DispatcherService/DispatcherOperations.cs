using QueueLib;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DispatcherService
{
    [ServiceBehavior(Namespace = Constants.Namespace,InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class DispatcherOperations : IDispatcherOperations
    {

        Dictionary<Guid, Worker> Workers = new Dictionary<Guid,Worker>();

        /// <summary>
        /// This method is automatically whenever this ServiceHost is instantiated. It creates the WCF
        /// service programmatically.
        /// 
        /// https://docs.microsoft.com/en-us/dotnet/framework/wcf/configuring-wcf-services-in-code
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(ServiceConfiguration config)
        {
            string hostname = "localhost";
            try
            {
                hostname = System.Net.Dns.GetHostName();
            }
            catch (Exception)
            {
                hostname = "localhost";
            }

            try
            {
                DispatcherQueue dispatcherQueue = new DispatcherQueue();
                dispatcherQueue.Create(true); // Creates a transactional queue
                dispatcherQueue.SetPermissions(); // Sets default permissions

                Uri uri = new Uri("net.msmq://" + hostname + "/private/dispatcher");
                NetMsmqBinding binding = new NetMsmqBinding();
                NetMsmqSecurity security = binding.Security;
                binding.Security.Mode = NetMsmqSecurityMode.None;
                binding.Namespace = Constants.Namespace;
                // Note the use of the static method GetContract in the ServiceEndPoint instantiation
                // https://stackoverflow.com/questions/42327988/addserviceendpoint-throws-key-is-null
                ServiceEndpoint se = new ServiceEndpoint(ContractDescription.GetContract(typeof(IDispatcherOperations)), binding, new EndpointAddress(uri));
                config.AddServiceEndpoint(se);
                Uri baseAddress = new Uri("http://" + hostname + ":9000");
                config.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = baseAddress });
                config.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
            }
            catch (InvalidOperationException  ex)
            {
                if (ex.Message == "Message Queuing has not been installed on this computer")
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            // To modify any existing behavior, use the following pattern:
            //
            // var behavior = config.Description.Behaviors.Find<ServiceDebugBehavior>();
            // behavior.IncludeExceptionDetailInFaults = true;
        }
        /// <summary>
        /// Registers a given job. The job is added to the job queue. This is
        /// typically called by a remote client to schedule processing 
        /// jobs.
        /// </summary>
        /// <param name="job">A job</param>
        public void RegisterJob(Job job)
        {
            Console.WriteLine(String.Format("Received job registration {0} found of type {1} from project {2} requested by {3} using workstation {4}. Processing.", job.JobID, job.JobType, job.Project, job.Investigator, job.RequestingHost));

            // Add job to proper queue
            try
            {
                JobQueue jobqueue = new JobQueue();
                jobqueue.WriteMessage(job);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        /// <summary>
        /// Registers a given worker. This is typically called ed by the worker service when
        /// it first starts up. to A worker queue will be created for new registration. This 
        /// queue will be used to assign jobs to specific processors.
        /// </summary>
        /// <param name="worker">A worker</param>
        public void RegisterWorker(Worker worker)
        {
            Workers.Add(worker.RegistrationID, worker);
            Console.WriteLine(String.Format("Received worker registration from {0} with ID of {1}.", worker.Hostname, worker.RegistrationID));
            // Add a queue that will allow assignment of jobs to this worker
            WorkerQueue workerQueue = new WorkerQueue(worker);
            try
            {
                workerQueue.Create(false);
                workerQueue.SetPermissions();
                Console.WriteLine("Worker queue creation OK.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Worker queue creation failed. " + ex.Message);
            }
        }
        /// <summary>
        /// A method a worker can use to request for a job. This is typically
        /// called when the worker is not busy and available to perform work.
        /// </summary>
        /// <param name="worker">A worker requestor</param>
        /// <param name="retval">An out parameter indicating success or failure of the operation.</param>
        public void RequestJob(Worker worker)
        {
            Console.WriteLine(String.Format("Received worker job request from {0}", worker.Hostname));

            // Checks which jobs are available from the job queues
            // Job queue priority order is highest, high, normal, low, lowest.
            var priorities = Enum.GetNames(typeof(JobPriority));
            JobQueue currentJobQueue = null;
            Console.WriteLine(String.Format("Checking if there are jobs in job queue"));
            try
            {
                currentJobQueue = new JobQueue();
                if (currentJobQueue.Count > 0)
                {
                    Console.WriteLine(String.Format("Found job in job queue"));
                    Job job = currentJobQueue.ReadMessage();
                    Console.WriteLine(String.Format("Assigning job {0} from queue priority {1} to worker queue {2} with a registrationID of {3}.", job.JobID, currentJobQueue.Label, worker.Hostname, worker.RegistrationID.ToString()));
                    WorkerQueue processorQueue = new WorkerQueue(worker);
                    processorQueue.WriteMessage(job);
                }
                else
                {
                    Console.WriteLine("No jobs available");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
           
        }
        /// <summary>
        /// A method a worker can use to remove itself from the available processors. 
        /// This is typically called when the worker is shutting down.
        /// </summary>
        /// <param name="worker">A worker</param>
        public void DeRegisterWorker(Worker worker)
        {
            Console.WriteLine(String.Format("Received worker de-registration request from {0}", worker.Hostname));
        }
    }
}
