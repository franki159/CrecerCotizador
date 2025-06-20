using System;
using System.Collections.Generic;
using System.Domain.Entities.Dashboard;
using System.Linq;
using System.Persistence.Data.Dashboard;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Business.Dashboard
{
    public class DashboardBusiness
    {
        DashboardData dashboardRequest;

        public List<LST_GET_CHART_JS> getSeriesChartBar(int nChartData, Int64 nIdUser)
        {
            dashboardRequest = new DashboardData();
            return dashboardRequest.getSeriesChartBar(nChartData, nIdUser);
        }

        public List<LST_GET_CHART_JS> getPolizasXCorredorChartBar(ChartFiltro filtro)       
        {
            dashboardRequest = new DashboardData();
            return dashboardRequest.getPolizasXCorredorChartBar(filtro);
        }

        public List<LST_GET_CHART_JS> getPolizasXProductoChartBar(ChartFiltro filtro)
        {
            dashboardRequest = new DashboardData();
            return dashboardRequest.getPolizasXProductoChartBar(filtro);
        }

        public List<LST_GET_CHART_JS> getPolizasXUbigeoChartBar(ChartFiltro filtro)
        {
            dashboardRequest = new DashboardData();
            return dashboardRequest.getPolizasXUbigeoChartBar(filtro);
        }

        public List<LST_GET_CHART_JS> getPolizasXRangoPrimaChartBar(ChartFiltro filtro)
        {
            dashboardRequest = new DashboardData();
            return dashboardRequest.getPolizasXRangoPrimaChartBar(filtro);
        }

        public List<LST_GET_CHART_JS> getPolizasXSumaAseguradaChartBar(ChartFiltro filtro)
        {
            dashboardRequest = new DashboardData();
            return dashboardRequest.getPolizasXSumaAseguradaChartBar(filtro);
        }

        
    }
}
