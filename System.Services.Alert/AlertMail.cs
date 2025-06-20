using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Domain.Entities.Services.Alert;
using System.Domain.Entities.Services.Alert.Parameters;
using System.Application.Business.Services.Alert;
using System.Domain.Entities.Policy;
using System.Domain.Entities.Policy.Parameters;
using System.Application.Business.Policy;
using System.Domain.Entities.Tools;

namespace System.Services.Alert
{
    partial class AlertMail : ServiceBase
    {
        public AlertMail()
        {
            InitializeComponent();
            //SendMail();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            Console.WriteLine("Inicio del Proceso");
            IEnumerable<LST_GET_SOLICITUDES> LstSolicitudes = null;
            AlertBusiness AlertBs = new AlertBusiness();
            Console.WriteLine("AlertBs.GetAlert");
            //AlertBs.GetAlert(new PARAMS_PALERT { idalert = 0});
            Console.WriteLine("Antes GetSolicitudesVencer");
            LstSolicitudes = AlertBs.GetSolicitudesVencer(30);
            
            Console.WriteLine("Despues GetSolicitudesVencer");
            foreach (var item in LstSolicitudes)
            {
                new AlertBusiness().SendMail(new PARAMS_PALERT_PARAM
                {
                    ACCION = 0,
                    NID_ALERT = 2,
                    NID_USER = 1,
                    SEVENT = "POLIZA_VENCIMIENTO",
                    PARAM_VALUE = item.NID_SOLICITUD.ToString()
                }, false, item.SFROM, item.STO); 
            }
            
            LstSolicitudes = AlertBs.GetSolicitudesVencer(15);
            foreach (var item in LstSolicitudes)
            {
                new AlertBusiness().SendMail(new PARAMS_PALERT_PARAM
                {
                    ACCION = 0,
                    NID_ALERT = 1,//1 ES DE 15 DIAS- 2
                    NID_USER = 1,
                    SEVENT = "POLIZA_VENCIMIENTO",
                    PARAM_VALUE = item.NID_SOLICITUD.ToString()
                }, false);
            }

            //LstSolicitudes = AlertBs.GetSolicitudesVencer(7);
            
            //foreach (var item in LstSolicitudes)
            //{
            //    new AlertBusiness().SendMail(new PARAMS_PALERT_PARAM
            //    {
            //        ACCION = 0,
            //        NID_ALERT = 1,//1 ES DE 15 DIAS- 2
            //        NID_USER = 1,
            //        SEVENT = "POLIZA_VENCIMIENTO",
            //        PARAM_VALUE = item.NID_SOLICITUD.ToString()
            //    }, false);
            //}

            foreach (var item in LstSolicitudes)
            {
                PARAM_POLICY objParametros = new PARAM_POLICY();
                PolicyBusiness businesss = new PolicyBusiness();
                IEnumerable<LST_MENSAJE> resultado;

                objParametros.PNID_SOLICITUD = item.NID_SOLICITUD;
                resultado = businesss.RenovarPolizaAutomático(objParametros);

                new AlertBusiness().SendMail(new PARAMS_PALERT_PARAM
                {
                    ACCION = 0,
                    NID_ALERT = 17,
                    NID_USER = 1,
                    SEVENT = "RENOVAR_AUT",
                    PARAM_VALUE = item.NID_SOLICITUD.ToString()
                }, false);
            }
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
        }        
    }
}
