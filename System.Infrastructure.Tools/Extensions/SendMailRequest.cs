using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Tools.Extensions
{
    public class To
    {
        public List<string> Email { get; set; }
    }

    public class Cc
    {
        public List<string> Email { get; set; }
    }

    public class Bcc
    {
        public List<string> Email { get; set; }
    }

    public class Body
    {
        public string Format { get; set; }
        public string Value { get; set; }
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string Encode { get; set; }
        public string Size { get; set; }
        public string Value { get; set; }
        public int lengt { get; set; }//para validar el tamañano del archivo
        public int nbloque { get; set; }//si es true es enviado y si es false aun no esta enviando
    }

    public class Message
    {
        public string Subject { get; set; }
        public string Classification { get; set; }
        public Body Body { get; set; }
        public List<Attachment> Attachment { get; set; }
    }

    public class Options
    {
        public bool OpenTracking { get; set; }
        public bool ClickTracking { get; set; }
        public bool TextHtmlTracking { get; set; }
        public bool AutoTextBody { get; set; }
    }

    public class GeneralData
    {
        public string FromName { get; set; }
        public string From { get; set; }
        public To To { get; set; }
        public Cc Cc { get; set; }
        public Bcc Bcc { get; set; }
        public Message Message { get; set; }
        public Options Options { get; set; }
    }

    public class SendMailRequest
    {
        public GeneralData GeneralData { get; set; }
    }

    //mails
    public class ResultEntity<T>
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public int IdResult { get; set; }
        public T oT { get; set; }
    }

    public class SendMailResponse
    {
        public Data Data { get; set; }
        public string Service { get; set; }
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public Status Status { get; set; }
    }
  
    public class Data
    {
        public List<Mail> Mails { get; set; }
    }

    public class Status
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class Mail
    {
        public string Id { get; set; }
    }


    public class Data2
    {
        public List<Mail> Mails { get; set; }
        public Summary2 Summary { get; set; }
    }

    public class Status2
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class SendMailResponse2
    {
        public Data2 Data { get; set; }
        public string Service { get; set; }
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public Status2 Status { get; set; }
    }
    public class Summary2
    {
        public string Sents { get; set; }
        public string Opens { get; set; }
        public string Clicks { get; set; }
        public string Retries { get; set; }
        public string Accepteds { get; set; }
        public string AcceptedsRate { get; set; }
        public string TACs { get; set; }
        public string PCTRs { get; set; }
        public string CTRs { get; set; }
        public string PCTOs { get; set; }
        public string CTOs { get; set; }
    }
}
