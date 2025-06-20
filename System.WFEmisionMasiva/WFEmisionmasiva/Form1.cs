using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;
using System.Application.Business.Caution;
using System.Domain.Entities.Policy;
using System.Domain.Entities.Contractor;
using System.Domain.Entities.Security;
using System.Domain.Entities.Tools;
using System.Application.Business.Policy;

using System.Text.RegularExpressions;
using static System.Domain.Entities.Caution.BEAN_AFILIACION;
using System.Application.Business.Services.Alert;

namespace WFEmisionmasiva
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEmitirPolzias_Click(object sender, EventArgs e)
        {
            try
            {
                AlertBusiness alert = new AlertBusiness();
                alert.EnviarAlertasCorreo();
                //TEST JUNIOR
                return;
                
                using (OracleConnection conn = new OracleConnection("User Id=SISPOC;Password=SISPOC; Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.31.7.103)(PORT = 1525)) ) (CONNECT_DATA =  (sid=exactus) ));"))
                using (OracleCommand cmd = new OracleCommand(@"SELECT P.NID_SOLICITUD FROM MIG_SISPOC_POLIZA P", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PARAM_POLICY objParametros = new PARAM_POLICY();
                            objParametros.PNID_SOLICITUD = reader.GetInt32(0);
                            AprobarPoliza(objParametros);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCargarSolicitudes_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.ColumnCount = 6;
                dataGridView1.Columns[0].Name = "Poliza";
                dataGridView1.Columns[1].Name = "Cod. Cliente";
                dataGridView1.Columns[2].Name = "Nom. Cliente";
                dataGridView1.Columns[3].Name = "Cod. Empresa Facturar";
                dataGridView1.Columns[4].Name = "Producto";
                dataGridView1.Columns[5].Name = "Cobertura";

                using (OracleConnection conn = new OracleConnection("User Id=SISPOC;Password=SISPOC; Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.31.7.103)(PORT = 1525)) ) (CONNECT_DATA =  (sid=exactus) ));"))
                using (OracleCommand cmd = new OracleCommand(@"SELECT P.SID_POLIZA, P.NID_CLIENTE,C.SRAZON_SOCIAL, 
                                                                P.NID_EMPRESA_FACTURAR, P.SPRODUCTO, P.SCOBERTURA 
                                                                FROM MIG_SISPOC_POLIZA P 
                                                                INNER JOIN TBL_SISPOC_CLIENTE C ON C.NID_CLIENTE = P.NID_CLIENTE", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            //Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                            ArrayList fila = new ArrayList();
                            fila.Add(reader.GetString(0));
                            fila.Add(reader.GetInt32(1));
                            fila.Add(reader.GetString(2));
                            fila.Add(reader.GetInt32(3));
                            fila.Add(reader.GetString(4));
                            fila.Add(reader.GetString(5));
                            dataGridView1.Rows.Add(fila.ToArray());

                            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                            chk.HeaderText = "Seleccione";
                            chk.Name = "CheckBox";
                            dataGridView1.Columns.Add(chk);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AprobarPoliza(PARAM_POLICY objParametros)
        {
            PolicyBusiness businesss = new PolicyBusiness();
            IEnumerable<LST_MENSAJE> resultado = Enumerable.Empty<LST_MENSAJE>();

            //Get parameter session
            //CRE_SESSION objSession = (CRE_SESSION)HttpContext.Session["ObjUsuarioSession"];
            //Set parameter user id for insert profile
            objParametros.PNUSER = "99999";

            try
            {
                string token = "";
                token = new CautionBusiness().ObtenerAcreditacionServicio();
                if (string.IsNullOrEmpty(token))
                {
                    //mensaje = "El servicio Acsel-e para la generación de la poliza no esta disponible",;
                    //codigocliente = "0";
                }

                //resultado = businesss.AprobarPoliza(objParametros);

                /*ALERTAS*/
                //if (resultado.FirstOrDefault().EXITO == 1)
                //{
                try
                {

                    entityRptaPolicy rpta = new entityRptaPolicy();
                    string NumeroPoliza = string.Empty;
                    //rpta = new CautionBusiness().SearchAcseleThird(objParametros.PNID_SOLICITUD, 
                    //resultado.FirstOrDefault().CODIGO_HISTORIAL.ToString(), resultado.FirstOrDefault().CODIGO_FLUJO.ToString(), token);
                    rpta = new CautionBusiness().SearchAcseleThird(objParametros.PNID_SOLICITUD, "", "", token);
                    if (rpta.rptaOk != null)
                    {
                        NumeroPoliza = rpta.rptaOk.numeroPoliza;
                    }

                    if ((!string.IsNullOrEmpty(token)))
                    {
                        new CautionBusiness().CerrarToken(token);
                    }

                    // new CautionBusiness().AltaPolicy(objParametros.PNID_SOLICITUD, token);
                    if (!(string.IsNullOrEmpty(NumeroPoliza))) //si completo el proceso envia ALERTA
                    {
                        resultado = businesss.AprobarPoliza(objParametros);
                    }
                    else
                    {
                        if (rpta.rptaError != null)
                        {
                            //mensaje = rpta.rptaError.errores.FirstOrDefault().errorMessage,
                            //codigocliente = "Error!"
                        }
                    }

                }
                catch (Exception ex) { }
                //}
                /*FIN ALERTAS*/


            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
        }

    }
}
