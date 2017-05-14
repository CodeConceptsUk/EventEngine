namespace CodeConcepts.EventEngine.ConsoleService
{
    partial class ProjectInstaller
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
            this.eventEngineProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.eventEngineInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // eventEngineProcessInstaller
            // 
            this.eventEngineProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.eventEngineProcessInstaller.Password = null;
            this.eventEngineProcessInstaller.Username = null;
            // 
            // eventEngineInstaller
            // 
            this.eventEngineInstaller.Description = "Event Engine";
            this.eventEngineInstaller.DisplayName = "Event Engine";
            this.eventEngineInstaller.ServiceName = "EventEngineService";
            this.eventEngineInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.eventEngineProcessInstaller,
            this.eventEngineInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller eventEngineProcessInstaller;
        private System.ServiceProcess.ServiceInstaller eventEngineInstaller;
    }
}