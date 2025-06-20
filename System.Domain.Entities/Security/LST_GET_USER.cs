using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class LST_GET_USER : EntityBase
    {
        public Int64 NID { get; set; }
        public string SUSER { get; set; }
        public string SPROFILE { get; set; }
        public string SNAME { get; set; }
        public string SADDRESS { get; set; }
        public string SCHARGE { get; set; }
        public string DFECREG { get; set; }
        public string SSTATE { get; set; } 
       // public FromFile file { get; set; }

    }
}
