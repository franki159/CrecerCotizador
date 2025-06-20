using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Oracle.DataAccess.Client;
using System.Persistence.Connection;
using System.Infrastructure.Tools.Extensions;
using System.Data;
using System.Domain.Entities.Dashboard;
using System.Infrastructure.Utilities.Utilities;

namespace System.Persistence.Data.Dashboard
{
    public class DashboardData : DataContextBase
    {

        public List<LST_GET_CHART_JS> getSeriesChartBar(int nChartData, Int64 nIdUser)
        {
            try
            {
                List<LST_GET_CHART_JS> lstSeries = null;
                LST_GET_CHART_JS eSerie = null;

                List<OracleParameter> parameter = new List<OracleParameter>();
                parameter.Add(new OracleParameter("NTYPE", OracleDbType.Int32, nChartData,ParameterDirection.Input));
                parameter.Add(new OracleParameter("NIDUSER", OracleDbType.Int64, nIdUser, ParameterDirection.Input));
                parameter.Add(new OracleParameter("RC1", OracleDbType.RefCursor, ParameterDirection.Output));

            
                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("SIGMA.PKG_SIGMA_STATISTICS.SP_SIGMA_GET_DATA_CHART", parameter)){

                    lstSeries = new List<LST_GET_CHART_JS>();
                    while (dr.Read())
                    {
                        eSerie = new LST_GET_CHART_JS();

                        eSerie.nIdSerie = dr["NSERIE"] != DBNull.Value ? Int32.Parse(dr["NSERIE"].ToString()) : 0;                        
                        eSerie.sSerie = dr["SSERIE"] != DBNull.Value ? dr["SSERIE"].ToString() : string.Empty;

                        int[] lstValMonth = new int[dr.FieldCount - 2];
                        string[] lstLabels = new string[dr.FieldCount - 2];

                        for (int i = 0; i < dr.FieldCount - 2; i++)
                        {
                            lstValMonth[i] = dr[i + 2] != DBNull.Value ? Int32.Parse(dr[i+2].ToString()) : 0;
                            lstLabels[i] = dr.GetName(i + 2).ToString();
                        }

                        eSerie.nValSerie = lstValMonth;
                        eSerie.sLabels = lstLabels;

                        lstSeries.Add(eSerie);
                    }

                }
                return lstSeries;
            }
            catch(Exception ex) {
                Utilities.GuardarLog("Error al obtener data Dashboard getSeriesChartBar - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

           
        }



        public List<LST_GET_CHART_JS> getPolizasXSumaAseguradaChartBar(ChartFiltro filtro)
        {
            try
            {
                List<LST_GET_CHART_JS> lstSeries = null;
                LST_GET_CHART_JS eSerie = null;
                List<LST_GET_CHART_JS> lstSeriesF = new List<LST_GET_CHART_JS>();
                List<OracleParameter> parameter = new List<OracleParameter>();
                parameter.Add(new OracleParameter("P_SRANGE_INI", OracleDbType.Date, filtro.fechaIni, ParameterDirection.Input));
                parameter.Add(new OracleParameter("P_SRANGE_FIN", OracleDbType.Date, filtro.fechafin, ParameterDirection.Input));
                parameter.Add(new OracleParameter("RC1", OracleDbType.RefCursor, ParameterDirection.Output));

            

                lstSeries = new List<LST_GET_CHART_JS>();
                int i = 0;
                int j = 0;
                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_DASHBOARD.SP_SISPOC_SUMA_ASEGURADA", parameter))
                {

                    while (dr.Read())
                    {
                        eSerie = new LST_GET_CHART_JS();

                        eSerie.TotalSoles = dr["TotalSoles"] != DBNull.Value ? Int32.Parse(dr["TotalSoles"].ToString()) : 0;
                        eSerie.TotalDolares = dr["TotalDolares"] != DBNull.Value ? Int32.Parse(dr["TotalDolares"].ToString()) : 0;
                        eSerie.sSerie = dr["Rango"] != DBNull.Value ? dr["Rango"].ToString() : string.Empty;
                        i++;
                        lstSeries.Add(eSerie);
                    }
                }

                int[] lstValMonthSoles = new int[i];
                string[] lstLabelsSoles = new string[i];
                int[] lstValMonthDolares = new int[i];
                string[] lstLabelsDolares = new string[i];

                LST_GET_CHART_JS eSerieF = new LST_GET_CHART_JS();


                foreach (var item in lstSeries)
                {
                    eSerieF = new LST_GET_CHART_JS();

                    lstValMonthSoles[j] = item.TotalSoles;
                    lstLabelsSoles[j] = item.sSerie;
                    lstValMonthDolares[j] = item.TotalDolares;
                    lstLabelsDolares[j] = item.sSerie;
                    j++;
                }

                eSerieF.nIdSerie = 1;
                eSerieF.sSerie = "Soles";
                eSerieF.nValSerie = lstValMonthSoles;
                eSerieF.sLabels = lstLabelsSoles;

                lstSeriesF.Add(eSerieF);
                eSerieF = new LST_GET_CHART_JS();
                eSerieF.nIdSerie = 2;
                eSerieF.sSerie = "Dólares";
                eSerieF.nValSerie = lstValMonthDolares;
                eSerieF.sLabels = lstLabelsDolares;

                lstSeriesF.Add(eSerieF);
                return lstSeriesF;

            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al obtener data Dashboard getPolizasXSumaAseguradaChartBar - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

        }

        public List<LST_GET_CHART_JS> getPolizasXCorredorChartBar(ChartFiltro filtro)
        {
            try
            {
                List<LST_GET_CHART_JS> lstSeries = new List<LST_GET_CHART_JS>();
                LST_GET_CHART_JS eSerie = new LST_GET_CHART_JS();

                List<LST_GET_CHART_JS> lstSeriesF = new List<LST_GET_CHART_JS>();
                LST_GET_CHART_JS eSerieF = new LST_GET_CHART_JS();
                int j = 0;

                List<OracleParameter> parameter = new List<OracleParameter>();
                parameter.Add(new OracleParameter("P_SRANGE_INI", OracleDbType.Date, filtro.fechaIni, ParameterDirection.Input));
                parameter.Add(new OracleParameter("P_SRANGE_FIN", OracleDbType.Date, filtro.fechafin, ParameterDirection.Input));
                parameter.Add(new OracleParameter("RC1", OracleDbType.RefCursor, ParameterDirection.Output));

            
                int i = 0;
                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_DASHBOARD.SP_SISPOC_CORREDOR_SEGUROS", parameter))
                {

                    lstSeries = new List<LST_GET_CHART_JS>();

                    while (dr.Read())
                    {
                        eSerie = new LST_GET_CHART_JS();

                        eSerie.nIdSerie = dr["Total"] != DBNull.Value ? Int32.Parse(dr["Total"].ToString()) : 0;
                        eSerie.sSerie = dr["Broker"] != DBNull.Value ? dr["Broker"].ToString() : string.Empty;
                        i++;
                        lstSeries.Add(eSerie);
                    }              
                }

                int[] lstValMonth = new int[i];
                string[] lstLabels = new string[i];
                foreach (var item in lstSeries)
                {
                    eSerieF = new LST_GET_CHART_JS();
                    lstValMonth[j] = item.nIdSerie;
                    lstLabels[j] = item.sSerie;
                    eSerieF.nValSerie = lstValMonth;
                    eSerieF.sLabels = lstLabels;
                    j++;
                }
                eSerieF.nValSerie = lstValMonth;
                eSerieF.sLabels = lstLabels;
                lstSeriesF.Add(eSerieF);

                return lstSeriesF;
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al obtener data Dashboard getPolizasXCorredorChartBar - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

            
        }

        public List<LST_GET_CHART_JS> getPolizasXProductoChartBar(ChartFiltro filtro)
        {
            try
            {
                List<LST_GET_CHART_JS> lstSeries = new List<LST_GET_CHART_JS>();
                LST_GET_CHART_JS eSerie = new LST_GET_CHART_JS();

                List<LST_GET_CHART_JS> lstSeriesF = new List<LST_GET_CHART_JS>();
                LST_GET_CHART_JS eSerieF = new LST_GET_CHART_JS();
                int j = 0;

                List<OracleParameter> parameter = new List<OracleParameter>();
                parameter.Add(new OracleParameter("P_SRANGE_INI", OracleDbType.Date, filtro.fechaIni, ParameterDirection.Input));
                parameter.Add(new OracleParameter("P_SRANGE_FIN", OracleDbType.Date, filtro.fechafin, ParameterDirection.Input));
                parameter.Add(new OracleParameter("RC1", OracleDbType.RefCursor, ParameterDirection.Output));

            
                int i = 0;
                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_DASHBOARD.SP_SISPOC_PRODUCTO", parameter))
                {

                    lstSeries = new List<LST_GET_CHART_JS>();                 
                  
                    while (dr.Read())
                    {
                        eSerie = new LST_GET_CHART_JS();

                        eSerie.nIdSerie = dr["Total"] != DBNull.Value ? Int32.Parse(dr["Total"].ToString()) : 0;
                        eSerie.sSerie = dr["Producto"] != DBNull.Value ? dr["Producto"].ToString() : string.Empty;

                        i++;
                        lstSeries.Add(eSerie);
                    }

                }

                int[] lstValMonth = new int[i];
                string[] lstLabels = new string[i];
                foreach (var item in lstSeries)
                {
                    eSerieF = new LST_GET_CHART_JS();
                    lstValMonth[j] = item.nIdSerie;// dr[i] != DBNull.Value ? Int32.Parse(dr[i].ToString()) : 0;
                    lstLabels[j] = item.sSerie;//  dr.GetName(i).ToString();
                    eSerieF.nValSerie = lstValMonth;
                    eSerieF.sLabels = lstLabels;
                    j++;
                }

                eSerieF.nValSerie = lstValMonth;
                eSerieF.sLabels = lstLabels;
                lstSeriesF.Add(eSerieF);

                return lstSeriesF;
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al obtener data Dashboard getPolizasXProductoChartBar - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

        }


        public List<LST_GET_CHART_JS> getPolizasXUbigeoChartBar(ChartFiltro filtro)
        {
            try
            {
                List<LST_GET_CHART_JS> lstSeries = new List<LST_GET_CHART_JS>();
                LST_GET_CHART_JS eSerie = new LST_GET_CHART_JS();

                List<LST_GET_CHART_JS> lstSeriesF = new List<LST_GET_CHART_JS>();
                LST_GET_CHART_JS eSerieF = new LST_GET_CHART_JS();
                int j = 0;

                List<OracleParameter> parameter = new List<OracleParameter>();
                parameter.Add(new OracleParameter("P_SRANGE_INI", OracleDbType.Date, filtro.fechaIni, ParameterDirection.Input));
                parameter.Add(new OracleParameter("P_SRANGE_FIN", OracleDbType.Date, filtro.fechafin, ParameterDirection.Input));
                parameter.Add(new OracleParameter("RC1", OracleDbType.RefCursor, ParameterDirection.Output));

            
                int i = 0;
                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_DASHBOARD.SP_SISPOC_UBIGEO", parameter))
                {

                    lstSeries = new List<LST_GET_CHART_JS>();

                    while (dr.Read())
                    {
                        eSerie = new LST_GET_CHART_JS();

                        eSerie.nIdSerie = dr["Total"] != DBNull.Value ? Int32.Parse(dr["Total"].ToString()) : 0;
                        eSerie.sSerie = dr["SDEPARTAMENTO"] != DBNull.Value ? dr["SDEPARTAMENTO"].ToString() : string.Empty;
                        i++;
                        lstSeries.Add(eSerie);
                    }
                }

                int[] lstValMonth = new int[i];
                string[] lstLabels = new string[i];
                foreach (var item in lstSeries)
                {
                    eSerieF = new LST_GET_CHART_JS();
                    lstValMonth[j] = item.nIdSerie;
                    lstLabels[j] = item.sSerie;
                    eSerieF.nValSerie = lstValMonth;
                    eSerieF.sLabels = lstLabels;
                    j++;
                }
                eSerieF.nValSerie = lstValMonth;
                eSerieF.sLabels = lstLabels;
                lstSeriesF.Add(eSerieF);

                return lstSeriesF;
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al obtener data Dashboard getPolizasXProductoChartBar - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

        }

        public List<LST_GET_CHART_JS> getPolizasXRangoPrimaChartBar(ChartFiltro filtro)
        {
            try
            {
                List<LST_GET_CHART_JS> lstSeries = null;
                LST_GET_CHART_JS eSerie = null;
                List<LST_GET_CHART_JS> lstSeriesF = new List<LST_GET_CHART_JS>();
                List<OracleParameter> parameter = new List<OracleParameter>();
                parameter.Add(new OracleParameter("P_SRANGE_INI", OracleDbType.Date, filtro.fechaIni, ParameterDirection.Input));
                parameter.Add(new OracleParameter("P_SRANGE_FIN", OracleDbType.Date, filtro.fechafin, ParameterDirection.Input));
                parameter.Add(new OracleParameter("RC1", OracleDbType.RefCursor, ParameterDirection.Output));

            

                lstSeries = new List<LST_GET_CHART_JS>();
                int i = 0;
                int j = 0;
                using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_DASHBOARD.SP_SISPOC_RANGO_PRIMA", parameter))
                {
                 
                    while (dr.Read())
                    {
                        eSerie = new LST_GET_CHART_JS();

                        eSerie.TotalSoles = dr["TotalSoles"] != DBNull.Value ? Int32.Parse(dr["TotalSoles"].ToString()) : 0;
                        eSerie.TotalDolares = dr["TotalDolares"] != DBNull.Value ? Int32.Parse(dr["TotalDolares"].ToString()) : 0;
                        eSerie.sSerie = dr["Rango"] != DBNull.Value ? dr["Rango"].ToString() : string.Empty;
                        i++;
                        lstSeries.Add(eSerie);
                    }
                }

                    int[] lstValMonthSoles = new int[i];
                    string[] lstLabelsSoles = new string[i];
                    int[] lstValMonthDolares = new int[i];
                    string[] lstLabelsDolares = new string[i];

                     LST_GET_CHART_JS eSerieF = new LST_GET_CHART_JS();
                   
                
                    foreach (var item in lstSeries)
                      {
                            eSerieF = new LST_GET_CHART_JS();

                        lstValMonthSoles[j] = item.TotalSoles;
                        lstLabelsSoles[j] = item.sSerie;
                    lstValMonthDolares[j] = item.TotalDolares;
                    lstLabelsDolares[j] = item.sSerie;                           
                            j++;
                        }

                    eSerieF.nIdSerie = 1;
                    eSerieF.sSerie = "Soles";
                    eSerieF.nValSerie = lstValMonthSoles;
                    eSerieF.sLabels = lstLabelsSoles;

                    lstSeriesF.Add(eSerieF);
                eSerieF = new LST_GET_CHART_JS();
                eSerieF.nIdSerie = 2;
                eSerieF.sSerie = "Dólares";
                eSerieF.nValSerie = lstValMonthDolares;
                eSerieF.sLabels = lstLabelsDolares;

                lstSeriesF.Add(eSerieF);
               
                return lstSeriesF;
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al obtener data Dashboard getPolizasXProductoChartBar - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                throw ex;
            }

           
        }


    }
}
