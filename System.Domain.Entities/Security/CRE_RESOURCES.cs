using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
    public class CRE_RESOURCES : EntityBase
    {
        public long NIDRESOURCE { get; set; }
        public long NIDPARENT { get; set; }
        public string SNAME { get; set; }
        public string SDESCRIPTION { get; set; }
        public string SHTML { get; set; }
        public string SSTATE { get; set; }
        public DateTime DREGDATE { get; set; }
        public long NUSERREG { get; set; }
        public DateTime DUPDDATE { get; set; }
        public long NUSERUPDATE { get; set; }
        public CRE_RESOURCES PARENT { get; set; }
        public IEnumerable<CRE_RESOURCES> CHILDREN { get; set; }
        public string SSTAG { get; set; }
        public decimal NVALIDATE { get; set; }
    }
}
