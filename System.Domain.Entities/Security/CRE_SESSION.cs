using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class CRE_SESSION
    {
        public Int64 NIDUSER { get; set; }
        public string SUSER { get; set; }
        public string SNAME { get; set; }
        public string SLASTNAME { get; set; }
        public string SLASTNAME2 { get; set; }
        public string SEMAIL { get; set; }
        public Int64 NIDPROFILE { get; set; }
        public string SNAME_PROFILE { get; set; } 
    }
}
