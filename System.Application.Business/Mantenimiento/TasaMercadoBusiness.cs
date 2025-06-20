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
    public class TasaMercadoBusiness
    {
        TasaMercadoData oTasaMercadoData;
        public IEnumerable<LST_GET_TASA_MERCADO> GetListaTasaMercado(PARAM_GET_TASA_MERCADO objParametros)
        {
            oTasaMercadoData = new TasaMercadoData();
            return oTasaMercadoData.GetListaTasaMercado(objParametros);
        }

        public IEnumerable<LST_MENSAJE> MantTasaMercado(PARAM_GET_TASA_MERCADO objParametros)
        {
            oTasaMercadoData = new TasaMercadoData();
            return oTasaMercadoData.MantTasaMercado(objParametros);
        }
    }
}
