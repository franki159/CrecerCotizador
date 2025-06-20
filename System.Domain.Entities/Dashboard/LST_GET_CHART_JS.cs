using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Dashboard
{
    public class LST_GET_CHART_JS
    {
        public int nIdSerie { get; set; }
        public string sSerie { get; set; }

        public string[] sLabels { get; set; }
        public int[] nValSerie { get; set; }
        public string sBgColor { get; set; }
        public string sBorderColor { get; set; }  
        public int TotalSoles { get; set; }
        public int TotalDolares { get; set; }
    }

    public class ChartFiltro
    {
       public int nChartData { get; set; }
        public Nullable<DateTime> fechaIni { get; set; }
        public Nullable<DateTime> fechafin { get; set; }
        public int nTipoReporte { get; set; }
    }
}
