using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Mantenimiento
{
    public class LST_GET_TASA_MERCADO : EntityBase
    {
        public Decimal NID { get; set; }
        public Decimal IDTASAMERCADO { get; set; }
        public Single NTASAMERCJUBSA { get; set; }
        public Single NTASAMERCJUBDA { get; set; }
        public Single NTASAMERCJUBSI { get; set; }
        public Single NTASAMERCINVSA { get; set; }
        public Single NTASAMERCINVDA { get; set; }
        public Single NTASAMERCINVSI { get; set; }
        public Single NTASAMERCSOBSA { get; set; }
        public Single NTASAMERCSOBDA { get; set; }
        public Single NTASAMERCSOBSI { get; set; }
        public String ESTADO { get; set; }
        public String NESTADO { get; set; }
        public String FECHA_REGISTRO { get; set; }
    }

    public class PARAM_GET_TASA_MERCADO
    {
        public Nullable<DateTime> P_SRANGEREGINI { get; set; }
        public Nullable<DateTime> P_SRANGEREGFIN { get; set; }
        public Decimal P_IDTASAMERCADO { get; set; }
        public int P_NPAGESIZE { get; set; }
        public int P_NPAGENUM { get; set; }
        public int P_CRITERIO { get; set; }
        public Decimal P_TASAMERCJUBSA { get; set; }
        public Decimal P_TASAMERCJUBDA { get; set; }
        public Decimal P_TASAMERCJUBSI { get; set; }
        public Decimal P_TASAMERCINVSA { get; set; }
        public Decimal P_TASAMERCINVDA { get; set; }
        public Decimal P_TASAMERCINVSI { get; set; }
        public Decimal P_TASAMERCSOBSA { get; set; }
        public Decimal P_TASAMERCSOBDA { get; set; }
        public Decimal P_TASAMERCSOBSI { get; set; }
        public int P_USERREGISTER { get; set; }
    }
}
