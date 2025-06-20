using System;
using System.Collections.Generic;
using System.Domain.Entities.Mantenimiento;
using System.Domain.Entities.Tools;
using System.Linq;
using System.Persistence.Data.Mantenimiento;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Business.Mantenimiento
{
    public class ParametroBusiness
    {
        ParametroData oParametroData;
        public IEnumerable<LST_GET_PARAMETRO> GetListaParametro(PARAM_GET_PARAMETRO objParametros)
        {
            oParametroData = new ParametroData();
            return oParametroData.GetListaParametro(objParametros);
        }

        public IEnumerable<LST_MENSAJE> MantParametro(PARAM_GET_PARAMETRO objParametros)
        {
            oParametroData = new ParametroData();
            return oParametroData.MantParametro(objParametros);
        }
    }
}
