using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class LST_GET_USER_AD    
    {
        public Int64 NIDUSER { get; set; }
        public string SUSER { get; set; }        
        public string SNAME { get; set; }
        public string SLASTNAME { get; set; }
        public string SLASTNAME2 { get; set; }
        public string SSEX { get; set; }
        public string SEMAIL { get; set; }
        public string SPHONE1 { get; set; }
    }
}
