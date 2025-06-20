using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Tools.Extensions
{
    public class DataReaderParameter
    {
        public static Object ValidarParameter(OracleDataReader oReader, String campo, String tipo)
        {
            Object result = DBNull.Value;

            if (tipo == "string")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetString(oReader.GetOrdinal(campo)).Trim();
                else
                    result = String.Empty;
            }
            else if (tipo == "int")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetInt32(oReader.GetOrdinal(campo));
                else
                    result = 0;
            }
            else if (tipo == "int64")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetInt64(oReader.GetOrdinal(campo));
                else
                    result = 0;
            }
            else if (tipo == "date")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDateTime(oReader.GetOrdinal(campo));
                else
                    result = DateTime.MinValue;
            }
            else if (tipo == "bool")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetBoolean(oReader.GetOrdinal(campo));
                else
                    result = false;
            }
            else if (tipo == "decimal")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDecimal(oReader.GetOrdinal(campo));
                else
                    result = (Decimal)0;
            }
            else if (tipo == "double")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDouble(oReader.GetOrdinal(campo));
                else
                    result = (Double)0;
            }

            return result;
        }
    }
}
