using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security.Parameters
{
    public class PARAMS_GET_USER
    {
        public string P_DESCRIPTION { get; set; }
        public decimal P_NPAGESIZE { get; set; }
        public decimal P_NPAGENUM { get; set; }
    }
}
