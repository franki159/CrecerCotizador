using System;
using System.Application.Business.Comercial;
using System.Collections.Generic;
using System.Domain.Entities.Comercial;
using System.Domain.Entities.Tools;
using System.Infrastructure.Utilities.Utilities;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Comercial
{
    public class ComercialController : Controller
    {
        // GET: Comercial
        public ActionResult CargaMealer()
        {
            return View();
        }
        
        public JsonResult GetVerificarNombreArchivoMealer(PARAMS_GET_MEALER objParametros)
        {
            MealerBusiness listMealer = new MealerBusiness();
            IEnumerable<LST_GET_MEALER> _entity = null;
            try
            {
                _entity = listMealer.GetVerificarNombreArchivoMealer(objParametros);
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

        public JsonResult GetListaComercialMealer(PARAMS_GET_MEALER objParametros)
        {
            MealerBusiness listMealer = new MealerBusiness();
            IEnumerable<LST_GET_MEALER> _entity = null;
            try
            {
                _entity = listMealer.GetListaComercialMealer(objParametros);
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

        public ActionResult UploadFilesMealer()
        {
            try
            {
                IEnumerable<LST_MENSAJE> entityList = null;
                string path = Server.MapPath("~/FileTmp/");
                //string pathProcesados = ConfigurationManager.AppSettings["PathNoAcseleProcesado"].ToString();
                string mensajeRespuesta = "";
                int valido = 1;
                string nombre_archivo_procesado = string.Empty;
                string NombreArchivoAutogenerado = "";
                //DateTime dFechaCierre;
                HttpFileCollectionBase files = Request.Files;
                string codigo = Request.Form[0];

                for (int i = 0; i < files.Count; i++)
                {
                    //DateTime.TryParse(codigo, out dFechaCierre);
                    HttpPostedFileBase file = files[i];

                    if (file.FileName.EndsWith("xml") || file.FileName.EndsWith("XML"))
                    {
                        NombreArchivoAutogenerado = file.FileName.Split('.')[0] + '_' + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("hhmmss") + '.' + file.FileName.Split('.')[1];
                        path += NombreArchivoAutogenerado;
                        file.SaveAs(path);

                        List<descargaSolicitudesEESS> listaSolicitudes = new List<descargaSolicitudesEESS>();

                        using (StreamReader srFileContent = new StreamReader(path, System.Text.Encoding.Default))
                        {
                            System.Xml.Serialization.XmlSerializer reader2 = new System.Xml.Serialization.XmlSerializer(typeof(descargaSolicitudesEESS));

                            try
                            {
                                var overview = (descargaSolicitudesEESS)reader2.Deserialize(srFileContent);

                                List<solicitudRecibidaEESS> listSolicitud = new List<solicitudRecibidaEESS>();
                                List<afiliado> listafiliado = new List<afiliado>();
                                List<fondo> listafondo = new List<fondo>();
                                List<beneficiario> listabeneficiario = new List<beneficiario>();
                                List<producto> listaProducto = new List<producto>();

                                for (int filas = 0; filas < overview.solicitudRecibidaEESS.Count(); filas++)
                                {
                                    var solicitud = new solicitudRecibidaEESS();
                                    solicitud.NFILA = filas + 1;
                                    solicitud.nroOperacion = overview.solicitudRecibidaEESS[filas].nroOperacion;
                                    solicitud.AFP = overview.solicitudRecibidaEESS[filas].AFP;
                                    solicitud.CUSPP = overview.solicitudRecibidaEESS[filas].CUSPP;
                                    solicitud.tipoBeneficio = overview.solicitudRecibidaEESS[filas].tipoBeneficio;
                                    solicitud.cambioModalidad = overview.solicitudRecibidaEESS[filas].cambioModalidad;
                                    solicitud.pensionPreliminar = overview.solicitudRecibidaEESS[filas].pensionPreliminar;
                                    solicitud.tasaRPyRT = overview.solicitudRecibidaEESS[filas].tasaRPyRT;
                                    solicitud.fechaDevengue = overview.solicitudRecibidaEESS[filas].fechaDevengue;
                                    solicitud.fechaSuscripcionIII = overview.solicitudRecibidaEESS[filas].fechaSuscripcionIII;
                                    solicitud.devengueSolicitud = overview.solicitudRecibidaEESS[filas].devengueSolicitud;
                                    solicitud.fechaEnvio = overview.solicitudRecibidaEESS[filas].fechaEnvio;
                                    solicitud.fechaCierre = overview.solicitudRecibidaEESS[filas].fechaCierre;
                                    solicitud.tipoCambio = overview.solicitudRecibidaEESS[filas].tipoCambio;
                                    solicitud.diaCita = overview.solicitudRecibidaEESS[filas].diaCita;
                                    solicitud.horaCita = overview.solicitudRecibidaEESS[filas].horaCita;
                                    solicitud.lugarCita = overview.solicitudRecibidaEESS[filas].lugarCita;
                                    solicitud.numeroMensualidad = overview.solicitudRecibidaEESS[filas].numeroMensualidad;
                                    solicitud.tipoFondo = overview.solicitudRecibidaEESS[filas].tipoFondo;
                                    listSolicitud.Add(solicitud);

                                    for (int fAfi = 0; fAfi < overview.solicitudRecibidaEESS[filas].afiliado.Count(); fAfi++)
                                    {
                                        var afiliado = new afiliado();
                                        afiliado.NCORRELATIVO = fAfi + 1;
                                        afiliado.NFILA = filas + 1;
                                        afiliado.tipoDoc = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].tipoDoc;
                                        afiliado.nroDoc = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].nroDoc;
                                        afiliado.apellidoPaterno = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].apellidoPaterno;
                                        afiliado.apellidoMaterno = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].apellidoMaterno;
                                        afiliado.primerNombre = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].primerNombre;
                                        afiliado.genero = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].genero;
                                        afiliado.fechaNacimiento = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].fechaNacimiento;
                                        afiliado.estadoSobrevivencia = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].estadoSobrevivencia;
                                        afiliado.segundoNombre = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].segundoNombre;
                                        afiliado.gradoInvalidez = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].gradoInvalidez;
                                        afiliado.condicionInvalidez = overview.solicitudRecibidaEESS[filas].afiliado[fAfi].condicionInvalidez;
                                        listafiliado.Add(afiliado);
                                    }

                                    for (int fFon = 0; fFon < overview.solicitudRecibidaEESS[filas].fondo.Count(); fFon++)
                                    {
                                        var fondo = new fondo();
                                        fondo.NCORRELATIVO = fFon + 1;
                                        fondo.NFILA = filas + 1;
                                        fondo.moneda = overview.solicitudRecibidaEESS[filas].fondo[fFon].moneda;
                                        fondo.capitalPension = overview.solicitudRecibidaEESS[filas].fondo[fFon].capitalPension;
                                        fondo.saldoCic = overview.solicitudRecibidaEESS[filas].fondo[fFon].saldoCic;
                                        fondo.valorCuota = overview.solicitudRecibidaEESS[filas].fondo[fFon].valorCuota;
                                        fondo.saldoCuotas = overview.solicitudRecibidaEESS[filas].fondo[fFon].saldoCuotas;
                                        fondo.tieneCobertura = overview.solicitudRecibidaEESS[filas].fondo[fFon].tieneCobertura;
                                        fondo.tipoCambioCompraAA = overview.solicitudRecibidaEESS[filas].fondo[fFon].tipoCambioCompraAA;
                                        fondo.EESScobertura = overview.solicitudRecibidaEESS[filas].fondo[fFon].EESScobertura;
                                        fondo.aporteAdicional = overview.solicitudRecibidaEESS[filas].fondo[fFon].aporteAdicional;
                                        fondo.bonoActualizado = overview.solicitudRecibidaEESS[filas].fondo[fFon].bonoActualizado;
                                        listafondo.Add(fondo);
                                    }

                                    for (int fBen = 0; fBen < overview.solicitudRecibidaEESS[filas].beneficiario.Count(); fBen++)
                                    {
                                        var beneficiario = new beneficiario();
                                        beneficiario.NCORRELATIVO = fBen + 1;
                                        beneficiario.NFILA = filas + 1;
                                        beneficiario.apellidoPaterno = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].apellidoPaterno;
                                        beneficiario.apellidoMaterno = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].apellidoMaterno;
                                        beneficiario.primerNombre = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].primerNombre;
                                        beneficiario.segundoNombre = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].segundoNombre;
                                        beneficiario.parentesco = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].parentesco;
                                        beneficiario.condicionInvalidez = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].condicionInvalidez;
                                        beneficiario.fechaNacimiento = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].fechaNacimiento;
                                        beneficiario.genero = overview.solicitudRecibidaEESS[filas].beneficiario[fBen].genero;
                                        listabeneficiario.Add(beneficiario);
                                    }

                                    for (int fPro = 0; fPro < overview.solicitudRecibidaEESS[filas].producto.Count(); fPro++)
                                    {
                                        var producto = new producto();
                                        producto.NCORRELATIVO = fPro + 1;
                                        producto.NFILA = filas + 1;
                                        producto.modalidad = overview.solicitudRecibidaEESS[filas].producto[fPro].modalidad;
                                        producto.moneda = overview.solicitudRecibidaEESS[filas].producto[fPro].moneda;
                                        producto.derechoCrecer = overview.solicitudRecibidaEESS[filas].producto[fPro].derechoCrecer;
                                        producto.gratificacion = overview.solicitudRecibidaEESS[filas].producto[fPro].gratificacion;
                                        producto.anosRT = overview.solicitudRecibidaEESS[filas].producto[fPro].anosRT;
                                        producto.porcentajeRVD = overview.solicitudRecibidaEESS[filas].producto[fPro].porcentajeRVD;
                                        producto.periodoGarantizado = overview.solicitudRecibidaEESS[filas].producto[fPro].periodoGarantizado;
                                        listaProducto.Add(producto);
                                    }
                                }

                                entityList = new MealerBusiness().RegistrarSolicitud(listSolicitud, listafiliado, listafondo, listabeneficiario, listaProducto, file.FileName, NombreArchivoAutogenerado); //, dFechaCierre);
                            }
                            catch (Exception ex)
                            {

                                return Json(new { Result = "El archivo seleccionado no corresponde al tipo elegido", ID = 0 }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        System.IO.File.Delete(path);
                    }
                }

                return Json(new
                {
                    entityList = entityList,
                    ID = 1
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al cargar el archivo - Message:" + ex.Message + " StackTrace:" + ex.StackTrace + " InnerException" + ex.InnerException);
                return Json(new { Result = "Error en el Archivo:" + ex.Message, ID = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAnio()
        {
            MealerBusiness listMealer = new MealerBusiness();
            IEnumerable<LST_GET_MEALER_ANIO> _entity = null;
            try
            {
                _entity = listMealer.GetAnio();
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

        public JsonResult GetDeleteSolicitud(PARAMS_GET_MEALER objParametros)
        {
            MealerBusiness listMealer = new MealerBusiness();
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
    }
}
