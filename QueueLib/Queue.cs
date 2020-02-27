using System;
using System.Messaging;

namespace QueueLib
{
    /// <summary>
    /// Common class for queues
    /// </summary>
    public class Queue
    {
        /// <summary>
        /// The queue's path
        /// </summary>
        protected internal string Path
        {
            get { return path; }
            set { path = value; }
        }
        /// <summary>
        /// An optional description for the queue
        /// </summary>
        protected internal string Description
        {
            get { return description; }
            set { description = value; }
        }

        string path;
        string description;

        /// <summary>
        /// The number of items in the queue
        /// </summary>
        public int Count
        {
            get
            {
                try
                {
                    int count = 0;
                    MessageQueue messageQueue = new MessageQueue(path);
                    var enumerator = messageQueue.GetMessageEnumerator2();
                    while (enumerator.MoveNext())
                        count++;
                    return count;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// A label attached to the queue
        /// </summary>
        public string Label
        {
            get
            {
                MessageQueue messageQueue = new MessageQueue(path);
                return messageQueue.Label;
            }
        }

        /// <summary>
        /// A default constructor
        /// </summary>
        protected internal Queue()
        {

        }
        /// <summary>
        /// A constructor where you can specify the path
        /// </summary>
        /// <param name="p"></param>
        protected internal Queue(string p)
        {
            path = p;
        }
        /// <summary>
        /// A constructor where you can specify the messagequeue path and description
        /// </summary>
        /// <param name="p">Path</param>
        /// <param name="d">Description</param>
        protected internal Queue(string p, string d)
        {
            path = p;
            description = d;
        }
        /// <summary>
        /// Deletes the queue
        /// </summary>
        protected internal void Delete()
        {
            // Determine whether the queue exists.
            if (MessageQueue.Exists(path))
            {
                try
                {
                    // Delete the queue.
                    MessageQueue.Delete(path);
                }
                catch (MessageQueueException e)
                {
                    if (e.MessageQueueErrorCode ==
                        MessageQueueErrorCode.AccessDenied)
                    {
                        throw new Exception("Access is denied. " +
                            "Queue might be a system queue.");
                    }

                    // Handle other sources of MessageQueueException.
                }

            }
        }
        protected internal void Purge()
        {
            // Determine whether the queue exists.
            if (MessageQueue.Exists(path))
            {
                MessageQueue messageQueue = new MessageQueue(path);
                try
                {
                    // Delete messages from the queue
                    messageQueue.Purge();
                }
                catch (MessageQueueException e)
                {
                    if (e.MessageQueueErrorCode ==
                        MessageQueueErrorCode.AccessDenied)
                    {
                        throw new Exception("Access is denied. " +
                            "Queue might be a system queue.");
                    }

                    // Handle other sources of MessageQueueException.
                }
                finally
                {
                    messageQueue.Dispose();
                }

            }
        }

        /// <summary>
        /// Creates a queue specified in the path parameter
        /// </summary>
        public void Create(bool transactional = false)
        {
            if (!MessageQueue.Exists(Path))
            {
                try
                {
                    MessageQueue queue = MessageQueue.Create(Path, transactional);
                    queue.Label = description;
                }
                catch (ArgumentException)
                {
                    throw new Exception("The specified path is empty.");
                }
                catch (MessageQueueException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        /// <summary>
        /// Reads the first available message in the queue. Note that this
        /// method blocks until a message is read.
        /// </summary>
        /// <returns>First available job</returns>
        public Job ReadMessage()
        {

            if (!MessageQueue.Exists(Path))
                return null;
            Job jobMessage = new Job();
            MessageQueue messageQueue = new MessageQueue(Path);
            messageQueue.MessageReadPropertyFilter.Priority = true;
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Job) });
            try
            {
                //Message message = messageQueue.Receive(new TimeSpan(0, 0, 1));
                Message message = messageQueue.Receive();
                jobMessage = (Job)message.Body;
            }
            catch (MessageQueueException e)
            {
                // Handle Message Queuing exceptions.
                if (e.Message == "Timeout for the requested operation has expired.")
                    throw new Exception("Queue is empty");
                else throw (e);
            }
            catch (InvalidOperationException e)
            {
                // Handle invalid serialization format.
                throw (e);
            }
            finally
            {
                messageQueue.Dispose();
            }
            
            return jobMessage;
        }

        /// <summary>
        /// Adds a worker into the worker queue. This method ensures that
        /// a worker can never be added more than once in the queue.
        /// </summary>
        /// <param name="message"></param>
        public void WriteMessage(Job job)
        {
            MessageQueue messageQueue = new MessageQueue(Path);
            try
            {
                if (MessageQueue.Exists(Path))
                {
                    Message message = new Message();
                    message.Body = job;
                    message.Priority = (MessagePriority)job.Priority;
                    messageQueue = new MessageQueue(Path);
                    messageQueue.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                messageQueue.Dispose();
            }
        }
        /// <summary>
        /// Checks if a given host is in the worker queue
        /// </summary>
        /// <param name="processorMsg"></param>
        /// <returns>Returns true if the machine is in the worker queue</returns>
        internal bool MessageExists(Job jobMessage)
        {
            bool exists = false;
            MessageQueue messageQueue = new MessageQueue(Path);
            Cursor cursor = messageQueue.CreateCursor();
            Job m = PeekMessage(new TimeSpan(0, 0, 1), cursor); // Checks the first message in the queue
            exists = m == jobMessage;
            while (!exists && m != null) // iterate through the rest of the messages in the queue
            {
                m = PeekMessage(new TimeSpan(0, 0, 1), cursor, PeekAction.Next);
                exists = m == jobMessage;
            }
            
            messageQueue.Dispose();

            return exists;
        }
        /// <summary>
        /// Peeks at the Worker message queue.
        /// </summary>
        /// <param name="t">Maimum time to wait for the queue to contain a message</param>
        /// <param name="c">A cursor that mains a specific position in the message queue</param>
        /// <param name="a">Peek action</param>
        /// <returns></returns>
        internal Job PeekMessage(TimeSpan t, Cursor c, PeekAction a = PeekAction.Current)
        {
            if (t != null)
                t = new TimeSpan(0, 0, 1);
            Job jobMessage = new Job();
            MessageQueue messageQueue = new MessageQueue(Path);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Job) });
            try
            {
                Message message = messageQueue.Peek(t, c, a);
                jobMessage = (Job)message.Body;
            }
            catch (MessageQueueException e)
            {
                // Handle Message Queuing exceptions.
                if (e.Message == "Timeout for the requested operation has expired.")
                    throw new Exception("Queue is empty");
                else throw (e);
            }
            catch (InvalidOperationException e)
            {
                // Handle invalid serialization format.
                throw (e);
            }
            finally
            {
                messageQueue.Dispose();
            }
            return jobMessage;
        }

        /// <summary>
        /// Peeks at the Worker message queue.
        /// </summary>       
        /// <returns>The worker message at top of the queue</returns>
        internal Job PeekMessage()
        {
            Job jobMessage = new Job();
            MessageQueue messageQueue = new MessageQueue(Path);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Job) });
            try
            {
                Message message = messageQueue.Peek();
                jobMessage = (Job)message.Body;
            }
            catch (MessageQueueException ex)
            {
                // Handle Message Queuing exceptions.
                if (ex.Message == "Timeout for the requested operation has expired.")
                    throw new Exception("Queue is empty");
                else throw (ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid serialization format.
                throw (ex);
            }
            finally
            {
                messageQueue.Dispose();
            }
            return jobMessage;
        }

        /// <summary>
        /// Sets default permission for the queue. Remote queues must have anonymous access. 
        /// </summary>
        public void SetPermissions()
        {
            MessageQueue messageQueue = new MessageQueue(Path);
            try 
            {
                messageQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl, AccessControlEntryType.Allow);
                messageQueue.SetPermissions("ANONYMOUS LOGON", MessageQueueAccessRights.FullControl, AccessControlEntryType.Allow);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                messageQueue.Dispose();
            }
        }
    }
}
