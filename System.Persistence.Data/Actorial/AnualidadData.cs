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
    public class AnualidadData : DataContextBase
    {
        public IEnumerable<LST_GET_ANUALIDAD> GetCalculoAnualidad(Decimal cic, Decimal tasaventa, Decimal ajuste, Int32 edad, Int32 sexo, Int32 condsalud)
        {
            IEnumerable<LST_GET_ANUALIDAD> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_CIC", OracleDbType.Decimal, cic, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_TASAVENTA", OracleDbType.Decimal, tasaventa, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_AJUSTE", OracleDbType.Decimal, ajuste, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_EDAD", OracleDbType.Int32, edad, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SEXO", OracleDbType.Int32, sexo, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_CNDSALUD", OracleDbType.Int32, condsalud, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_CAL_ANUALIDAD", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_ANUALIDAD>();
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
