using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class LST_GET_RESOURCES : EntityBase
    {
        public Int64 NID { get; set; }
        public string SNAME { get; set; }
        public string SFATHER { get; set; }        
        public string SUSERREG { get; set; }
        public string DFECREG { get; set; }
        public string SSTATE { get; set; }
    }
}
