using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Mantenimiento
{
    public class LST_GET_PARAMETRO : EntityBase
    {
        public Decimal NID { get; set; }
        public Decimal IDPARAMETRO { get; set; }
        public Single NFACTORAJUST { get; set; }
        public String ESTADO { get; set; }
        public String NESTADO { get; set; }
        public String FECHA_LIM_HIJO { get; set; }
        public Single NGASTOEMIS { get; set; }
        public Single NGASTOMANT { get; set; }
        public Single NFACTORSEG { get; set; }
        public Single NMARGSOLV { get; set; }
        public String FECHA_REGISTRO { get; set; }
        
    }

    public class PARAM_GET_PARAMETRO
    {
        public Nullable<DateTime> P_RANGE_INI { get; set; }
        public Nullable<DateTime> P_RANGE_FIN { get; set; }
        public Decimal P_IDPARAMETRO { get; set; }
        public int P_NPAGESIZE { get; set; }
        public int P_NPAGENUM { get; set; }
        public int P_CRITERIO { get; set; }
        public Decimal P_FACTORAJUST { get; set; }
        public Nullable<DateTime> P_FECLIMHJS { get; set; }
        public Decimal P_GASTOEMIS { get; set; }
        public Decimal P_GASTOMANT { get; set; }
        public Decimal P_FACTORSEG { get; set; }
        public Decimal P_MARGSOLV { get; set; }
        public int P_USERREGISTER { get; set; }
        
    }
}
