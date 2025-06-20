using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Classes.Setting;

namespace Web.Classes.Controllers
{
    public class ControllerCustomBase : Controller
    {
        public enum enuReportFileFormat
        {
            PDF,
            EXCEL
        }
        public FileContentResult ReportRender(string reportAbsolutePath, string filename, enuReportFileFormat fileFormat, IEnumerable<ReportDataSource> datasets, IEnumerable<ReportParameter> parameters)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = reportAbsolutePath;

            if (parameters != null)
                localReport.SetParameters(parameters);

            foreach (ReportDataSource datasource in datasets)
            {
                localReport.DataSources.Add(datasource);
            }

            //Renderizado
            string deviceInfo = "<DeviceInfo>" +
             "  <OutputFormat>PDF</OutputFormat>" +
             "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            string mimeType;
            byte[] renderedBytes;
            string encoding;
            string fileNameExtension;
            string formatExtension;

            if (fileFormat == enuReportFileFormat.PDF)
                formatExtension = "PDF";
            else
                formatExtension = "EXCEL";
            
            renderedBytes = localReport.Render(formatExtension,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            string datePattern = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            string fileNameTotal = string.Format("{0}.{1}.{2}", filename, datePattern, fileNameExtension);

            Response.AddHeader("Content-Disposition",
             "attachment; filename=" + fileNameTotal);
            return new FileContentResult(renderedBytes, mimeType);
        }
    }
}