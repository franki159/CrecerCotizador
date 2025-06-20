using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Common
{
    public abstract class EntityBase
    {
        public decimal ROWNUMBER { get; set; }
        public decimal ROWTOTAL { get; set; }
        public long STATUS { get; set; }
        public string TAG { get; set; }
    }
}
