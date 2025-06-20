using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace System.Services.Alert
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /*static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new AlertMail()
            };
            ServiceBase.Run(ServicesToRun);
        }*/
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                AlertMail service1 = new AlertMail();
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Pon el código que tenías antes aquí
            }
        }
    }
}
