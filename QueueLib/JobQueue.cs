using System;

namespace QueueLib
{
    public class JobQueue : Queue
    {
        public JobQueue(string dispatcher = ".")
        {
            string pathFormat = @"{0}\Private$\JobQueue";
            Path = Path = String.Format(pathFormat, dispatcher);
            Description = "Job Queue";
        }
    }
}
