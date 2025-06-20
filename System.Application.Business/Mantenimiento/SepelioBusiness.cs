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
    public class SepelioBusiness
    {
        SepelioData oSepelioData;
        public IEnumerable<LST_GET_SEPELIO> GetListaSepelio(PARAM_GET_SEPELIO objParametros)
        {
            oSepelioData = new SepelioData();
            return oSepelioData.GetListaSepelio(objParametros);
        }

        public IEnumerable<LST_MENSAJE> MantSepelio(PARAM_GET_SEPELIO objParametros, List<PARAM_GET_SEPELIO> lista)
        {
            oSepelioData = new SepelioData();
            return oSepelioData.MantSepelio(objParametros, lista);
        }

        public IEnumerable<LST_GET_VALIDA_SEPELIO> GetListaValidaSepelio(List<PARAM_GET_SEPELIO> lista)
        {
            oSepelioData = new SepelioData();
            return oSepelioData.GetListaValidaSepelio(lista);
        }
    }
}
