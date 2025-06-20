using ServiceStack.Html;
using System.IO;
using System.Text;

namespace System.Domain.Entities.Common
{
    public class ReaderHtmlMal
    {
        private string pathHtml;
        public ReaderHtmlMal(string pathHtml)
        {
            this.pathHtml = pathHtml;
        }
        public string ReadTemplateHTML()
        {
            using (StreamReader lector = new StreamReader(pathHtml, Encoding.UTF8))
            {
                return Minifiers.Html.Compress(lector.ReadToEnd());
            }
        }
    }
}
