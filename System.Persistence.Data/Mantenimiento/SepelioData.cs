using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Domain.Entities.Mantenimiento;
using System.Domain.Entities.Tools;
using System.Infrastructure.Tools.Extensions;
using System.Infrastructure.Utilities.Utilities;
using System.IO;
using System.Linq;
using System.Persistence.Connection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace System.Persistence.Data.Mantenimiento
{
    public class SepelioData : DataContextBase
    {
        public IEnumerable<LST_GET_SEPELIO> GetListaSepelio(PARAM_GET_SEPELIO objParametros)
        {
            IEnumerable<LST_GET_SEPELIO> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SRANGEREGINI", OracleDbType.Date, objParametros.P_RANGE_INI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGEREGFIN", OracleDbType.Date, objParametros.P_RANGE_FIN, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_IDSEPELIO", OracleDbType.Decimal, objParametros.P_IDSEPELIO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Int32, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Int32, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_SEPELIO", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_SEPELIO>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar el parametro - Message: " + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_MENSAJE> MantSepelio(PARAM_GET_SEPELIO objParametros, List<PARAM_GET_SEPELIO> lista)
        {
            IEnumerable<LST_MENSAJE> entity = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);

            tx.WriteStartElement("Campos");
            foreach (var item in lista)
            {
                tx.WriteStartElement("Campo");
                tx.WriteElementString("FECHA", ((DateTime)item.P_DFECCIERRE).ToString("yyyy-MM-dd HH:mm:ss"));
                tx.WriteElementString("MONTO", item.P_NMONTO.ToString());
                tx.WriteEndElement();
            }
            tx.WriteEndElement();
            var xmlSepelio = sw.ToString();

            parameter.Add(new OracleParameter("P_CRITERIO", OracleDbType.Int32, objParametros.P_CRITERIO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_IDSEPELIO", OracleDbType.Long, objParametros.P_IDSEPELIO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_DFECCIERRE", OracleDbType.Date, objParametros.P_DFECCIERRE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NMONTO", OracleDbType.Decimal, objParametros.P_NMONTO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_USERREGISTER", OracleDbType.Int32, objParametros.P_USERREGISTER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_XML_SEPELIO", OracleDbType.Clob, xmlSepelio, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_MANTENEDOR_SEPELIO", parameter))
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

        public IEnumerable<LST_GET_VALIDA_SEPELIO> GetListaValidaSepelio(List<PARAM_GET_SEPELIO> lista)
        {
            IEnumerable<LST_GET_VALIDA_SEPELIO> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);

            tx.WriteStartElement("Campos");
            foreach (var item in lista)
            {
                tx.WriteStartElement("Campo");
                tx.WriteElementString("FECHA", ((DateTime)item.P_DFECCIERRE).ToString("yyyy-MM-dd HH:mm:ss"));
                tx.WriteEndElement();
            }
            tx.WriteEndElement();
            var xmlSepelio = sw.ToString();

            parameter.Add(new OracleParameter("P_XML_SEPELIO", OracleDbType.Clob, xmlSepelio, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_VALIDA_SEPELIO", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_VALIDA_SEPELIO>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar el parametro - Message: " + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                    throw new Exception();
                }
            }
            return entityUser;
        }
    }
}
