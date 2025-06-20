using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security.Parameters
{
    public class PARAMS_INS_RESOURCES
    {
        public Int64 P_NIDRESOURCE { get; set; }
        public Int64 P_NIDPARENT { get; set; }
        public string P_SNAME { get; set; }
        public string P_SDESCRIPTION { get; set; }
        public string P_SHTML { get; set; }
        public Int64 P_NUSERREG { get; set; }
    }
}
