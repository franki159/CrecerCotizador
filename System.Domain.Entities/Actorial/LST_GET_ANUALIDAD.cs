using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Actorial
{
    public class LST_GET_ANUALIDAD : EntityBase
    {
        public Decimal ANIO { get; set; }
        public Decimal TASA_AJUSTADA { get; set; }
        public Decimal PENSION_ANUAL { get; set; }
        public Decimal PENSION_MENSUAL { get; set; }
    }
}
