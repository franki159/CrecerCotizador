using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Comercial
{
    public class LST_GET_MEALER : EntityBase
    {
        public String SELECTCARGA { get; set; }
        public Decimal NID { get; set; }
        //public Decimal ID_ARCHIVO { get; set; }
        public String NOMBRE_ARCHIVO { get; set; }
        public String ESTADO { get; set; }
        public String NESTADO { get; set; }
        public String FECHA_PROC { get; set; }
        public String FECHA_CIERRE { get; set; }
        public Int64 CORRECTOS { get; set; }
        public Int64 CON_ERROR { get; set; }
        public Int64 TOTAL { get; set; }
        public String DESCARGA { get; set; }
    }

    public class LST_GET_MEALER_ANIO
    {
        public String ANIO { get; set; }
    }

    public class PARAMS_GET_MEALER
    {
        public string P_SNOMBRE_ARCHIVO { get; set; }
        //public string P_PERIODO { get; set; }
        public int P_NPAGESIZE { get; set; }
        public int P_NPAGENUM { get; set; }
        public int P_IDLOTE { get; set; }
        public Nullable<DateTime> P_SRANGEREG_INI { get; set; }
        public Nullable<DateTime> P_SRANGEREG_FIN { get; set; }
        public Nullable<DateTime> P_SRANGECIE_INI { get; set; }
        public Nullable<DateTime> P_SRANGECIE_FIN { get; set; }
    }

    public class LST_GET_NROOPERACION_AFILIACION
    {
        public Int64 NID { get; set; }
        public String SNROOPERACION { get; set; }
        public String SFECHADEVENGUE { get; set; }
        public String SFECHAENVIO { get; set; }
        public String SFECHACIERRE { get; set; }
    }

    public class LST_GET_VARIABLE_CAB : EntityBase
    {
        public Decimal NID { get; set; }
        public String SARCH { get; set; }
        public Int64 NROREGISTRO { get; set; }
        public String ESTADO { get; set; }
        public String NESTADO { get; set; }
        public String FECHA_REG { get; set; }
        public String DESCARGA { get; set; }
    }
}
