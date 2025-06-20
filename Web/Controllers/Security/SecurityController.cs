using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Application.Business.Security;
using System.Domain.Entities.Security;
using System.Domain.Entities.Security.Parameters;
using System.Configuration;
using System.Domain.Entities.Common.Param;

using Web.Classes.Setting;
using System.Infrastructure.Utilities.Utilities;
using System.Infrastructure.Tools.Services.Models;

namespace Web.Controllers
{
    public class SecurityController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Process()
        {
            /*StringConecctionRequest model = new StringConecctionRequest();
            model.claveSecreto = ConfigurationManager.AppSettings["api_BD_QA_ORA"].ToString();
            Session["DB_CONNECTION"] = (Utilities.GetStringConnectionFromAzure(model));*/
            return View();
        }

        [HttpGet]
        public JsonResult Credentials()
        {
            /*
            //Object's 
            IEnumerable<LST_GET_USER_PROFILE> entityUser = null;
            PARAMS_GET_USER_PROFILE paramsUser = new PARAMS_GET_USER_PROFILE();
            IEnumerable<LST_GET_PARAM> entityParam = null;

            //Entitys Session
            CRE_SESSION objUsuarioSession = new CRE_SESSION();
            CRE_MAIN objHome = new CRE_MAIN();

            //Class setting
            Classes.Setting.Setting clsSetting = new Classes.Setting.Setting();

            //Parameter of questions to database


            //paramsUser.P_SUSER = clsSetting.Username(HttpContext.User.Identity.Name.ToString());
            paramsUser.P_SUSER = "CONSULTOR";
            //paramsUser.P_SUSER = "RCASTILLO";
            //paramsUser.P_SUSER = "DPIZARRO";
            //paramsUser.P_SUSER = "OMARIN";


            //Object send js            
            objHome.MainUrl = ConfigurationManager.AppSettings["LoginTrue"].ToString();
            objHome.Username =  paramsUser.P_SUSER;
            
            //Send user to database for get information login
            SecurityBusiness securityBusiness = new SecurityBusiness();
            entityUser = securityBusiness.GetUser(paramsUser);

            if (entityUser == null)
            {
                objHome.MainUrl = ConfigurationManager.AppSettings["LoginFalse"].ToString();
                objHome.NArea = entityUser.FirstOrDefault() == null ? "" : entityUser.FirstOrDefault().NAREA.ToString();
            }
            else
            {
                foreach (LST_GET_USER_PROFILE item in entityUser)
                {
                    clsSetting.StorageSession(item);
                    break;
                }

               if (entityParam!=null) clsSetting.StorageSession(entityParam);

                objHome.NArea = entityUser.FirstOrDefault() == null ? "" : entityUser.FirstOrDefault().NAREA.ToString();
            }
            */
            LST_GET_USER_PROFILE item = new LST_GET_USER_PROFILE();
            item.NAREA = 2;
            item.NIDPROFILE = 21;
            item.NIDUSER = 301;
            item.SEMAIL = String.Empty;
            item.SLASTNAME = "CONSULTOR";
            item.SLASTNAME2 = "CONSULTOR";
            item.SNAME = "CONSULTOR";
            item.SNAME_PROFILE = "Administrador";
            item.SUSER = "CONSULTOR";

            Classes.Setting.Setting clsSetting = new Classes.Setting.Setting();

            clsSetting.StorageSession(item);

            CRE_MAIN objHome = new CRE_MAIN();

            objHome.NArea = "2";
            objHome.MainUrl = ConfigurationManager.AppSettings["LoginTrue"].ToString();
            objHome.Username = "CONSULTOR";
    
            return Json(new
            {
                objHome = objHome
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CheckFilter]
        public JsonResult MainContent()
        {
            return Json(new
            {
                objMain = HttpContext.Session["MainContent"] == null ? "" : HttpContext.Session["MainContent"].ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CheckFilter]
        public JsonResult MainItemSelect(string MainItem, string idPage)
        {
            HttpContext.Session["MainItem"] = MainItem;
            HttpContext.Session["idPage"] = idPage;
            return Json(new
            {
                objResponse = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CheckFilter]
        public JsonResult MainItemChildSelect(string MainItem, string idPage)
        {
            HttpContext.Session["MainItemChild"] = MainItem;
            HttpContext.Session["idPage"] = idPage;
            return Json(new
            {
                objResponse = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CheckFilter]
        public JsonResult ReadMainItemSelected()
        {
            string mainItem = string.Empty;
            string idPage = string.Empty;

            if (HttpContext.Session["MainItem"] == null)
            {
                mainItem = "";
            }
            else
            {
                mainItem = HttpContext.Session["MainItem"].ToString();
            }

            if (HttpContext.Session["idPage"] == null)
            {
                idPage = "";
            }
            else
            {
                idPage = HttpContext.Session["idPage"].ToString();
            }

            return Json(new
            {
                objMainItem = mainItem
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CheckFilter]
        public JsonResult ReadMainItemChildSelected()
        {
            string mainItem = string.Empty;

            if (HttpContext.Session["MainItemChild"] == null)
            {
                mainItem = "";
            }
            else
            {
                mainItem = HttpContext.Session["MainItemChild"].ToString();
            }

            return Json(new
            {
                objMainChildItem = mainItem
            }, JsonRequestBehavior.AllowGet);
        }

    }
}