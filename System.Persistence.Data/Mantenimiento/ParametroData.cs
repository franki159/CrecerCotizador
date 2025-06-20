using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Domain.Entities.Mantenimiento;
using System.Domain.Entities.Tools;
using System.Infrastructure.Tools.Extensions;
using System.Infrastructure.Utilities.Utilities;
using System.Linq;
using System.Persistence.Connection;
using System.Text;
using System.Threading.Tasks;

namespace System.Persistence.Data.Mantenimiento
{
    public class ParametroData : DataContextBase
    {
        public IEnumerable<LST_GET_PARAMETRO> GetListaParametro(PARAM_GET_PARAMETRO objParametros)
        {
            IEnumerable<LST_GET_PARAMETRO> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SRANGEREGINI", OracleDbType.Date, objParametros.P_RANGE_INI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGEREGFIN", OracleDbType.Date, objParametros.P_RANGE_FIN, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_IDPARAMETRO", OracleDbType.Decimal, objParametros.P_IDPARAMETRO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Int32, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Int32, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_PARAMETRO", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_PARAMETRO>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar el parametro - Message: " + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_MENSAJE> MantParametro(PARAM_GET_PARAMETRO objParametros)
        {
            IEnumerable<LST_MENSAJE> entity = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_CRITERIO", OracleDbType.Int32, objParametros.P_CRITERIO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_IDPARAMETRO", OracleDbType.Long, objParametros.P_IDPARAMETRO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_FACTORAJUST", OracleDbType.Decimal, objParametros.P_FACTORAJUST, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_FECLIMHJS", OracleDbType.Date, objParametros.P_FECLIMHJS, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_GASTOEMIS", OracleDbType.Decimal, objParametros.P_GASTOEMIS, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_GASTOMANT", OracleDbType.Decimal, objParametros.P_GASTOMANT, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_FACTORSEG", OracleDbType.Decimal, objParametros.P_FACTORSEG, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_MARGSOLV", OracleDbType.Decimal, objParametros.P_MARGSOLV, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_USERREGISTER", OracleDbType.Int32, objParametros.P_USERREGISTER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_MANTENEDOR_PARAMETRO", parameter))
            {
                try
                {
                    entity = dr.ReadRows<LST_MENSAJE>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al registrar el parametro - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                    throw new Exception();
                }
            }
            return entity;
        }

    }


}
