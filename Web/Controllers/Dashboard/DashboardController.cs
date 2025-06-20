using System;
using System.Application.Business.Dashboard;
using System.Collections.Generic;
using System.Domain.Entities.Dashboard;
using System.Domain.Entities.Security;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Classes.Setting;

namespace Web.Controllers.Dashboard
{    
    [CheckFilter]
    public class DashboardController : Controller
    {
        private static CRE_SESSION userSession = null;
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getChartBar(ChartFiltro obj)// nChartData, string fechaIni, string fechafin)
        {
            DashboardBusiness bDashboard = new DashboardBusiness();
            userSession = (CRE_SESSION)Session["ObjUsuarioSession"];


            List<LST_GET_CHART_JS> lstSeries = new List<LST_GET_CHART_JS>();
            LST_GET_CHART_JS entidad = new LST_GET_CHART_JS();
            if (obj.nTipoReporte == 1) {            
           

            //lstSeries.Add(entidad);
                lstSeries = bDashboard.getPolizasXCorredorChartBar(obj);
            }

            if (obj.nTipoReporte == 2)
            {
               
                lstSeries = bDashboard.getPolizasXUbigeoChartBar(obj);
                //lstSeries.Add(entidad);
            }

            if (obj.nTipoReporte == 3)
            {
                //entidad.sSerie = "1";
                //entidad.nIdSerie = 1;
                //string[] sLabels = { "258,000.00", "170,000.00", "90,000.00 ", "50,000.00" };
                //entidad.sLabels = sLabels;
                //int[] nValSerie = { 9, 15, 7, 13 };
                //entidad.nValSerie = nValSerie;
                //entidad.sBgColor = "red";
                //entidad.sBorderColor = "yellow";

                //lstSeries.Add(entidad);
                //entidad = new LST_GET_CHART_JS();
                //entidad.sSerie = "2";
                //entidad.nIdSerie = 2;
                //string[] sLabels2 = { "258,000.00", "170,000.00", "90,000.00 ", "50,000.00" };
                //entidad.sLabels = sLabels2;
                //int[] nValSerie2 = { 3, 15, 7, 7 };
                //entidad.nValSerie = nValSerie2;
                //entidad.sBgColor = "red";
                //entidad.sBorderColor = "yellow";

                //lstSeries.Add(entidad);
                 lstSeries = bDashboard.getPolizasXRangoPrimaChartBar(obj);
            }

            if (obj.nTipoReporte == 6)
            {
                //entidad.sSerie = "";
                //entidad.nIdSerie = 1;
                //string[] sLabels = { "Para la contratación de Obras, Suministros y Servicios", "Aduaneras y Tributarias", "Para Exportadores e Importadores  ", "De Funcionamiento de acuerdo a Ley" };
                //entidad.sLabels = sLabels;
                //int[] nValSerie = { 9, 15, 7, 13 };
                //entidad.nValSerie = nValSerie;
                //entidad.sBgColor = "red";
                //entidad.sBorderColor = "yellow";

                //lstSeries.Add(entidad);
                lstSeries = bDashboard.getPolizasXProductoChartBar(obj);
            }

            if (obj.nTipoReporte == 7)
            {
                //entidad.sSerie = "";
                //entidad.nIdSerie = 1;
                //string[] sLabels = { "[20-6]millon", "[1-5]millon", "[99-50]mil", "[50-10]mil" };
                //entidad.sLabels = sLabels;
                //int[] nValSerie = { 6, 3, 4, 5 };
                //entidad.nValSerie = nValSerie;
                //entidad.sBgColor = "red";
                //entidad.sBorderColor = "yellow";

                //lstSeries.Add(entidad);
                lstSeries = bDashboard.getPolizasXSumaAseguradaChartBar(obj);
                
            }

                        

            return Json(new
            {
                cb = lstSeries
            }, JsonRequestBehavior.AllowGet);



        }

        [HttpGet]
        public JsonResult getAutoJobs()
        {
            List<LST_GET_CHART_JS> lstEJobs = null;

            return Json(new
            {
                eJobs = lstEJobs
            }, JsonRequestBehavior.AllowGet);
        }

	}
}