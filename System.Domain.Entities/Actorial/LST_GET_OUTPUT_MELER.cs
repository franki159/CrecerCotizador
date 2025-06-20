using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Actorial
{
    public class LST_GET_OUTPUT_MELER : EntityBase
    {
        public Decimal NID { get; set; }
        public Decimal NIDINPUT { get; set; }
        public String NOMBRE_ARCHIVO { get; set; }
        public String ESTADO { get; set; }
        public String NESTADO { get; set; }
        public String FECHA_PROC { get; set; }
        public Int64 CORRECTOS { get; set; }
        public Int64 CON_ERROR { get; set; }
        public Int64 TOTAL { get; set; }
        public String DESCARGA { get; set; }
    }
}
