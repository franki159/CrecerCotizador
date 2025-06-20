using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class LST_GET_RESOURCE
    {
        public Int64 NID { get; set; }
        public decimal NIDPARENT { get; set; }
        public string SNAME { get; set; }
        public string SHTML { get; set; }
        public string SSTAG { get; set; }
        public Int64 NIDASI { get; set; }
    }
}
