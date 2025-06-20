using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Tools
{
    public class LST_MENSAJE
    {
        public decimal EXITO { get; set; }
        public string MENSAJE { get; set; }
        public decimal CODIGO { get; set; }
        public decimal CANTIDAD { get; set; }
        public List<LST_MENSAJE> DATA { get; set; }
    }
}
