using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class LST_GET_RESOURCES_EDIT
    {
        public Int64 NIDRESOURCE { get; set; }
        public Int64 NIDPARENT { get; set; }
        public string SNAME { get; set; }
        public string SDESCRIPTION { get; set; }
        public string SHTML { get; set; }
        public string SUSER { get; set; }
        public string DFECREG { get; set; }
    }
}
