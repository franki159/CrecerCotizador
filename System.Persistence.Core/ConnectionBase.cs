using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace System.Persistence.Core
{
    public class ConnectionBase
    {
        static string strConexionOracle = null;
        static string strConexionSQLServer = null;
        private OracleConnection DataConnectionOracle = new OracleConnection(strConexionOracle);
        private SqlConnection DataConnectionSQLServer = new SqlConnection(strConexionSQLServer);

        public enum enuTypeDataBase
        {
            Oracle,
            SQLServer
        }

        public enum enuTypeExecute
        {
            ExecuteNonQuery,
            ExecuteReader
        }

        public static DbParameterCollection ParamsCollectionResult; //SAC- HOMOLOGACION
        public ConnectionBase()
        {
            try
            {
                //ConnectionBase.strConexionOracle = ((VIEW_Respuesta)HttpContext.Current.Session["DB_CONNECTION"])?.Data.ToString();//ConfigurationManager.ConnectionStrings["cnxStringOracle"].ConnectionString;
                ConnectionBase.strConexionOracle = ConfigurationManager.ConnectionStrings["cnxStringOracle"].ConnectionString;
                DataConnectionOracle.ConnectionString = ConnectionBase.strConexionOracle;
            }
            catch (Exception ex)
            {
                //Utilities.GuardarLog("Error al obtener los datos de la alerta:" + ex.Message);
                throw new Exception();
            }
            //ConnectionBase.strConexionSQLServer = ConfigurationManager.ConnectionStrings["cnxStringSQLServer"].ConnectionString;
            //DataConnectionSQLServer.ConnectionString = ConnectionBase.strConexionSQLServer;

        }

        protected DbConnection ConnectionGet(enuTypeDataBase typeDataBase = enuTypeDataBase.Oracle)
        {
            DbConnection DataConnection = null;
            switch (typeDataBase)
            {
                case enuTypeDataBase.Oracle:
                    DataConnection = DataConnectionOracle;
                    break;
                case enuTypeDataBase.SQLServer:
                    DataConnection = DataConnectionSQLServer;
                    break;
                default:
                    break;
            }
            return DataConnection;
        }
        protected DbDataReader ExecuteByStoredProcedure(string nameStore,
                IEnumerable<DbParameter> parameters = null,
                enuTypeDataBase typeDataBase = enuTypeDataBase.Oracle,
                enuTypeExecute typeExecute = enuTypeExecute.ExecuteReader)
        {
            DbConnection DataConnection = ConnectionGet(typeDataBase);
            DbCommand cmdCommand = DataConnection.CreateCommand();
            cmdCommand.CommandText = nameStore;
            cmdCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (DbParameter parameter in parameters)
                {
                    cmdCommand.Parameters.Add(parameter);
                }
            }

            DataConnection.Open();
            DbDataReader myReader;

            if (((cmdCommand.Parameters.Contains("C_TABLE") || IsOracleReader(cmdCommand)) || typeDataBase == enuTypeDataBase.SQLServer) && typeExecute == enuTypeExecute.ExecuteReader)
            {
                myReader = cmdCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                cmdCommand.ExecuteNonQuery();
                ParamsCollectionResult = cmdCommand.Parameters; 
                cmdCommand.Connection.Close();
                myReader = null;
            }

            return myReader;
        }

        protected DbDataReader ExecuteStoredProcedure(string nameStore,
                IEnumerable<DbParameter> parameters,
                int ncursor)
        {

            enuTypeDataBase typeDataBase = enuTypeDataBase.Oracle;

            DbConnection DataConnection = ConnectionGet(typeDataBase);
            DbCommand cmdCommand = DataConnection.CreateCommand();
            cmdCommand.CommandText = nameStore;
            cmdCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (DbParameter parameter in parameters)
                {
                    cmdCommand.Parameters.Add(parameter);
                }
            }

            DataConnection.Open();
            DbDataReader myReader;

            if ((ncursor == 1|| IsOracleReader(cmdCommand)))
            {
                myReader = cmdCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                cmdCommand.ExecuteNonQuery();
                ParamsCollectionResult = cmdCommand.Parameters;
                cmdCommand.Connection.Close();
                myReader = null;
            }

            return myReader;
        }
        protected string ExecuteFuntion(string nameFuntion, int typeResul, List<OracleParameter> parameters)
        {
            string result = null;

            enuTypeDataBase typeDataBase = enuTypeDataBase.Oracle;

            DbConnection DataConnection = ConnectionGet(typeDataBase);
            DbCommand cmdCommand = DataConnection.CreateCommand();
            cmdCommand.CommandText = nameFuntion;
            cmdCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {

                if (typeResul == 1)
                {
                    parameters.Add(new OracleParameter("PSALID", OracleDbType.Int64, ParameterDirection.Output));
                }
                else
                {
                    OracleParameter objParaRet = new OracleParameter("PSALID", OracleDbType.Varchar2, 1000);
                    objParaRet.Direction = ParameterDirection.Output;
                    parameters.Add(objParaRet);
                }


                foreach (DbParameter parameter in parameters)
                {
                    cmdCommand.Parameters.Add(parameter);
                }
            }

            DataConnection.Open();
            cmdCommand.ExecuteNonQuery();
            ParamsCollectionResult = cmdCommand.Parameters;
            cmdCommand.Connection.Close();

            result = cmdCommand.Parameters["PSALID"].Value.ToString();

            return result;
        }

        private bool IsOracleReader(DbCommand cmdCommand)
        {
            bool isOracleReader = false;
            foreach (DbParameter item in cmdCommand.Parameters)
            {
                if (item is OracleParameter)
                {
                    if ((item as OracleParameter).OracleDbType == OracleDbType.RefCursor)
                    {
                        isOracleReader = true;
                        break;
                    }
                }
            }
            return isOracleReader;
        }
    }
}
