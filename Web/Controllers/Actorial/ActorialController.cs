using OfficeOpenXml;
using System;
using System.Application.Business.Actorial;
using System.Application.Business.Comercial;
using System.Collections.Generic;
using System.Domain.Entities.Actorial;
using System.Domain.Entities.Actorial.Parameters;
using System.Domain.Entities.Comercial;
using System.Domain.Entities.Tools;
using System.Infrastructure.Utilities.Utilities;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Web.Controllers.Actorial
{
    public class ActorialController : Controller
    {
        // GET: Actorial
        public ActionResult OutputMeler()
        {
            return View();
        }

        public ActionResult Calculo()
        {
            return View();
        }

        public ActionResult Anualidad()
        {
            return View();
        }

        public JsonResult GetListaActorialMealer(PARAMS_GET_MEALER objParametros)
        {
            MealerBusiness listMealer = new MealerBusiness();
            IEnumerable<LST_GET_MEALER> _entity = null;
            List<LST_GET_MEALER> lista = new List<LST_GET_MEALER>();
            try
            {
                _entity = listMealer.GetListaComercialMealer(objParametros);

                if (_entity != null)
                {
                    lista = _entity.ToList();

                    _entity = lista.FindAll(delegate (LST_GET_MEALER obj) { return !obj.NESTADO.Equals("3"); });
                }
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVerificarNombreArchivoOutputMealer(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            ActorialBusiness listMealer = new ActorialBusiness();
            IEnumerable<LST_GET_OUTPUT_MELER> _entity = null;
            try
            {
                _entity = listMealer.GetVerificarNombreArchivoOutputMealer(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity.FirstOrDefault()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListaActorialOutputMealer(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            ActorialBusiness listMealer = new ActorialBusiness();
            IEnumerable<LST_GET_OUTPUT_MELER> _entity = null;
            try
            {
                _entity = listMealer.GetListaActorialOutputMealer(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            try
            {
                List<LST_MENSAJE> listError = new List<LST_MENSAJE>();
                List<LST_MENSAJE> listErrorExcel = new List<LST_MENSAJE>();
                LST_MENSAJE obeError = new LST_MENSAJE();
                IEnumerable<LST_MENSAJE> entityList = null;
                string path = Server.MapPath("~/FileTmp/");
                string mensajeRespuesta = "";
                string NombreArchivoAutogenerado = "";
                int valido = 1;
                HttpFileCollectionBase files = Request.Files;
                string codigo = Request.Form[0];

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    NombreArchivoAutogenerado = file.FileName.Split('.')[0] + '_' + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("hhmmss") + '.' + file.FileName.Split('.')[1];

                    path += NombreArchivoAutogenerado; 
                    file.SaveAs(path);

                    if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                    {
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
                        {
                            if (package.Workbook.Worksheets.Count >= 1)
                            {
                                int res = validarFormato(package);
                                if (res == 1)
                                {

                                    List<solicitudRecibida> listSolicitudRecibida = new List<solicitudRecibida>();
                                    List<SolicitudRecibidaProducto> listProducto = new List<SolicitudRecibidaProducto>();
                                    List<SolicitudRecibidaCotizacion> listCotizacion = new List<SolicitudRecibidaCotizacion>();

                                    foreach (var hoja in package.Workbook.Worksheets)
                                    {
                                        Utilities.GuardarLog("HOJA ASIGNACION");
                                        String nroOperacion = String.Empty;
                                        List<solicitudRecibida> listSolNueva = new List<solicitudRecibida>();
                                        solicitudRecibida oBeSolicitud = new solicitudRecibida();
                                       
                                        Int32 icont = 0;
                                        Int32 icorrelativo = 0;

                                        Int64 nOperacion;
                                        Int32 iAnio, iPeriodo;
                                        Single dPorcentaje;
                                        long nCotizacion;
                                        Decimal dPrimaUnicaEESS, dPrimeraPensionRV, dTasaInteresRV, dPrimaUnicaAFPEESS,
                                            dPrimaPensionRT, dTasaInteresRT, dPrimeraPensionRVD, dTasaInteresRVD;
                                        String sValor = String.Empty;

                                        for (int filas = 2; filas <= hoja.Dimension.End.Row; filas++)
                                        {
                                            Utilities.GuardarLog("HOJA ASIGNACION - fila: " + filas.ToString() + " NUMERO_OPERACION:" + hoja.Cells[$"A{filas}"].Text.ToString());

                                            oBeSolicitud = new solicitudRecibida();
                                            listSolNueva = new List<solicitudRecibida>();
                                            var solicitud = new solicitudRecibida();

                                            solicitud.nroOperacion = hoja.Cells[$"A{filas}"].Text == "" ? "" : hoja.Cells[$"A{filas}"].Value.ToString();
                                            solicitud.CUSPP = hoja.Cells[$"B{filas}"].Text == "" ? "" : hoja.Cells[$"B{filas}"].Value.ToString();
                                            nroOperacion = solicitud.nroOperacion;

                                            if (!nroOperacion.Trim().Equals(String.Empty))
                                            {
                                                oBeSolicitud = listSolicitudRecibida.Find(delegate (solicitudRecibida obj) { return obj.nroOperacion.Equals(nroOperacion); });

                                                if (oBeSolicitud == null)
                                                {
                                                    icorrelativo = 0;
                                                    icont = icont + 1;
                                                    solicitud.NFILA = icont;
                                                    listSolicitudRecibida.Add(solicitud);
                                                }
                                                
                                                icorrelativo = icorrelativo + 1;
                                                var solicitudProducto = new SolicitudRecibidaProducto();
                                                solicitudProducto.NFILA = icont;
                                                solicitudProducto.NCORRELATIVO = icorrelativo;
                                                solicitudProducto.modalidad = hoja.Cells[$"C{filas}"].Text == "" ? "" : hoja.Cells[$"C{filas}"].Value.ToString();

                                                sValor = String.Empty;
                                                iAnio = 0;
                                                sValor = hoja.Cells[$"D{filas}"].Text == "" ? "" : hoja.Cells[$"D{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Int32.TryParse(sValor, out iAnio);

                                                    if (iAnio <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'anosRT' no tiene el formato correcto (Numero).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dPorcentaje = 0;
                                                sValor = hoja.Cells[$"E{filas}"].Text == "" ? "" : hoja.Cells[$"E{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Single.TryParse(sValor, out dPorcentaje);

                                                    if (dPorcentaje <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'porcentajeRVD' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                iPeriodo = 0;
                                                sValor = hoja.Cells[$"F{filas}"].Text == "" ? "" : hoja.Cells[$"F{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Int32.TryParse(sValor, out iPeriodo);

                                                    if (iPeriodo <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'periodoGarantizado' no tiene el formato correcto (Numero).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                solicitudProducto.anosRT = hoja.Cells[$"D{filas}"].Text == "" ? "" : hoja.Cells[$"D{filas}"].Value.ToString();
                                                solicitudProducto.porcentajeRVD = hoja.Cells[$"E{filas}"].Text == "" ? "" : hoja.Cells[$"E{filas}"].Value.ToString();
                                                solicitudProducto.periodoGarantizado = hoja.Cells[$"F{filas}"].Text == "" ? "" : hoja.Cells[$"F{filas}"].Value.ToString();
                                                solicitudProducto.moneda = hoja.Cells[$"G{filas}"].Text == "" ? "" : hoja.Cells[$"G{filas}"].Value.ToString();
                                                solicitudProducto.derechoCrecer = hoja.Cells[$"H{filas}"].Text == "" ? "" : hoja.Cells[$"H{filas}"].Value.ToString();
                                                solicitudProducto.gratificacion = hoja.Cells[$"I{filas}"].Text == "" ? "" : hoja.Cells[$"I{filas}"].Value.ToString();
                                                listProducto.Add(solicitudProducto);

                                                sValor = string.Empty;
                                                nCotizacion = 0;
                                                sValor = hoja.Cells[$"K{filas}"].Text == "" ? "" : hoja.Cells[$"K{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    long.TryParse(sValor, out nCotizacion);

                                                    if (nCotizacion <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'nroCotizacion' no tiene el formato correcto (Numero).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dPrimaUnicaEESS = 0;
                                                sValor = hoja.Cells[$"L{filas}"].Text == "" ? "" : hoja.Cells[$"L{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dPrimaUnicaEESS);

                                                    if (dPrimaUnicaEESS <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'primaUnicaEESS' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dPrimeraPensionRV = 0;
                                                sValor = hoja.Cells[$"M{filas}"].Text == "" ? "" : hoja.Cells[$"M{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dPrimeraPensionRV);

                                                    if (dPrimeraPensionRV <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'primeraPensionRV' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dTasaInteresRV = 0;
                                                sValor = hoja.Cells[$"N{filas}"].Text == "" ? "" : hoja.Cells[$"N{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dTasaInteresRV);

                                                    if (dTasaInteresRV <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'tasaInteresRV' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dPrimaUnicaAFPEESS = 0;
                                                sValor = hoja.Cells[$"O{filas}"].Text == "" ? "" : hoja.Cells[$"O{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dPrimaUnicaAFPEESS);

                                                    if (dPrimaUnicaAFPEESS <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'primaUnicaAFPEESS' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dPrimaPensionRT = 0;
                                                sValor = hoja.Cells[$"P{filas}"].Text == "" ? "" : hoja.Cells[$"P{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dPrimaPensionRT);

                                                    if (dPrimaPensionRT <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'primeraPensionRT' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dTasaInteresRT = 0;
                                                sValor = hoja.Cells[$"Q{filas}"].Text == "" ? "" : hoja.Cells[$"Q{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dTasaInteresRT);

                                                    if (dTasaInteresRT <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'tasaInteresRT' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dPrimeraPensionRVD = 0;
                                                sValor = hoja.Cells[$"R{filas}"].Text == "" ? "" : hoja.Cells[$"R{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dPrimeraPensionRVD);

                                                    if (dPrimeraPensionRVD <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'primeraPensionRVD' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                sValor = string.Empty;
                                                dTasaInteresRVD = 0;
                                                sValor = hoja.Cells[$"S{filas}"].Text == "" ? "" : hoja.Cells[$"S{filas}"].Value.ToString();

                                                if (!sValor.Trim().Equals(String.Empty))
                                                {
                                                    Decimal.TryParse(sValor, out dTasaInteresRVD);

                                                    if (dTasaInteresRVD <= 0)
                                                    {
                                                        obeError = new LST_MENSAJE();
                                                        obeError.CANTIDAD = filas;
                                                        obeError.MENSAJE = "La columna 'primeraPensionRVD' no tiene el formato correcto (Decimal).";
                                                        listErrorExcel.Add(obeError);
                                                    }
                                                }

                                                var solicitudCotizacion = new SolicitudRecibidaCotizacion();
                                                solicitudCotizacion.NFILA = icont;
                                                solicitudCotizacion.NCORRELATIVO = icorrelativo;
                                                solicitudCotizacion.siCotizaNoCotiza = hoja.Cells[$"J{filas}"].Text == "" ? "" : hoja.Cells[$"J{filas}"].Value.ToString();
                                                solicitudCotizacion.nroCotizacion = hoja.Cells[$"K{filas}"].Text == "" ? "" : hoja.Cells[$"K{filas}"].Value.ToString();
                                                solicitudCotizacion.primaUnicaEESS = hoja.Cells[$"L{filas}"].Text == "" ? "" : hoja.Cells[$"L{filas}"].Value.ToString();
                                                solicitudCotizacion.primeraPensionRV = hoja.Cells[$"M{filas}"].Text == "" ? "" : hoja.Cells[$"M{filas}"].Value.ToString();
                                                solicitudCotizacion.tasaInteresRV = hoja.Cells[$"N{filas}"].Text == "" ? "" : hoja.Cells[$"N{filas}"].Value.ToString();
                                                solicitudCotizacion.primaUnicaAFPEESS = hoja.Cells[$"O{filas}"].Text == "" ? "" : hoja.Cells[$"O{filas}"].Value.ToString();
                                                solicitudCotizacion.primeraPensionRT = hoja.Cells[$"P{filas}"].Text == "" ? "" : hoja.Cells[$"P{filas}"].Value.ToString();
                                                solicitudCotizacion.tasaInteresRT = hoja.Cells[$"Q{filas}"].Text == "" ? "" : hoja.Cells[$"Q{filas}"].Value.ToString();
                                                solicitudCotizacion.primeraPensionRVD = hoja.Cells[$"R{filas}"].Text == "" ? "" : hoja.Cells[$"R{filas}"].Value.ToString();
                                                solicitudCotizacion.tasaInteresRVD = hoja.Cells[$"S{filas}"].Text == "" ? "" : hoja.Cells[$"S{filas}"].Value.ToString();
                                                listCotizacion.Add(solicitudCotizacion);
                                            }
                                        }
                                    }

                                    if (listErrorExcel.Count > 0)
                                    {
                                        obeError = new LST_MENSAJE();
                                        obeError.EXITO = -2;
                                        obeError.DATA = listErrorExcel;
                                        listError.Add(obeError);

                                        entityList = listError;
                                    }
                                    else
                                    {
                                        entityList = new ActorialBusiness().RegistrarSolicitud(codigo, listSolicitudRecibida, listProducto, listCotizacion, file.FileName, NombreArchivoAutogenerado);
                                    }
                                }
                                else
                                {
                                    obeError = new LST_MENSAJE();
                                    obeError.EXITO = -3;
                                    obeError.MENSAJE = "El encabezado de las columnas no son las correctas.";
                                    listError.Add(obeError);

                                    entityList = listError;
                                }
                            }
                            else
                            {
                                obeError = new LST_MENSAJE();
                                obeError.EXITO = -1;
                                obeError.MENSAJE = "El Archivo no tiene Hojas con Datos.";
                                listError.Add(obeError);

                                entityList = listError;
                            }
                        }
                    }
                }

                return Json(new
                {
                    entityList = entityList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al cargar el archivo - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                return Json(new { entityList = new LST_MENSAJE { EXITO = -1, MENSAJE = ex.Message } }, JsonRequestBehavior.AllowGet);
            }
        }

        public int validarFormato(ExcelPackage package)
        {
            int result = 1;
            foreach (var hoja in package.Workbook.Worksheets)
            { 
                    var totColumn = (hoja.Cells[$"A{1}"].Value == null ? "" : hoja.Cells[$"A{1}"].Value.ToString().Trim()) + (hoja.Cells[$"B{1}"].Value == null ? "" : hoja.Cells[$"B{1}"].Value.ToString().Trim()) + (hoja.Cells[$"C{1}"].Value == null ? "" : hoja.Cells[$"C{1}"].Value.ToString().Trim()) + (hoja.Cells[$"D{1}"].Value == null ? "" : hoja.Cells[$"D{1}"].Value.ToString().Trim()) +
                                    (hoja.Cells[$"E{1}"].Value == null ? "" : hoja.Cells[$"E{1}"].Value.ToString().Trim()) + (hoja.Cells[$"F{1}"].Value == null ? "" : hoja.Cells[$"F{1}"].Value.ToString().Trim()) + (hoja.Cells[$"G{1}"].Value == null ? "" : hoja.Cells[$"G{1}"].Value.ToString().Trim()) + (hoja.Cells[$"H{1}"].Value == null ? "" : hoja.Cells[$"H{1}"].Value.ToString().Trim()) +
                                    (hoja.Cells[$"I{1}"].Value == null ? "" : hoja.Cells[$"I{1}"].Value.ToString().Trim()) + (hoja.Cells[$"J{1}"].Value == null ? "" : hoja.Cells[$"J{1}"].Value.ToString().Trim()) + (hoja.Cells[$"K{1}"].Value == null ? "" : hoja.Cells[$"K{1}"].Value.ToString().Trim()) + (hoja.Cells[$"L{1}"].Value == null ? "" : hoja.Cells[$"L{1}"].Value.ToString().Trim()) +
                                    (hoja.Cells[$"M{1}"].Value == null ? "" : hoja.Cells[$"M{1}"].Value.ToString().Trim()) + (hoja.Cells[$"N{1}"].Value == null ? "" : hoja.Cells[$"N{1}"].Value.ToString().Trim()) + (hoja.Cells[$"O{1}"].Value == null ? "" : hoja.Cells[$"O{1}"].Value.ToString().Trim()) + (hoja.Cells[$"P{1}"].Value == null ? "" : hoja.Cells[$"P{1}"].Value.ToString().Trim()) +
                                    (hoja.Cells[$"Q{1}"].Value == null ? "" : hoja.Cells[$"Q{1}"].Value.ToString().Trim()) + (hoja.Cells[$"R{1}"].Value == null ? "" : hoja.Cells[$"R{1}"].Value.ToString().Trim()) + (hoja.Cells[$"S{1}"].Value == null ? "" : hoja.Cells[$"S{1}"].Value.ToString().Trim());

                var formatValido = "nroOperacionCUSPPmodalidadanosRTporcentajeRVDperiodoGarantizadomonedaderechoCrecergratificacionsiCotizaNoCotizanroCotizacionprimaUnicaEESSprimeraPensionRVtasaInteresRVprimaUnicaAFPEESSprimeraPensionRTtasaInteresRTprimeraPensionRVDtasaInteresRVD";
                    if (totColumn != formatValido)
                        result = 0;
            }

            return result;
        }

        public JsonResult GetDeleteSolicitud(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            ActorialBusiness listMealer = new ActorialBusiness();
            Boolean result = false;

            try
            {
                result = listMealer.EliminarSolicitud(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownloadPlantilla()
        {
            try
            {
                string path = Server.MapPath("~/FileTmp/");
                byte[] fileContents = null;
                path += "Encabezado_Meler_Output.xlsx";

                if (System.IO.File.Exists(path))
                    fileContents = System.IO.File.ReadAllBytes(path);

                String NombreArchivoAutogenerado = "Download_Plantilla_Meler_Output.xlsx";
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", NombreArchivoAutogenerado);
                //return File(fileContents, "application/vnd.ms-excel", NombreArchivoAutogenerado); //xls
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al descargar el archivo - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                return Json(new { entityList = new LST_MENSAJE { EXITO = -1, MENSAJE = ex.Message } }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetListaNroOpercionAfiliado(PARAMS_GET_MEALER objParametros)
        {
            MealerBusiness listMealer = new MealerBusiness();
            IEnumerable<LST_GET_NROOPERACION_AFILIACION> _entity = null;
            List<LST_GET_NROOPERACION_AFILIACION> lista = new List<LST_GET_NROOPERACION_AFILIACION>();
            try
            {
                _entity = listMealer.GetListaNroOpercionAfiliado(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult GuardarVariableAfiliado(Int64 idLote, Int64[] lstSelectedItems)
        {
            bool result = false;
            IEnumerable<LST_VALIDA_MAX_NROOPERACION> _entity = null;
            try
            {
                List<LST_VALIDA_MAX_NROOPERACION> lista = new List<LST_VALIDA_MAX_NROOPERACION>();
                MealerBusiness oMealerBusiness = new MealerBusiness();
                
                _entity = oMealerBusiness.GetValidaMaxCotizacionNroOperacion(idLote, lstSelectedItems);
                lista = _entity.ToList();

                if (lista.Count <= 0)
                {
                    oMealerBusiness.InsertarVariableAfiliacion(idLote, lstSelectedItems);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Utilities.GuardarLog("Error al procesar las variables:  " + ex.Message.ToString());
            }

            return Json(new
            {
                result = result,
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListaVariableCab(PARAMS_GET_MEALER objParametros)
        {
            MealerBusiness listMealer = new MealerBusiness();
            IEnumerable<LST_GET_VARIABLE_CAB> _entity = null;
            List<LST_GET_VARIABLE_CAB> lista = new List<LST_GET_VARIABLE_CAB>();
            try
            {
                _entity = listMealer.GetListaVariableCab(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListaTablaMaestra(String sDesc)
        {
            TablaMaestraBusiness listMealer = new TablaMaestraBusiness();
            IEnumerable<LST_GET_TABLA_MAESTRA> _entity = null;
            List<LST_GET_TABLA_MAESTRA> lista = new List<LST_GET_TABLA_MAESTRA>();
            try
            {
                _entity = listMealer.GetListaTablaMaestra(sDesc.ToUpper());
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProcesoAnualidad(Decimal cic, Decimal tasaventa, Decimal ajuste, Int32 edad, Int32 sexo, Int32 condsalud)
        {
            AnualidadBusiness listMealer = new AnualidadBusiness();
            IEnumerable<LST_GET_ANUALIDAD> _entity = null;
            List<LST_GET_ANUALIDAD> lista = new List<LST_GET_ANUALIDAD>();
            try
            {
                _entity = listMealer.GetCalculoAnualidad(cic, tasaventa, ajuste, edad, sexo, condsalud);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entity = _entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}