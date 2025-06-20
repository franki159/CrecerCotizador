using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Application.Business.Security;
using System.Domain.Entities.Security;
using System.Domain.Entities.Security.Parameters;
using System.Configuration;
using Web.Classes.Setting;

namespace Web.Controllers.Security
{
      [CheckFilter]
    public class SecurityModuleController : Controller
    {
        // GET: SecurityModule
        public ActionResult SecurityModule()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetResources(PARAMS_GET_RESOURCES objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCES> entityResource = null;

            try
            {
                entityResource = securityBusiness.GetResourcesGrid(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityResource = entityResource
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdResourceState(PARAMS_UPD_RESOURCES_STATE objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            bool result = false;

            try
            {
                securityBusiness.UpdResourcesState(objParametros);
                result = true;
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                result = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetResourcesFather()
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCE_FATHER> entityResource = null;

            try
            {
                entityResource = securityBusiness.GetResourceFather();
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityResource = entityResource
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetResourcesImage()
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCE_IMAGE> entityResource = null;

            try
            {
                entityResource = securityBusiness.GetResourceImage();
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityResource = entityResource
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsResources(PARAMS_INS_RESOURCES objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            bool result = false;

            //Get parameter session
            CRE_SESSION objSession = (CRE_SESSION)HttpContext.Session["ObjUsuarioSession"];

            //Set parameter user id for insert profile
            objParametros.P_NUSERREG = objSession.NIDUSER;

            try
            {
                securityBusiness.InsResource(objParametros);
                result = true;
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                result = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetResourceEdit(PARAMS_GET_RESOURCES_EDIT objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCES_EDIT> entityResource = null;

            try
            {
                entityResource = securityBusiness.GetResourceEdit(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityResource = entityResource.First()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DelResource(PARAMS_DEL_RESOURCE objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_DEL_RESOURCE> entityDelResource = null;

            try
            {
                entityDelResource = securityBusiness.DelResource(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityDelResource = entityDelResource.First()
            }, JsonRequestBehavior.AllowGet);
        }

    }
}