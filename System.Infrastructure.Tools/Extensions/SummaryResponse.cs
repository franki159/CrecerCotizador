using System.Collections.Generic;

namespace System.Infrastructure.Tools.Extensions
{
    public class SummaryResponse
    {
        public DataResponse Data { get; set; }
        public string Service { get; set; }
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public StatusResponse Status { get; set; }
    }
    public class DataResponse
    {
        public string PageDetail { get; set; }
        public List<MailResponse> Mails { get; set; }

    }
    public class MailResponse
    {
        public string Id { get; set; }
        public string RuleId { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string UrlEmlOriginal { get; set; }
        public string UrlEmlProced { get; set; }
        public DateTime ReceptionDate { get; set; }
        public DateTime SendDate { get; set; }
        public string Status { get; set; }

    }
    public class StatusResponse
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
