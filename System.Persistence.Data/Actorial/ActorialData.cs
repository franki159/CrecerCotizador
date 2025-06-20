using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Domain.Entities.Actorial;
using System.Domain.Entities.Actorial.Parameters;
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

namespace System.Persistence.Data.Actorial
{
    public class ActorialData : DataContextBase
    {
        public IEnumerable<LST_GET_OUTPUT_MELER> GetVerificarNombreArchivoOutputMealer(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            IEnumerable<LST_GET_OUTPUT_MELER> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SNOMBRE_ARCHIVO", OracleDbType.Varchar2, objParametros.P_SNOMBRE_ARCHIVO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_OUTPUT_CAB_VALIDAR", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_OUTPUT_MELER>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_GET_OUTPUT_MELER> GetListaActorialOutputMealer(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            IEnumerable<LST_GET_OUTPUT_MELER> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_IDLOTEINPUT", OracleDbType.Varchar2, objParametros.P_PERIODO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Int32, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Int32, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_OUTPUT_CAB", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_OUTPUT_MELER>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_MENSAJE> RegistrarSolicitud(String idLote, List<solicitudRecibida> Solicitud, List<SolicitudRecibidaProducto> Producto, List<SolicitudRecibidaCotizacion> Cotizacion, String NombreArchivo, string NombreArchivoAutogenerado)
        {
            IEnumerable<LST_MENSAJE> entity = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                //Solicitud
                XDocument xml = new XDocument();
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");
                foreach (var item in Solicitud)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("FILA", item.NFILA.ToString());
                    tx.WriteElementString("NROPERACION", item.nroOperacion);
                    tx.WriteElementString("CUSPP", item.CUSPP);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlSolicitud = sw.ToString();

                //Producto
                xml = new XDocument();
                sw = new StringWriter();
                tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");
                foreach (var item in Producto)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("FILA", item.NFILA.ToString());
                    tx.WriteElementString("CORRELATIVO", item.NCORRELATIVO.ToString());
                    tx.WriteElementString("MODALIDAD", item.modalidad);
                    tx.WriteElementString("ANOSRT", item.anosRT);
                    tx.WriteElementString("PORCENTAJERVD", item.porcentajeRVD);
                    tx.WriteElementString("PERIODOGARANTIZADO", item.periodoGarantizado);
                    tx.WriteElementString("MONEDA", item.moneda);
                    tx.WriteElementString("DERECHOCRECER", item.derechoCrecer);
                    tx.WriteElementString("GRATIFICACION", item.gratificacion);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlProducto = sw.ToString();

                //Cotizacion
                xml = new XDocument();
                sw = new StringWriter();
                tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");
                foreach (var item in Cotizacion)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("FILA", item.NFILA.ToString());
                    tx.WriteElementString("CORRELATIVO", item.NCORRELATIVO.ToString());
                    tx.WriteElementString("SICOTIZANOCOTIZA", item.siCotizaNoCotiza);
                    tx.WriteElementString("NROCOTIZACION", item.nroCotizacion);
                    tx.WriteElementString("PRIMAUNICAEESS", item.primaUnicaEESS);
                    tx.WriteElementString("PRIMERAPENSIONRV", item.primeraPensionRV);
                    tx.WriteElementString("TASAINTERESRV", item.tasaInteresRV);
                    tx.WriteElementString("PRIMAUNICAAFPEESS", item.primaUnicaAFPEESS);
                    tx.WriteElementString("PRIMERAPENSIONRT", item.primeraPensionRT);
                    tx.WriteElementString("TASAINTERESRT", item.tasaInteresRT);
                    tx.WriteElementString("PRIMERAPENSIONRVD", item.primeraPensionRVD);
                    tx.WriteElementString("TASAINTERESRVD", item.tasaInteresRVD);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlCotizacion = sw.ToString();

                parameters.Add(new OracleParameter("P_IDLOTEINPUT", OracleDbType.Decimal, idLote, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_SOLICITUD", OracleDbType.Clob, xmlSolicitud, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_PRODUCTO", OracleDbType.Clob, xmlProducto, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_COTIZACION", OracleDbType.Clob, xmlCotizacion, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.NVarchar2, NombreArchivo, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NOMBRE_ARCHIVO_AUTOGENERADO", OracleDbType.NVarchar2, NombreArchivoAutogenerado, ParameterDirection.Input));
                parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_INS_MELER_OUTPUT_SOLICITUD", parameters))
                {
                    entity = dr.ReadRows<LST_MENSAJE>();
                }
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al guardar tramas actorial - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

            return entity;
        }

        public IEnumerable<LST_REPORTE_OUTPUT_MELER> GetReporteOutputMeler(PARAMS_GET_OUTPUT_MELER objParameters)
        {
            IEnumerable<LST_REPORTE_OUTPUT_MELER> entityErrores = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            parameters.Add(new OracleParameter("P_IDLOTE", OracleDbType.Int32, objParameters.P_IDLOTE, ParameterDirection.Input));
            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_REPORTE_OUTPUT_MELER", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_REPORTE_OUTPUT_MELER>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las Garantías Constituidas:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }

        public bool EliminarSolicitud(PARAMS_GET_OUTPUT_MELER objParameters)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();
            bool result = false;
            try
            {
                parameters.Add(new OracleParameter("P_IDLOTE", OracleDbType.Long, objParameters.P_IDLOTE, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_DEL_OUTPUT_MELER", parameters);
                result = true;
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al eliminar la solicitud Output - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

            return result;
        }
    }
}
