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
using System.IO;

namespace Web.Controllers.Security
{
      [CheckFilter]
    public class SecurityUserController : Controller
    {
        // GET: SecurityUser
        public ActionResult SecurityUser()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUser(PARAMS_GET_USER objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_USER> entityUser = null; 

            try
            {
                entityUser = securityBusiness.GetUserGrid(objParametros);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            return Json(new
            {
                entityUser = entityUser
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdUserState(PARAMS_UPD_USER_STATE objParametros) 
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            bool result = false;

            try
            {
                securityBusiness.UpdUserState(objParametros);
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
        public JsonResult GetProfileUserNotAssigned(PARAMS_GET_PROFILE_USER_PNA objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_PROFILE_USER_PNA> entityProfile = null;

            try
            {
                entityProfile = securityBusiness.GetProfileUserNotAssigned(objParametros);
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
        public JsonResult GetProfileUserAssigned(PARAMS_GET_PROFILE_USER_PSA objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_PROFILE_USER_PSA> entityProfile = null;

            try
            {
                entityProfile = securityBusiness.GetProfileUserAssigned(objParametros);
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
        public JsonResult GetUserAD(PARAMS_GET_USER_AD objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_USER_AD> entityUser = null;

            try
            {
                entityUser = securityBusiness.GetUserAD(objParametros);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            return Json(new
            {
                entityUser = entityUser
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserADData(PARAMS_GET_USER_AD_DATA objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_USER_AD_DATA> entityUser = null;

            try
            {
                entityUser = securityBusiness.GetUserADData(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityUser = entityUser.First()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsUser(PARAMS_INS_USER objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            bool result = false;

            //Get parameter session
            CRE_SESSION objSession = (CRE_SESSION)HttpContext.Session["ObjUsuarioSession"];

            //Set parameter user id for insert profile
            objParametros.P_NUSERREG = objSession.NIDUSER;

            try
            {
                securityBusiness.InsUser(objParametros);
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
        public JsonResult GetUserEdit(PARAMS_GET_USER_EDIT objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_USER_EDIT> entityUser = null;

            try
            {
                entityUser = securityBusiness.GetUserEdit(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityUser = entityUser.First()
            }, JsonRequestBehavior.AllowGet);
        }


        public FileResult GetUserFoto(int id)
        {
            PARAMS_GET_USER_EDIT objParametros = new PARAMS_GET_USER_EDIT();
            objParametros.P_NIDUSER = id;
            SecurityBusiness securityBusiness = new SecurityBusiness();            
            
            LST_GET_USER_EDIT_FOTO foto= securityBusiness.GetUserEditFoto(objParametros);
            MemoryStream stream = new MemoryStream(foto.Img);

            return new FileStreamResult(stream, foto.Formato); // "image/png");            
        }


        [HttpGet]
        public JsonResult DelUser(PARAMS_DEL_USER objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_DEL_USER> entityDelUser = null;

            try
            {
                entityDelUser = securityBusiness.DelUser(objParametros);
            }
            catch
            {
                throw new Exception();
            }

            return Json(new
            {
                entityDelUser = entityDelUser.First()
            }, JsonRequestBehavior.AllowGet);
        }

    }
}