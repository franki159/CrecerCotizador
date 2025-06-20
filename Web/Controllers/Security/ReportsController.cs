using Microsoft.Reporting.WebForms;
using System;
using System.Application.Business.Security;
using System.Collections.Generic;
using System.Domain.Entities.Security;
using System.Domain.Entities.Security.Parameters;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Classes.Controllers;
using Web.Classes.Setting;

namespace Web.Controllers.Security
{
      [CheckFilter]
    public class ReportsController : ControllerCustomBase
    {

        [HttpGet]
        public FileContentResult SecurityUser(PARAMS_GET_USER objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_USER> entityUser = securityBusiness.GetUserGrid(objParametros);

            ReportDataSource dsSecurityUser = new ReportDataSource();
            dsSecurityUser.Name = "dsSecurityUser";
            dsSecurityUser.Value = entityUser;

            IEnumerable<ReportDataSource> datasets = new List<ReportDataSource> { dsSecurityUser };

            return this.ReportRender(Server.MapPath("~/Reports/Security/SecurityUser.rdlc"),
                        "SecurityUser",
                        enuReportFileFormat.EXCEL, datasets, null);
        }

        [HttpGet]
        public FileContentResult SecurityProfile(PARAMS_GET_PROFILE objParametros)
        {
            SecurityBusiness securityBusiness = new SecurityBusiness();
            IEnumerable<LST_GET_PROFILE> entityProfile = securityBusiness.GetProfile(objParametros);

            ReportDataSource dsSecurityProfile = new ReportDataSource();
            dsSecurityProfile.Name = "dsSecurityProfile";
            dsSecurityProfile.Value = entityProfile;

            IEnumerable<ReportDataSource> datasets = new List<ReportDataSource> { dsSecurityProfile };

            return this.ReportRender(Server.MapPath("~/Reports/Security/SecurityProfile.rdlc"),
                        "SecurityProfile",
                        enuReportFileFormat.EXCEL, datasets, null);
        }

        [HttpGet]
        public FileContentResult SecurityModule(PARAMS_GET_RESOURCES objParametros)
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

            ReportDataSource dsSecurityUser = new ReportDataSource();
            dsSecurityUser.Name = "dsSecurityModule";
            dsSecurityUser.Value = entityResource;

            IEnumerable<ReportDataSource> datasets = new List<ReportDataSource> { dsSecurityUser };

            return this.ReportRender(Server.MapPath("~/Reports/Security/SecurityModule.rdlc"),
                        "SecurityModule",
                        enuReportFileFormat.EXCEL, datasets, null);
        }
    }
}