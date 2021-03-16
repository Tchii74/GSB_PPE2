using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsServiceGSB
{
    public partial class GSB : ServiceBase
    {
        /// <summary>
        /// variable global de type timer
        /// </summary>
        private Timer timer = new Timer();

        public GSB()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();

            if (!System.Diagnostics.EventLog.SourceExists("GSB_Source"))
            {
                System.Diagnostics.EventLog.CreateEventSource("GSB_Source", "GSB_Log");
            }

            eventLog1.Source = "GSB_Source";
            eventLog1.Log = "GSB_Log";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry($"Démarrage du service, {DateTime.Now}");
        }

        protected override void OnStop()
        {
        }
    }
}
