using Microsoft.Reporting.WebForms;
using System;
using System.Application.Business.Actorial;
using System.Application.Business.Comercial;
using System.Collections.Generic;
using System.Domain.Entities.Actorial;
using System.Domain.Entities.Actorial.Parameters;
using System.Domain.Entities.Comercial;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Web.Classes.Controllers;

namespace Web.Controllers.Report
{
    public class ReportController : ControllerCustomBase
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileContentResult ReporteComercialMealer(PARAMS_GET_MEALER objParameters)
        {
            string fileReportName = string.Format("Reporte {0}", "Input Meler");
            MealerBusiness oMealerBusiness = new MealerBusiness();
            IEnumerable<LST_REPORTE_MEALER> eEntidadResult = null;
            //Setting objsetting = new Setting();
            //userSession = (CRE_SESSION)Session["ObjUsuarioSession"];
            //objParameters.NIDUSER = Convert.ToInt32(userSession.NIDUSER);

            try
            {
                eEntidadResult = oMealerBusiness.GetReporteComercialMealer(objParameters);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            ReportDataSource dsSecurityUser = new ReportDataSource();
            dsSecurityUser.Name = "dsComercial";
            dsSecurityUser.Value = eEntidadResult;

            IEnumerable<ReportDataSource> datasets = new List<ReportDataSource> { dsSecurityUser };

            ReportParameter p1 = new ReportParameter("pNameReport", "Reporte Input Meler");

            IEnumerable<ReportParameter> parameters = new List<ReportParameter> { p1 }; //, p2, p3 };

            return this.ReportRender(Server.MapPath("~/Reports/Formulario/ReporteInputMealer.rdlc"),
                        fileReportName,
                        enuReportFileFormat.EXCEL, datasets, parameters);
        }

        [HttpGet]
        public FileContentResult DownloadXml(PARAMS_GET_OUTPUT_MELER objParameters)
        {
            ActorialBusiness oActorialBusiness = new ActorialBusiness();
            IEnumerable<LST_REPORTE_OUTPUT_MELER> eEntidadResult = null;
            List<LST_REPORTE_OUTPUT_MELER> lista = new List<LST_REPORTE_OUTPUT_MELER>();
            string path = Server.MapPath("~/FileTmp/");
            path += "Output_Meler_" + objParameters.P_IDLOTE.ToString() + ".xml";

            byte[] fileContents = null;

            if (System.IO.File.Exists(path))
            {
                fileContents = System.IO.File.ReadAllBytes(path);
                //return File(fileContents, "text/xml", "sample.xml");
            }
            else
            {
                try
                {
                    eEntidadResult = oActorialBusiness.GetReporteOutputMeler(objParameters);
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }

                if (eEntidadResult != null)
                    lista = eEntidadResult.ToList();

                if (lista.Count > 0)
                {
                    //List<LST_REPORTE_OUTPUT_MELER> listGrupo = new List<LST_REPORTE_OUTPUT_MELER>();
                    var listGrupo = lista.GroupBy(x => x.NLINEA).ToList();
                    
                    if (listGrupo.Count > 0)
                    {
                        XmlDocument xml = new XmlDocument();
                        XmlNode root = xml.CreateElement(lista[0].SNODE_PRI);
                        xml.AppendChild(root);

                        LST_REPORTE_OUTPUT_MELER obeSolicitud = new LST_REPORTE_OUTPUT_MELER();
                        List<LST_REPORTE_OUTPUT_MELER> lstProducto = new List<LST_REPORTE_OUTPUT_MELER>();

                        foreach (var item in listGrupo)
                        {
                            obeSolicitud = lista.Find(delegate (LST_REPORTE_OUTPUT_MELER obj) { return obj.NLINEA == (Decimal)item.Key; });

                            if (obeSolicitud != null)
                            {
                                XmlNode nodeSol = xml.CreateElement(obeSolicitud.SNODE_SEC);

                                XmlNode nodeSolOpe = xml.CreateElement(obeSolicitud.SNROOPERACION_XML);
                                nodeSolOpe.InnerText = obeSolicitud.NROOPERACION.ToString();
                                nodeSol.AppendChild(nodeSolOpe);

                                XmlNode nodeSolCuss = xml.CreateElement(obeSolicitud.CUSPP_XML);
                                nodeSolCuss.InnerText = obeSolicitud.SCUSPP;
                                nodeSol.AppendChild(nodeSolCuss);

                                //Producto
                                lstProducto = lista.FindAll(delegate (LST_REPORTE_OUTPUT_MELER obj) { return obj.NLINEA == (Decimal)item.Key && obj.CORRELATIVOPROD > 0; });

                                foreach (LST_REPORTE_OUTPUT_MELER itemProd in lstProducto)
                                {
                                    XmlNode nodeProducto = xml.CreateElement(obeSolicitud.PRODUCTO_NODE);

                                    if (itemProd.SMODALIDAD != null)
                                    {
                                        if (!itemProd.SMODALIDAD.Equals(String.Empty))
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SMODALIDAD_XML);
                                            xmlnode.InnerText = itemProd.SMODALIDAD;
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.SMONEDA != null)
                                    {
                                        if (!itemProd.SMONEDA.Equals(String.Empty))
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SMONEDA_XML);
                                            xmlnode.InnerText = itemProd.SMONEDA;
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NANOSRT != null)
                                    {
                                        if (itemProd.NANOSRT > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SANOSRT_XML);
                                            xmlnode.InnerText = itemProd.NANOSRT.ToString();
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NPORCENTAJERVD != null)
                                    {
                                        if (itemProd.NPORCENTAJERVD > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPORCENTAJERVD_XML);
                                            xmlnode.InnerText = itemProd.NPORCENTAJERVD.ToString();
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NPERIODOGARANTIZADO != null)
                                    {
                                        if (itemProd.NPERIODOGARANTIZADO > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPERIODOGARANTIZADO_XML);
                                            xmlnode.InnerText = itemProd.NPERIODOGARANTIZADO.ToString();
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.SDERECHOCRECER != null)
                                    {
                                        if (!itemProd.SDERECHOCRECER.Equals(String.Empty))
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SDERECHOCRECER_XML);
                                            xmlnode.InnerText = itemProd.SDERECHOCRECER;
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.SGRATIFICACION != null)
                                    {
                                        if (!itemProd.SGRATIFICACION.Equals(String.Empty))
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SGRATIFICACION_XML);
                                            xmlnode.InnerText = itemProd.SGRATIFICACION;
                                            nodeProducto.AppendChild(xmlnode);
                                        }
                                    }

                                    //COTIZACION
                                    XmlNode nodeCotizacion = xml.CreateElement(obeSolicitud.COTIZADO_NODE);

                                    if (itemProd.SSICOTIZANOCOTIZA != null)
                                    {
                                        if (!itemProd.SSICOTIZANOCOTIZA.Equals(String.Empty))
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SSICOTIZANOCOTIZA_XML);
                                            xmlnode.InnerText = itemProd.SSICOTIZANOCOTIZA;
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NROCOTIZACION != null)
                                    {
                                        if (itemProd.NROCOTIZACION > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SNROCOTIZACION_XML);
                                            xmlnode.InnerText = itemProd.NROCOTIZACION.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NPRIMAUNICAAFPEESS != null)
                                    {
                                        if (itemProd.NPRIMAUNICAAFPEESS > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPRIMAUNICAAFPEESS_XML);
                                            xmlnode.InnerText = itemProd.NPRIMAUNICAAFPEESS.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NPRIMAUNICAEESS != null)
                                    {
                                        if (itemProd.NPRIMAUNICAEESS > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPRIMAUNICAEESS_XML);
                                            xmlnode.InnerText = itemProd.NPRIMAUNICAEESS.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NPRIMERAPENSIONRV != null)
                                    {
                                        if (itemProd.NPRIMERAPENSIONRV > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPRIMERAPENSIONRV_XML);
                                            xmlnode.InnerText = itemProd.NPRIMERAPENSIONRV.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NTASAINTERESRV != null)
                                    {
                                        if (itemProd.NTASAINTERESRV > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.STASAINTERESRV_XML);
                                            xmlnode.InnerText = itemProd.NTASAINTERESRV.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }
                                    
                                    if (itemProd.NPRIMERAPENSIONRT != null)
                                    {
                                        if (itemProd.NPRIMERAPENSIONRT > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPRIMERAPENSIONRT_XML);
                                            xmlnode.InnerText = itemProd.NPRIMERAPENSIONRT.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NTASAINTERESRT != null)
                                    {
                                        if (itemProd.NTASAINTERESRT > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.STASAINTERESRT_XML);
                                            xmlnode.InnerText = itemProd.NTASAINTERESRT.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NPRIMERAPENSIONRVD != null)
                                    {
                                        if (itemProd.NPRIMERAPENSIONRVD > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.SPRIMERAPENSIONRVD_XML);
                                            xmlnode.InnerText = itemProd.NPRIMERAPENSIONRVD.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    if (itemProd.NTASAINTERESRVD != null)
                                    {
                                        if (itemProd.NTASAINTERESRVD > 0)
                                        {
                                            XmlNode xmlnode = xml.CreateElement(itemProd.STASAINTERESRVD_XML);
                                            xmlnode.InnerText = itemProd.NTASAINTERESRVD.ToString();
                                            nodeCotizacion.AppendChild(xmlnode);
                                        }
                                    }

                                    root.AppendChild(nodeSol);
                                    root.AppendChild(nodeSol).AppendChild(nodeProducto);
                                    root.AppendChild(nodeSol).AppendChild(nodeProducto).AppendChild(nodeCotizacion);
                                }
                            }
                        }

                        xml.Save(path);
                    }

                    if (System.IO.File.Exists(path))
                        fileContents = System.IO.File.ReadAllBytes(path);
                }
            }

            String NombreArchivoAutogenerado = "Output_Meler_" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("hhmmss") + ".xml";

            return File(fileContents, "text/xml", NombreArchivoAutogenerado);
        }

        [HttpGet]
        public FileContentResult ReporteVariableDet(PARAMS_GET_MEALER objParameters)
        {
            
            string fileReportName = string.Format("Reporte {0}", "Reporte Variables");
            MealerBusiness oMealerBusiness = new MealerBusiness();
            IEnumerable<LST_REPORTE_VARIABLE_DET> eEntidadResult = null;
            //IEnumerable<LST_REPORTE_VARIABLE_DET_PROD> eEntidadResult2 = null;
            try
            {
                eEntidadResult = oMealerBusiness.GetReporteVariableDet(objParameters);
                //eEntidadResult2 = oMealerBusiness.GetReporteVariableDetProd(objParameters);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            ReportDataSource dsSecurityUser = new ReportDataSource();
            dsSecurityUser.Name = "dsVariable";
            dsSecurityUser.Value = eEntidadResult;



            IEnumerable<LST_REPORTE_VARIABLE_MOR> eEntidadResult2 = null;
            try
            {
                eEntidadResult2 = oMealerBusiness.GetReporteVariableMor(objParameters);
                //eEntidadResult2 = oMealerBusiness.GetReporteVariableDetProd(objParameters);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            ReportDataSource dsSecurityUser2 = new ReportDataSource();
            dsSecurityUser2.Name = "dsMortalidad";
            dsSecurityUser2.Value = eEntidadResult2;

            //ReportDataSource dsSecurityUser = new ReportDataSource();
            //dsSecurityUser.Name = "dsVariable";
            //dsSecurityUser.Value = eEntidadResult;

            IEnumerable<ReportDataSource> datasets = new List<ReportDataSource> { dsSecurityUser, dsSecurityUser2 };

            ReportParameter p1 = new ReportParameter("pNameReport", "Reporte Lista de Variables");

            IEnumerable<ReportParameter> parameters = new List<ReportParameter> { p1 }; //, p2, p3 };

            return this.ReportRender(Server.MapPath("~/Reports/Formulario/ReporteVariable.rdlc"),
                        fileReportName,
                        enuReportFileFormat.EXCEL, datasets, parameters);
        }
        [HttpGet]
        public FileContentResult ReporteVariableMor(PARAMS_GET_MEALER objParameters)
        {

            string fileReportName = string.Format("Reporte {0}", "Reporte Mortalidad");
            MealerBusiness oMealerBusiness = new MealerBusiness();
            IEnumerable<LST_REPORTE_VARIABLE_MOR> eEntidadResult = null;
            //IEnumerable<LST_REPORTE_VARIABLE_DET_PROD> eEntidadResult2 = null;
            try
            {
                eEntidadResult = oMealerBusiness.GetReporteVariableMor(objParameters);
                //eEntidadResult2 = oMealerBusiness.GetReporteVariableDetProd(objParameters);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            ReportDataSource dsSecurityUser = new ReportDataSource();
            dsSecurityUser.Name = "dsMortalidad";
            dsSecurityUser.Value = eEntidadResult;

            //ReportDataSource dsSecurityUser = new ReportDataSource();
            //dsSecurityUser.Name = "dsVariable";
            //dsSecurityUser.Value = eEntidadResult;

            IEnumerable<ReportDataSource> datasets = new List<ReportDataSource> { dsSecurityUser };

            ReportParameter p1 = new ReportParameter("pNameReport", "Reporte de Mortalidad");

            IEnumerable<ReportParameter> parameters = new List<ReportParameter> { p1 }; //, p2, p3 };

            return this.ReportRender(Server.MapPath("~/Reports/Formulario/ReporteBloque3.rdlc"),
                        fileReportName,
                        enuReportFileFormat.EXCEL, datasets, parameters);
        }
    }
}