namespace DispatcherService
{
    partial class DispatcherService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorkerDispatcher = new System.ComponentModel.BackgroundWorker();
            // 
            // backgroundWorkerDispatcher
            // 
            this.backgroundWorkerDispatcher.WorkerSupportsCancellation = true;
            this.backgroundWorkerDispatcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDispatcher_DoWork);
            this.backgroundWorkerDispatcher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDispatcher_RunWorkerCompleted);
            // 
            // DispatcherService
            // 
            this.CanHandlePowerEvent = true;
            this.CanShutdown = true;
            this.ServiceName = "nuix-x-dispatcher-service";

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerDispatcher;
    }
}
