using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Common.Parameters
{
    public class PagerParameter : PagerBaseParameter
    {
        public string SORTDATAFIELD { get; set; }
        public string SORTORDER { get; set; }
    }
}
