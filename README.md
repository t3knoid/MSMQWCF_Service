# MSMQ WCF Service
The Service comprises of two Windows services, the Dispatcher and Worker services. The system uses Windows WCF and MSMQ Queue technologies. The service can be executed using a service account.

# Running the Service
Use the Service Control Panel to install and run the Dispatcher and Worker services. The Dispatcher and Worker service can run in the same server. However, the recommended configuration is to run each service in a separate machine.

# Dispatcher Service
The Dispatcher service provides methods to orchestrate job requests and worker service availability. The following tasks 
- Tracks workers
- Tracks jobs
- Assigns jobs to available workers
- Prioritizes the job queue

The Dispatcher service 

## Dispatcher Operations
The following are available operations that are available to clients.
- RegisterJob - Registers a given job. The job is added to the job queue. This is typically called by a remote client to schedule jobs.
- RegisterWorker - Registers a given worker. This is typically called ed by the worker service when it first starts up. to A worker queue will be created for new registration. This queue will be used to assign jobs to specific workers.
- RequestJob - A method a worker can use to request for a job. This is typically called when the worker is not busy and available to perform work.
- DeRegisterWorker - A method a worker can use to remove itself from the available workers. This is typically called when the worker is shutting down.

## The Dispatcher Message Queue
The Dispatcher message queue is used by the dispatcher to receive messages from the Worker service and the client. This queue is created automatically by the dispatcher service. The queue is located on the machine where the Dispatcher service is installed. To access the queue, perform the following:

1. Open Computer Management.
2. Select Services and Applications.
3. Select Message Queuing.
4. Select Private Queues.
5. Finally, select dispatcher.

If for some reason the dispatcher queue is not present after installing the Dispatcher service, the dispatcher queue can be created manually by performing the following:

1. Open Computer Management.
2. Select Services and Applications.
3. Select Message Queuing.
4. Right-click on Private Queues to open a context menu.
5. Select New > Private Queue from the context menu.
6. Enter "dispatcher" in the Queue name field.
7. Check the "Transactional" checkbox.
8. Click OK to create the queue.

After creating the queue, add the appropriate permissions to the queue using the following steps:

1. Right-click on the "dispatcher" queue to open a context menu.
2. Select "Properties" from the context menu to bring up the Properties dialog box.
3. Click on the Security tab.
4. Click the Add button to add the "ANONYMOUS LOGIN" user.
5. Finally give the "Everyone" and "ANONYMOUS LOGIN" user "Full Control" permissions.
 
# Worker Service
The worker service performs jobs that are sent to the dispatcher. Mutiple worker services can be deployed to scale workload as needed.

# Installing Windows Message Queuing Services

1. In Server Manager, click Manage > Add Roles and Features.
2. With the "Role-based or feature-based installation" option selected in the Installation Type screen, Click Next
3. Click Next on the Server Selection screen.
4. Click Next on the Server Roles screen.
5. On the Features screen, expand Message Queuing.
6. Expand Message Queuing Services.
7. Check the Message Queuing Server checkbox.
6. Click Next, then click Install.
