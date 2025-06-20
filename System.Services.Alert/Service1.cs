using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace System.Services.Alert
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AlertMail service1 = new AlertMail();
            service1.TestStartupAndStop(args);
            //if (Environment.UserInteractive)
            //{
            //    AlertMail service1 = new AlertMail();
            //    service1.TestStartupAndStop(args);
            //}
            //else
            //{
            //    // Pon el código que tenías antes aquí
            //}
        }

        protected override void OnStop()
        {
        }
    }
}
