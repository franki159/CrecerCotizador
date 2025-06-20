using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Domain.Entities.Security.Parameters
{
    public class PARAMS_INS_USER
    {
        public Int64 P_NIDUSER { get; set; }
        public string P_SUSER { get; set; }
        public string P_SSTATE { get; set; }
        public Int64 P_NUSERREG { get; set; }
        public string P_SNAME { get; set; }
        public string P_SLASTNAME { get; set; }
        public string P_SLASTNAME2 { get; set; }
        public string P_SSEX { get; set; }
        public string P_SADDRESS { get; set; }
        public string P_SEMAIL { get; set; }
        public string P_SPHONE1 { get; set; }
        public Int64 P_NIDPROFILE { get; set; }
        public Int64 P_NAREA { get; set; }
        public string P_NIDCHARGE { get; set; }
        //public HttpPostedFileBase FilesToUpload1 { get; set; }
    }
}
