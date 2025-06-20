using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Domain.Entities.Tools;
using System.Domain.Entities.Tools.Parameters;
using System.Infrastructure.Tools.Extensions;
using System.Infrastructure.Utilities.Utilities;
using System.Persistence.Connection;

namespace System.Persistence.Data.Tools
{
    public class ToolsData : DataContextBase
    {
        public IEnumerable<LST_GET_LIST> GetList(PARAMS_GET_LIST objParametros)
        {
            IEnumerable<LST_GET_LIST> entList = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SCODE", OracleDbType.NVarchar2, objParametros.P_SCODE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SIGMA_TOOLS.GET_LIST", parameter))
            {
                entList = dr.ReadRows<LST_GET_LIST>();
                Utilities.GuardarLog("Lista variables del sistema");
            }

            return entList;
        }

        public IEnumerable<LST_GET_GROUP_ASI> GetGroupAsi()
        {
            IEnumerable<LST_GET_GROUP_ASI> entGroup = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SIGMA_TOOLS.GET_GROUP_ASI", parameter))
            {
                entGroup = dr.ReadRows<LST_GET_GROUP_ASI>();
                Utilities.GuardarLog("Lista grupos de asientos");
            }

            return entGroup;
        }
    }
}
