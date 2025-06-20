using System.Web;

namespace System.Domain.Entities.Common
{
    public class FileRequest
    {
        public HttpPostedFileBase[] Files { get; set; }
        public string scodigosolicitud { get; set; }
    }
}
