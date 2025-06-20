using System;
using System.Application.Business.Mantenimiento;
using System.Collections.Generic;
using System.Domain.Entities.Mantenimiento;
using System.Domain.Entities.Tools;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Mantenimiento
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Parametro()
        {
            return View();
        }

        public ActionResult Sepelio()
        {
            return View();
        }

        public ActionResult TasaMercado()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetListaParametro(PARAM_GET_PARAMETRO objParametros)
        {
            ParametroBusiness lista = new ParametroBusiness();
            IEnumerable<LST_GET_PARAMETRO> _entity = null;
            try
            {
                _entity = lista.GetListaParametro(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityList = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MantParametro(PARAM_GET_PARAMETRO objParametros)
        {
            ParametroBusiness oParametroBusiness = new ParametroBusiness();
            IEnumerable<LST_MENSAJE> entityList = null;

            try
            {
                entityList = oParametroBusiness.MantParametro(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityList = entityList
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListaSepelio(PARAM_GET_SEPELIO objParametros)
        {
            SepelioBusiness lista = new SepelioBusiness();
            IEnumerable<LST_GET_SEPELIO> _entity = null;
            try
            {
                _entity = lista.GetListaSepelio(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityList = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MantSepelio(PARAM_GET_SEPELIO objParametros)
        {
            SepelioBusiness oSepelioBusiness = new SepelioBusiness();
            IEnumerable<LST_MENSAJE> entityList = null;
            IEnumerable<LST_GET_VALIDA_SEPELIO> entityListValida = null;

            try
            {
                if (objParametros.P_CRITERIO != 3)
                    objParametros.P_DFECCIERRE = new DateTime(objParametros.P_DFECCIERRE.Value.Year, objParametros.P_DFECCIERRE.Value.Month, 1);

                List<PARAM_GET_SEPELIO> lista = new List<PARAM_GET_SEPELIO>();

                if (objParametros.P_CRITERIO == 1)
                {
                    PARAM_GET_SEPELIO obj = new PARAM_GET_SEPELIO();
                    if (objParametros.P_TIPO == 2)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            obj = new PARAM_GET_SEPELIO();
                            obj.P_DFECCIERRE = objParametros.P_DFECCIERRE;
                            obj.P_NMONTO = objParametros.P_NMONTO;
                            lista.Add(obj);

                            objParametros.P_DFECCIERRE = ((DateTime)objParametros.P_DFECCIERRE).AddMonths(1);
                        }
                    }
                    else
                    {
                        obj = new PARAM_GET_SEPELIO();
                        obj.P_DFECCIERRE = objParametros.P_DFECCIERRE;
                        obj.P_NMONTO = objParametros.P_NMONTO;
                        lista.Add(obj);
                    }

                    List<LST_GET_VALIDA_SEPELIO> ListaValida = new List<LST_GET_VALIDA_SEPELIO>();
                    entityListValida = oSepelioBusiness.GetListaValidaSepelio(lista);
                    ListaValida = entityListValida.ToList();

                    if (ListaValida.Count > 0)
                    {
                        return Json(new
                        {
                            entityList = ListaValida,
                            valida = 1
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                entityList = oSepelioBusiness.MantSepelio(objParametros, lista);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityList = entityList,
                valida = 0
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFechaTrimestral(DateTime fecha)
        {
            List<LST_GET_SEPELIO> lista = new List<LST_GET_SEPELIO>();
            LST_GET_SEPELIO obj = new LST_GET_SEPELIO();

            DateTime fechaInicial;
            fechaInicial = new DateTime(fecha.Year, fecha.Month, 1);
            String fullMonthName = String.Empty;

            for (int i = 0; i < 3; i++)
            {
                fullMonthName = fechaInicial.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")); //new DateTime(2015, i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                obj = new LST_GET_SEPELIO();
                obj.FECHACIERRE = fullMonthName.ToUpper();
                lista.Add(obj);

                fechaInicial = fechaInicial.AddMonths(1);
            }

            return Json(new
            {
                entityList = lista
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListaTasaMercado(PARAM_GET_TASA_MERCADO objParametros)
        {
            TasaMercadoBusiness lista = new TasaMercadoBusiness();
            IEnumerable<LST_GET_TASA_MERCADO> _entity = null;
            try
            {
                _entity = lista.GetListaTasaMercado(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityList = _entity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MantTasaMercado(PARAM_GET_TASA_MERCADO objParametros)
        {
            TasaMercadoBusiness oParametroBusiness = new TasaMercadoBusiness();
            IEnumerable<LST_MENSAJE> entityList = null;

            try
            {
                entityList = oParametroBusiness.MantTasaMercado(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityList = entityList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}