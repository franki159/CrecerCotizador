using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Common.Parameters
{
    public class PagerBaseParameter : BaseParameter
    {
        public int filterscount { get; set; }
        public int groupscount { get; set; }
        public int? pagenum { get; set; }
        public int? pagesize { get; set; }
        public int? recordstartindex { get; set; }
        public int? recordendindex { get; set; }
        public int _ { get; set; }
    }
    
}
