using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Mantenimiento
{
    public class LST_GET_SEPELIO : EntityBase
    {
        public Decimal NID { get; set; }
        public Decimal IDSEPELIO { get; set; }
        public String FECHACIERRE { get; set; }
        public Single NMONTO { get; set; }
        public String ESTADO { get; set; }
        public String NESTADO { get; set; }
    }

    public class LST_GET_VALIDA_SEPELIO 
    {
        public String FECHACIERRE { get; set; }
    }

    public class PARAM_GET_SEPELIO
    {
        public Nullable<DateTime> P_RANGE_INI { get; set; }
        public Nullable<DateTime> P_RANGE_FIN { get; set; }
        public Decimal P_IDSEPELIO { get; set; }
        public int P_NPAGESIZE { get; set; }
        public int P_NPAGENUM { get; set; }
        public int P_CRITERIO { get; set; }
        public Nullable<DateTime> P_DFECCIERRE { get; set; }
        public Decimal P_NMONTO { get; set; }
        public int P_USERREGISTER { get; set; }
        public int P_TIPO { get; set; }
    }
}
