using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security.Parameters
{
    public class PARAMS_INS_PROFILE
    {
        public Int64 P_NIDPROFILE { get; set; }
        public string P_SNAME { get; set; }
        public string P_SDESCRIPTION { get; set; }
        public Int64 P_NUSER { get; set; }
        public string SCONSULTA { get; set; }
    }
}
