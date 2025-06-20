using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Domain.Entities.Common.Parameters;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security.Parameters
{
    public class PARAMS_GET_PROFILE
    {
        public string P_DESCRIPTION { get; set; }
        public decimal P_NPAGESIZE { get; set; }
        public decimal P_NPAGENUM { get; set; }
    }
}
