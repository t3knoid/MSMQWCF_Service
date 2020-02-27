using System;

namespace QueueLib
{
    /// <summary>
    /// A worker queue contains jobs assigned for specific processors
    /// Queues are named using the worker hostname.
    /// </summary>
    public class WorkerQueue : Queue
    {
        string pathFormat = @"{0}\Private$\WorkerQueue-{1}";
        string description = "Worker Job Queue";
        string DispatcherHost = ".";

        public WorkerQueue(Worker worker, string dispatcher = ".")
        {
            Path = String.Format(pathFormat, dispatcher, worker.Hostname);
            Description = description;
            DispatcherHost = dispatcher;
        }
    }
}
