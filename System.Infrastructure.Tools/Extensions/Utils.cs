using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace System.Infrastructure.Tools.Extensions
{
    public class Utils
    {
        //Variable output
        bool invalid = false;

        public bool SendEmail(String ToEmail, string cc, string bcc, string strAttachment, String Subj, string Message)
        {

            try
            {
                if (string.IsNullOrEmpty(ToEmail))
                    return true;

                if (ToEmail.ToLower().Contains("ñ") || ToEmail.ToLower().Contains("á") || ToEmail.ToLower().Contains("é") ||
                    ToEmail.ToLower().Contains("í") || ToEmail.ToLower().Contains("ó") || ToEmail.ToLower().Contains("ú"))
                    return true;

                if (!IsValidEmail(ToEmail))
                    return true;

                var HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                var FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
                var Password = ConfigurationManager.AppSettings["Password"].ToString();

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailid);
                mailMessage.Subject = Subj;
                mailMessage.Body = "<HTML><body> " + Message + "</body></HTML>";

                mailMessage.IsBodyHtml = true;

                string[] ToMuliId = ToEmail.Split(',');
                foreach (string ToEMailId in ToMuliId)
                {
                    mailMessage.To.Add(new MailAddress(ToEMailId));
                }

                if (cc != null && cc != "")
                {
                    string[] CCId = cc.Split(',');

                    foreach (string CCEmail in CCId)
                    {
                        mailMessage.CC.Add(new MailAddress(CCEmail));
                    }
                }

                if (bcc != null && bcc != "")
                {
                    string[] bccid = bcc.Split(',');

                    foreach (string bccEmailId in bccid)
                    {
                        mailMessage.Bcc.Add(new MailAddress(bccEmailId));
                    }
                }

                if (strAttachment != null && strAttachment != "")
                {

                    foreach (string sAttachment in strAttachment.Split(",".ToCharArray()))
                    {
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(sAttachment);
                        mailMessage.Attachments.Add(attachment);
                    }
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAdd;

                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,24}))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        //NEUVOS PARA ENVIAR DE CORREO
        public static string LeerTemplateHTML(string url)
        {
            using (StreamReader lector = new StreamReader(url, System.Text.Encoding.UTF8))
            {
                return lector.ReadToEnd();
            }
        }

        public void SendMailPersonalized(List<Attachment> listArchivosAdjuntos, List<String> listdestino, List<String> listCopia, List<String> listCopiaOculta, String asunto, String cuerpo)
        {
            try 
            {
                String emailFrom = ConfigurationManager.AppSettings["AlertMailSolicitud"];
                String displayNameFrom = ConfigurationManager.AppSettings["displayNameFrom"];

                SendMailRequest request = new SendMailRequest();

                request.GeneralData = new GeneralData();
                request.GeneralData.FromName = displayNameFrom;
                request.GeneralData.From = emailFrom;

                request.GeneralData.To = new To();

                request.GeneralData.To.Email = new List<string>();
                if (listdestino != null)
                {
                    foreach (var item in listdestino)
                    {
                        request.GeneralData.To.Email.Add(item);
                    }
                }

                request.GeneralData.Cc = new Cc();
                request.GeneralData.Cc.Email = new List<string>();
                if (listCopia != null)
                {
                    foreach (var item in listCopia)
                    {
                        request.GeneralData.Cc.Email.Add(item);
                    }
                }

                request.GeneralData.Bcc = new Bcc();
                if (listCopiaOculta != null)
                {
                    foreach (var item in listCopiaOculta)
                    {
                        request.GeneralData.Bcc.Email.Add(item);
                    }
                }

                request.GeneralData.Message = new Message();
                request.GeneralData.Message.Subject = string.Format("Crecer Seguros - ¡{0}!", asunto);
                request.GeneralData.Message.Classification = "C";
                request.GeneralData.Message.Body = new Body();
                request.GeneralData.Message.Body.Format = "html";
                request.GeneralData.Message.Body.Value = cuerpo;

                request.GeneralData.Message.Attachment = new List<Attachment>();
                if (listArchivosAdjuntos != null)
                {
                    request.GeneralData.Message.Attachment.AddRange(listArchivosAdjuntos);
                }

                request.GeneralData.Options = new Options();
                request.GeneralData.Options.OpenTracking = true;
                request.GeneralData.Options.ClickTracking = true;
                request.GeneralData.Options.TextHtmlTracking = true;
                request.GeneralData.Options.AutoTextBody = false;

                var resData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                GuardarLogTXT(string.Format("resData => {0}", resData));
                //SendMailData mailservice = new SendMailData();
                var resultMail = this.CRECERCOMPE(request);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultEntity<SendMailResponse> CRECERCOMPE(SendMailRequest prequest)
        {
            var sTimeOut = ConfigurationManager.AppSettings["SendMailTimeOut"].ToString();
            var sUrlOperacion = ConfigurationManager.AppSettings["SendMailUrl"].ToString();
            ResultEntity<SendMailResponse> oResult = new ResultEntity<SendMailResponse>();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string srequestsinfiles = string.Empty;
            GuardarLogTXT(string.Format("sUrlOperacion => {0}", sUrlOperacion));
            GuardarLogTXT(string.Format("sTimeOut => {0}", sTimeOut));
            try
            {
                HttpWebRequest request; HttpWebResponse response;
                request = WebRequest.Create(sUrlOperacion) as HttpWebRequest;
                var sParamJson = JsonConvert.SerializeObject(prequest);

                var oreqsinfile = prequest;
                oreqsinfile.GeneralData.Message.Body.Value = "CuerpoHtmlDelCorreo";
                foreach (var item in oreqsinfile.GeneralData.Message.Attachment)
                {
                    item.Value = "CodBase64String";
                }
                srequestsinfiles = JsonConvert.SerializeObject(oreqsinfile);
                byte[] data = UTF8Encoding.UTF8.GetBytes(sParamJson);
                request.Timeout = Convert.ToInt32(sTimeOut) * 1000;
                request.Method = "POST";
                request.ContentLength = data.Length;
                request.ContentType = "application/json; charset=utf-8";
                string suser = ConfigurationManager.AppSettings["SendMailUserName"].ToString();
                string spwd = ConfigurationManager.AppSettings["SendMailPassword"].ToString();
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(suser + ":" + spwd));
                request.Headers.Add("Authorization", "Basic " + encoded);
                request.Headers.Add("Username", suser);
                request.Headers.Add("Password", spwd);

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();
                }
                try
                {
                    GuardarLogTXT(string.Format("body => {0}", sParamJson));
                    response = request.GetResponse() as HttpWebResponse;
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        var res = JsonConvert.DeserializeObject<SendMailResponse>(reader.ReadToEnd());
                        GuardarLogTXT(string.Format("Envío de correo satisfactorio"));
                        oResult.oT = res;
                        oResult.Result = 1;
                        return oResult;
                    }
                }
                catch (WebException e)
                {
                    try
                    {
                        var responseCatch = e.Response as HttpWebResponse;
                        StreamReader streamReader = new StreamReader(responseCatch.GetResponseStream(), Encoding.UTF7);
                        var d = streamReader.ReadToEnd();
                        GuardarLogTXT(string.Format("d => {0}", d));
                    }
                    catch (WebException ex)
                    {
                        GuardarLogTXT(string.Format("ex => {0}", ex));
                    }
                }
            }
            catch (WebException ex)
            {
                GuardarLogTXT(string.Format("ex => {0}", ex));
            }
            return oResult;
        }

        public SummaryResponse CRECERCOMPESummary(string mailId)
        {
            var sTimeOut = ConfigurationManager.AppSettings["SendMailTimeOut"].ToString();
            var summaryMailUrl = $"{ConfigurationManager.AppSettings["SummaryMailUrl"].ToString()}{mailId}";
            SummaryResponse summaryResponse = new SummaryResponse();
            try
            {
                HttpWebRequest request; HttpWebResponse response;
                request = WebRequest.Create(summaryMailUrl) as HttpWebRequest;
                request.Timeout = Convert.ToInt32(sTimeOut) * 1000;
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                string suser = ConfigurationManager.AppSettings["SendMailUserName"].ToString();
                string spwd = ConfigurationManager.AppSettings["SendMailPassword"].ToString();
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(suser + ":" + spwd));
                request.Headers.Add("Authorization", "Basic " + encoded);
                request.Headers.Add("Username", suser);
                request.Headers.Add("Password", spwd);
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        var res = JsonConvert.DeserializeObject<SummaryResponse>(reader.ReadToEnd());
                        summaryResponse = res;

                        return summaryResponse;
                    }
                }
                catch (WebException e)
                {
                    try
                    {
                        GuardarLogTXT("Error Aquiiiiiiiii : " + e.Message);
                        var responseCatch = e.Response as HttpWebResponse;
                        if (responseCatch != null)
                        {
                            StreamReader streamReader = new StreamReader(responseCatch.GetResponseStream(), Encoding.UTF7);
                            var d = streamReader.ReadToEnd();
                            var res = JsonConvert.DeserializeObject<SendMailResponse>(streamReader.ReadToEnd());
                            GuardarLogTXT("Datos Servicios: logError : " + d + " header: " + "Authorization: Basic " + encoded + " ruta: " + summaryMailUrl);
                        }
                    }
                    catch (WebException)
                    {
                        GuardarLogTXT("No existe conexion con el servicio " + summaryMailUrl);
                    }
                }
            }
            catch (WebException)
            {
                GuardarLogTXT("No existe conexion con el servicio " + summaryMailUrl + "Generacion de Token ");
            }
            return summaryResponse;
        }

        //Save Log
        public static void GuardarLogTXT(string mensaje)
        {
            var RutaLog = ConfigurationManager.AppSettings["LogLocal"].ToString();
            //Save in file log
            FileStream stream = null;
            StreamWriter writer = null;

            string dateString = DateTime.Now.ToShortDateString();
            dateString = dateString.Replace("/", "");

            string fileLog = "LogSispoc-" + dateString.ToString() + ".txt";
            string rutaLogApp = RutaLog + "\\" + fileLog;

            if (!File.Exists(rutaLogApp))
            {
                //Crear el archivo txt de log
                stream = new FileStream(rutaLogApp, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(stream);
                writer.Write(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + mensaje.ToString());
                writer.WriteLine();
                writer.Close();
            }
            else
            {
                using (StreamWriter newTask = new StreamWriter(rutaLogApp, true))
                {
                    newTask.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + mensaje.ToString());
                }
            }

        }
    }
}
