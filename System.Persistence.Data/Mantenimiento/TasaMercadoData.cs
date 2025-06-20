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
    public class TasaMercadoData : DataContextBase
    {
        public IEnumerable<LST_GET_TASA_MERCADO> GetListaTasaMercado(PARAM_GET_TASA_MERCADO objParametros)
        {
            IEnumerable<LST_GET_TASA_MERCADO> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SRANGEREGINI", OracleDbType.Date, objParametros.P_SRANGEREGINI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGEREGFIN", OracleDbType.Date, objParametros.P_SRANGEREGFIN, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_IDTASAMERCADO", OracleDbType.Decimal, objParametros.P_IDTASAMERCADO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Int32, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Int32, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_TASA_MERCADO", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_TASA_MERCADO>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar el parametro - Message: " + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_MENSAJE> MantTasaMercado(PARAM_GET_TASA_MERCADO objParametros)
        {
            IEnumerable<LST_MENSAJE> entity = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_CRITERIO", OracleDbType.Int32, objParametros.P_CRITERIO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_IDTASAMERCADO", OracleDbType.Long, objParametros.P_IDTASAMERCADO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCJUBSA", OracleDbType.Decimal, objParametros.P_TASAMERCJUBSA, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCJUBDA", OracleDbType.Decimal, objParametros.P_TASAMERCJUBDA, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCJUBSI", OracleDbType.Decimal, objParametros.P_TASAMERCJUBSI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCINVSA", OracleDbType.Decimal, objParametros.P_TASAMERCINVSA, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCINVDA", OracleDbType.Decimal, objParametros.P_TASAMERCINVDA, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCINVSI", OracleDbType.Decimal, objParametros.P_TASAMERCINVSI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCSOBSA", OracleDbType.Decimal, objParametros.P_TASAMERCSOBSA, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCSOBDA", OracleDbType.Decimal, objParametros.P_TASAMERCSOBDA, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAMERCSOBSI", OracleDbType.Decimal, objParametros.P_TASAMERCSOBSI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_USERREGISTER", OracleDbType.Int32, objParametros.P_USERREGISTER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_MANTENEDOR_TASA_MERCADO", parameter))
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
