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
    public class SecurityProfileController : Controller
    {
        public ActionResult SecurityProfile()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetProfile(PARAMS_GET_PROFILE objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_PROFILE> entityProfile = null;

            try
            {
                entityProfile = securityBusiness.GetProfile(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityProfile = entityProfile
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdProfileState(PARAMS_UPD_PROFILE_STATE objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            bool result = false;

            try
            {
                securityBusiness.UpdProfileState(objParametros);
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
        public JsonResult GetResourceProfile()
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCE_PROFILE> entityResourceProfile = null;

            try
            {
                entityResourceProfile = securityBusiness.GetResourceProfile();
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityResourceProfile = entityResourceProfile 
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetResourceProfileByID(PARAMS_GET_RESOURCE_PROFILE_ID objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCE_PROFILE_ID> entityResourcesProfiles = null;

            try
            {
                entityResourcesProfiles = securityBusiness.GetResourceProfileByID(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityResourcesProfiles = entityResourcesProfiles
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsProfile(PARAMS_INS_PROFILE objParametros, Int64[] lstResources)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            bool result = false;

            //Get parameter session
            CRE_SESSION objSession = (CRE_SESSION)HttpContext.Session["ObjUsuarioSession"];

            //Set parameter user id for insert profile
            objParametros.P_NUSER = objSession.NIDUSER;

            try
            {
                securityBusiness.InsProfile(objParametros, lstResources); 
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
        public JsonResult DelProfile(PARAMS_DEL_PROFILE objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_DEL_PROFILE> entityDelProfile = null; 

            try
            {
                entityDelProfile = securityBusiness.DelProfile(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityDelProfile = entityDelProfile.First()
            }, JsonRequestBehavior.AllowGet);
        }

    }
}