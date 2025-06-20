using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Domain.Entities.Comercial;
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

namespace System.Persistence.Data.Comercial
{
    public class MealerData : DataContextBase
    {
        public IEnumerable<LST_GET_MEALER> GetVerificarNombreArchivoMealer(PARAMS_GET_MEALER objParametros)
        {
            IEnumerable<LST_GET_MEALER> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SNOMBRE_ARCHIVO", OracleDbType.Varchar2, objParametros.P_SNOMBRE_ARCHIVO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_MELER_INPUT_CAB_VALIDAR", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_MEALER>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_GET_MEALER> GetListaComercialMealer(PARAMS_GET_MEALER objParametros)
        {
            IEnumerable<LST_GET_MEALER> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            //parameter.Add(new OracleParameter("P_SPERIODO", OracleDbType.Varchar2, objParametros.P_PERIODO, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGEREGINI", OracleDbType.Date, objParametros.P_SRANGEREG_INI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGEREGFIN", OracleDbType.Date, objParametros.P_SRANGEREG_FIN, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGECIEINI", OracleDbType.Date, objParametros.P_SRANGECIE_INI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGECIEFIN", OracleDbType.Date, objParametros.P_SRANGECIE_FIN, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Int32, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Int32, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_MELER_INPUT_CAB", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_MEALER>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_MENSAJE> RegistrarSolicitud(List<solicitudRecibidaEESS> Solicitud, List<afiliado> Afiliado, List<fondo> fondo, List<beneficiario> beneficiario, List<producto> Producto, String NombreArchivo, string NombreArchivoAutogenerado) //, DateTime dFechaCierre)
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
                    tx.WriteElementString("AFP", item.AFP);
                    tx.WriteElementString("CUSPP", item.CUSPP);
                    tx.WriteElementString("TIPOBENEFICIO", item.tipoBeneficio);
                    tx.WriteElementString("CAMBIOMODALIDAD", item.cambioModalidad);
                    tx.WriteElementString("PENSIONPRELIMINAR", item.pensionPreliminar);
                    tx.WriteElementString("TASARPYRT", item.tasaRPyRT);
                    tx.WriteElementString("FECHADEVENGUE", item.fechaDevengue);
                    tx.WriteElementString("FECHASUSCRIPCIONIII", item.fechaSuscripcionIII);
                    tx.WriteElementString("DEVENGUESOLICITUD", item.devengueSolicitud);
                    tx.WriteElementString("FECHAENVIO", item.fechaEnvio);
                    tx.WriteElementString("FECHACIERRE", item.fechaCierre);
                    tx.WriteElementString("TIPOCAMBIO", item.tipoCambio);
                    tx.WriteElementString("DIACITA", item.diaCita);
                    tx.WriteElementString("HORACITA", item.horaCita);
                    tx.WriteElementString("LUGARCITA", item.lugarCita);
                    tx.WriteElementString("NUMEROMENSUALIDAD", item.numeroMensualidad);
                    tx.WriteElementString("TIPOFONDO", item.tipoFondo);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlSolicitud = sw.ToString();

                //Afiliado
                xml = new XDocument();
                sw = new StringWriter();
                tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");
                foreach (var item in Afiliado)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("FILA", item.NFILA.ToString());
                    tx.WriteElementString("CORRELATIVO", item.NCORRELATIVO.ToString());
                    tx.WriteElementString("TIPODOC", item.tipoDoc);
                    tx.WriteElementString("NRODOC", item.nroDoc);
                    tx.WriteElementString("APELLIDOPATERNO", item.apellidoPaterno);
                    tx.WriteElementString("APELLIDOMATERNO", item.apellidoMaterno);
                    tx.WriteElementString("PRIMERNOMBRE", item.primerNombre);
                    tx.WriteElementString("GENERO", item.genero);
                    tx.WriteElementString("FECHANACIMIENTO", item.fechaNacimiento);
                    tx.WriteElementString("ESTADOSOBREVIVENCIA", item.estadoSobrevivencia);
                    tx.WriteElementString("SEGUNDONOMBRE", item.segundoNombre);
                    tx.WriteElementString("GRADOINVALIDEZ", item.gradoInvalidez);
                    tx.WriteElementString("CONDICIONINVALIDEZ", item.condicionInvalidez);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlAfilicion = sw.ToString();

                //Fondo
                xml = new XDocument();
                sw = new StringWriter();
                tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");
                foreach (var item in fondo)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("FILA", item.NFILA.ToString());
                    tx.WriteElementString("CORRELATIVO", item.NCORRELATIVO.ToString());
                    tx.WriteElementString("MONEDA", item.moneda);
                    tx.WriteElementString("CAPITALPENSION", item.capitalPension);
                    tx.WriteElementString("SALDOCIC", item.saldoCic);
                    tx.WriteElementString("VALORCUOTA", item.valorCuota);
                    tx.WriteElementString("SALDOCUOTAS", item.saldoCuotas);
                    tx.WriteElementString("TIENECOBERTURA", item.tieneCobertura);
                    tx.WriteElementString("TIPOCAMBIOCOMPRAAA", item.tipoCambioCompraAA);
                    tx.WriteElementString("EESSCOBERTURA", item.EESScobertura);
                    tx.WriteElementString("APORTEADICIONAL", item.aporteAdicional);
                    tx.WriteElementString("BONOACTUALIZADO", item.bonoActualizado);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlFondo = sw.ToString();

                //Beneficiario
                xml = new XDocument();
                sw = new StringWriter();
                tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");
                foreach (var item in beneficiario)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("FILA", item.NFILA.ToString());
                    tx.WriteElementString("CORRELATIVO", item.NCORRELATIVO.ToString());
                    tx.WriteElementString("APELLIDOPATERNO", item.apellidoPaterno);
                    tx.WriteElementString("APELLIDOMATERNO", item.apellidoMaterno);
                    tx.WriteElementString("PRIMERNOMBRE", item.primerNombre);
                    tx.WriteElementString("SEGUNDONOMBRE", item.segundoNombre);
                    tx.WriteElementString("PARENTESCO", item.parentesco);
                    tx.WriteElementString("CONDICIONINVALIDEZ", item.condicionInvalidez);
                    tx.WriteElementString("FECHANACIMIENTO", item.fechaNacimiento);
                    tx.WriteElementString("GENERO", item.genero);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlBeneficiario = sw.ToString();

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
                    tx.WriteElementString("MONEDA", item.moneda);
                    tx.WriteElementString("DERECHOCRECER", item.derechoCrecer);
                    tx.WriteElementString("GRATIFICACION", item.gratificacion);
                    tx.WriteElementString("ANOSRT", item.anosRT);
                    tx.WriteElementString("PORCENTAJERVD", item.porcentajeRVD);
                    tx.WriteElementString("PERIODOGARANTIZADO", item.periodoGarantizado);
                    tx.WriteEndElement();
                }
                tx.WriteEndElement();
                var xmlProducto = sw.ToString();

                parameters.Add(new OracleParameter("P_XML_SOLICITUD", OracleDbType.Clob, xmlSolicitud, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_AFILIACION", OracleDbType.Clob, xmlAfilicion, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_BENEFICIO", OracleDbType.Clob, xmlBeneficiario, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_FONDO", OracleDbType.Clob, xmlFondo, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_PRODUCTO", OracleDbType.Clob, xmlProducto, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.NVarchar2, NombreArchivo, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NOMBRE_ARCHIVO_AUTOGENERADO", OracleDbType.NVarchar2, NombreArchivoAutogenerado, ParameterDirection.Input));
                //parameters.Add(new OracleParameter("P_FECHA_CIERRE", OracleDbType.Date, dFechaCierre, ParameterDirection.Input));
                parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_INS_MELER_INPUT_SOLICITUD", parameters))
                {
                    entity = dr.ReadRows<LST_MENSAJE>();
                }
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al guardar tramas afiliados - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

            return entity;
        }

        public IEnumerable<LST_REPORTE_MEALER> GetReporteComercialMealer(PARAMS_GET_MEALER objParameters)
        {
            IEnumerable<LST_REPORTE_MEALER> entityErrores = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            parameters.Add(new OracleParameter("P_IDLOTE", OracleDbType.Int32, objParameters.P_IDLOTE, ParameterDirection.Input));
            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_REPORTE_INPUT_MELER", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_REPORTE_MEALER>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las Garantías Constituidas:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }

        public IEnumerable<LST_GET_MEALER_ANIO> GetAnio()
        {
            IEnumerable<LST_GET_MEALER_ANIO> entityErrores = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_LISTA_ANIO", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_GET_MEALER_ANIO>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las Garantías Constituidas:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }

        public bool EliminarSolicitud(PARAMS_GET_MEALER objParameters)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();
            bool result = false;
            try
            {
                parameters.Add(new OracleParameter("P_IDLOTE", OracleDbType.Long, objParameters.P_IDLOTE, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_DEL_INPUT_MELER", parameters);
                result = true;
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al guardar tramas afiliados - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

            return result;
        }

        public IEnumerable<LST_GET_NROOPERACION_AFILIACION> GetListaNroOpercionAfiliado(PARAMS_GET_MEALER objParametros)
        {
            IEnumerable<LST_GET_NROOPERACION_AFILIACION> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_IDLOTE", OracleDbType.Int64, objParametros.P_IDLOTE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_LISTA_NROOPERACION_AFILIADO", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_NROOPERACION_AFILIACION>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public void InsertarVariableAfiliacion(Int64 idLote, Int64[] listAfiliacion)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);

                tx.WriteStartElement("Campos");

                foreach (Int64 item in listAfiliacion)
                {
                    tx.WriteStartElement("Campo");
                    tx.WriteElementString("ID", item.ToString());
                    tx.WriteEndElement();
                }

                tx.WriteEndElement();
                var xmlAfiliacion = sw.ToString();

                parameters.Add(new OracleParameter("P_IDLOTE", OracleDbType.Int64, idLote, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_XML_ASIENTO", OracleDbType.Clob, xmlAfiliacion, ParameterDirection.Input));
                this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_INS_VARIABLES_AFILIACION", parameters);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error: " + ex.Message);
                throw new Exception();
            }
        }

        public IEnumerable<LST_GET_VARIABLE_CAB> GetListaVariableCab(PARAMS_GET_MEALER objParametros)
        {
            IEnumerable<LST_GET_VARIABLE_CAB> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SRANGEREGINI", OracleDbType.Date, objParametros.P_SRANGEREG_INI, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_SRANGEREGFIN", OracleDbType.Date, objParametros.P_SRANGEREG_FIN, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Int32, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Int32, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_VITALICIA.RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_VARIABLE_CAB", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_VARIABLE_CAB>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error: " + ex.Message);
                    throw new Exception();
                }
            }
            return entityUser;
        }

        public IEnumerable<LST_REPORTE_VARIABLE_DET> GetReporteVariableDet(PARAMS_GET_MEALER objParameters)
        {
            IEnumerable<LST_REPORTE_VARIABLE_DET> entityErrores = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            parameters.Add(new OracleParameter("P_IDVARIABLE", OracleDbType.Int32, objParameters.P_IDLOTE, ParameterDirection.Input));
            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_REPORTE_VARIABLE_DET", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_REPORTE_VARIABLE_DET>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las Garantías Constituidas:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }
        public IEnumerable<LST_REPORTE_VARIABLE_MOR> GetReporteVariableMor(PARAMS_GET_MEALER objParameters)
        {
            IEnumerable<LST_REPORTE_VARIABLE_MOR> entityErrores = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            parameters.Add(new OracleParameter("P_IDVARIABLE", OracleDbType.Int32, objParameters.P_IDLOTE, ParameterDirection.Input));
            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_REPORTE_VARIABLE_MOR", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_REPORTE_VARIABLE_MOR>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las Garantías Constituidas:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }

        public IEnumerable<LST_REPORTE_VARIABLE_DET_PROD> GetReporteVariableDetProd(PARAMS_GET_MEALER objParameters)
        {
            IEnumerable<LST_REPORTE_VARIABLE_DET_PROD> entityErrores = null;
            List<OracleParameter> parameters = new List<OracleParameter>();

            parameters.Add(new OracleParameter("P_IDVARIABLE", OracleDbType.Int32, objParameters.P_IDLOTE, ParameterDirection.Input));
            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_REPORTE_VARIABLE_DET_PROD", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_REPORTE_VARIABLE_DET_PROD>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las Garantías Constituidas:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }

        public IEnumerable<LST_VALIDA_MAX_NROOPERACION> GetValidaMaxCotizacionNroOperacion(Int64 idLote, Int64[] listAfiliacion)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();
            IEnumerable<LST_VALIDA_MAX_NROOPERACION> entityErrores = null;

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);

            tx.WriteStartElement("Campos");

            foreach (Int64 item in listAfiliacion)
            {
                tx.WriteStartElement("Campo");
                tx.WriteElementString("ID", item.ToString());
                tx.WriteEndElement();
            }

            tx.WriteEndElement();
            var xmlAfiliacion = sw.ToString();

            parameters.Add(new OracleParameter("P_XML_NROOPERACION", OracleDbType.Clob, xmlAfiliacion, ParameterDirection.Input));
            parameters.Add(new OracleParameter("P_IDLOTE", OracleDbType.Int64, idLote, ParameterDirection.Input));
            parameters.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("RTA_PKG_MELER_COTIZADOR.RTA_PRO_GET_VALIDA_NROOPERACION_AFILIADO", parameters))
            {
                try
                {
                    entityErrores = dr.ReadRows<LST_VALIDA_MAX_NROOPERACION>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al listar las GetValidaMaxCotizacionNroOperacion:" + ex.Message);
                    throw new Exception();
                }
            }

            return entityErrores;
        }
    }
}
