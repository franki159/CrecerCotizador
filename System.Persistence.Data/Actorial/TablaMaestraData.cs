using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Domain.Entities.Actorial;
using System.Infrastructure.Tools.Extensions;
using System.Infrastructure.Utilities.Utilities;
using System.Linq;
using System.Persistence.Connection;
using System.Text;
using System.Threading.Tasks;

namespace System.Persistence.Data.Actorial
{
    public class TablaMaestraData : DataContextBase
    {
        public IEnumerable<LST_GET_TABLA_MAESTRA> GetListaTablaMaestra(String sDesc)
        {
            IEnumerable<LST_GET_TABLA_MAESTRA> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SDESCTABLA", OracleDbType.Varchar2, sDesc, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_LISTA_TABLA_MAESTRA", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_TABLA_MAESTRA>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }
    }
}
