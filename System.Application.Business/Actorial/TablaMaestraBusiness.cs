using System;
using System.Collections.Generic;
using System.Domain.Entities.Actorial;
using System.Linq;
using System.Persistence.Data.Actorial;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Business.Actorial
{
    public class TablaMaestraBusiness
    {
        TablaMaestraData oTablaMaestraData;

        public IEnumerable<LST_GET_TABLA_MAESTRA> GetListaTablaMaestra(String sDesc)
        {
            oTablaMaestraData = new TablaMaestraData();
            return oTablaMaestraData.GetListaTablaMaestra(sDesc);
        }
    }
}
