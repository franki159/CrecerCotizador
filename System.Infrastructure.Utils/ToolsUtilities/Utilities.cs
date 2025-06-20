using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Oracle.DataAccess.Client;
using System.Infrastructure.Tools.Extensions;
using System.Persistence.Connection;
using System.Web;
using System.Persistence.Core;
using System.Configuration;
using System.Infrastructure.Tools.Services.Models;
using System.Infrastructure.Tools.Services;
using System.Domain.Entities.Tools.Parameters;

namespace System.Infrastructure.Utilities.Utilities
{
    public class Utilities : DataContextBase
    {
        //Variable log
        public static string RutaLog { get; set; }
        public static string RutaConexion { get; set; }

        public static void GuardarLog(string mensaje)
        {

            RutaLog = ConfigurationManager.AppSettings["LogLocal"].ToString();
            //Save in file log
            FileStream stream = null;
            StreamWriter writer = null;

            string dateString = DateTime.Now.ToShortDateString();
            dateString = dateString.Replace("/", "");

            string fileLog = "LogSispoc-" + dateString.ToString() + ".txt";
            string rutaLogApp = RutaLog + "\\" + fileLog;

            if (!File.Exists(rutaLogApp))
            {
                //Crear el archivo txt de log
                stream = new FileStream(rutaLogApp, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(stream);
                writer.Write(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + mensaje.ToString());
                writer.WriteLine();
                writer.Close();
            }
            else
            {
                //Edita el archivo txt de log
                //stream = new FileStream(rutaLogApp, FileMode.Append, FileAccess.Write);
                //writer = new StreamWriter(stream);
                //writer.Write(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + mensaje.ToString());
                //writer.WriteLine();
                //writer.Close();
            }

            //Save in database
            List<OracleParameter> parameters = new List<OracleParameter>();

            PARAMS_INS_LOG objParametros = new PARAMS_INS_LOG();
            objParametros.P_SDESCMESS = mensaje;
            objParametros.P_NUSERCODE = 1;//(Int64)HttpContext.Current.Session["1"];

            try
            {
                //Get connection
                RutaConexion = ConfigurationManager.ConnectionStrings["cnxStringOracle"].ConnectionString;

                //Establis connection
                OracleConnection DataConnectionOracle = new OracleConnection(RutaConexion);

                //Configuration command
                DbConnection DataConnection = DataConnectionOracle;
                DbCommand cmdCommand = DataConnection.CreateCommand();
                cmdCommand.CommandText = "RTA_PKG_MELER_COTIZADOR.RTA_PRO_INS_LOG_PROCESO";
                cmdCommand.CommandType = CommandType.StoredProcedure;
                
                parameters.Add(new OracleParameter("P_SAPLICACION", OracleDbType.Varchar2, "RTA_PKG_MELER_COTIZADOR", ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SPAQUETE", OracleDbType.Varchar2, "RTA_PKG_MELER_COTIZADOR", ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SPROCEDIMIENTO", OracleDbType.Varchar2, "RTA_PRO_INS_LOG_PROCESO", ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NTIPO_ERROR", OracleDbType.Varchar2, "3", ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_ERROR", OracleDbType.Varchar2, objParametros.P_SDESCMESS, ParameterDirection.Input));

                if (parameters != null)
                {
                    foreach (DbParameter parameter in parameters)
                    {
                        cmdCommand.Parameters.Add(parameter);
                    }
                }

                DataConnection.Open();
                cmdCommand.ExecuteNonQuery();
                cmdCommand.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static async Task SaveLogInAzure(LogRequest model)
        {
            var apiService = new ApiService();
            var apiCrecerBase = ConfigurationManager.AppSettings["apiCrecerBaseLog"].ToString();
            var apiSeguridadLogCrecer = ConfigurationManager.AppSettings["apiSeguridadLogCrecer"].ToString();
            var apiRegistroLog = ConfigurationManager.AppSettings["apiRegistroLog"].ToString();

            await apiService.Post<LogRequest, VIEW_Respuesta>
                (apiCrecerBase, apiSeguridadLogCrecer, apiRegistroLog, string.Empty,
                string.Empty, model, false);
        }


        public static VIEW_Respuesta GetStringConnectionFromAzure(StringConecctionRequest model)
        {
            var modelStringConn = new StringConecctionRequest();
            modelStringConn.claveSecreto = ConfigurationManager.AppSettings["api_BD_QA_ORA"].ToString();

            var apiService = new ApiService();
            var apiCrecerBase = ConfigurationManager.AppSettings["apiCrecerBaseToken"].ToString();
            var apiSeguridadLogCrecer = ConfigurationManager.AppSettings["apiSeguridadTokenCrecer"].ToString();
            var apiSecretoSeguridadToken = ConfigurationManager.AppSettings["apiSecretoSeguridadToken"].ToString();

            //var respon = apiService.PostNotAsinc<StringConecctionRequest, ApiCrecerResponse<string>>
            var respon = apiService.PostNotAsinc<StringConecctionRequest, VIEW_Respuesta>
    (apiCrecerBase, apiSeguridadLogCrecer, apiSecretoSeguridadToken, string.Empty, string.Empty, model, false);

            var respuesta = ((VIEW_Respuesta)(respon.Resullt));

            return respuesta;
        }

        public static void GuardarLogTXT(string mensaje)
        {

            RutaLog = ConfigurationManager.AppSettings["LogLocal"].ToString();
            //Save in file log
            FileStream stream = null;
            StreamWriter writer = null;

            string dateString = DateTime.Now.ToShortDateString();
            dateString = dateString.Replace("/", "");

            string fileLog = "LogSispoc-" + dateString.ToString() + ".txt";
            string rutaLogApp = RutaLog + "\\" + fileLog;

            if (!File.Exists(rutaLogApp))
            {
                //Crear el archivo txt de log
                stream = new FileStream(rutaLogApp, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(stream);
                writer.Write(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + mensaje.ToString());
                writer.WriteLine();
                writer.Close();
            }
            else
            {
                using (StreamWriter newTask = new StreamWriter(rutaLogApp, true))
                {
                    newTask.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + mensaje.ToString());
                }
            }

        }
    }
}
