using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Tools.Security
{
    public class Token
    {
        public string keyClient { get; set; }
        public string keySecret { get; set; }
        public string hash { get; set; }
        public long timeStamp { get; set; }
        public long sequence { get; set; }

        private string url;
        private string parameters;

        public Token(string keyClient, string keySecret, string url, string parameters, long timeStamp, long sequence)
        {
            this.keyClient = keyClient;
            this.keySecret = keySecret;
            this.url = url;
            this.parameters = parameters;
            this.timeStamp = timeStamp;
            this.sequence = sequence;
            this.regenerate();
        }

        public Token(string keyClient, string keySecret)
            : this(keyClient, keySecret, null, null, DateTime.Now.Ticks, new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF).Next(0, 9999999))
        {
        }

        public Token(string keyClient)
            : this(keyClient, null)
        {

        }

        private Token(string keyClient, string hash, long timeStamp, long sequence)
        {
            this.keyClient = keyClient;
            this.hash = hash;
            this.timeStamp = timeStamp;
            this.sequence = sequence;
        }

        public static Token parse(string strToken)
        {
            try
            {
                strToken = Uri.UnescapeDataString(strToken);
                string keyClient = strToken.Split(',')[0];
                string hash = strToken.Split(',')[1];
                long timeStamp = Convert.ToInt64(strToken.Split(',')[2]);
                long sequence = Convert.ToInt64(strToken.Split(',')[3]);

                return new Token(keyClient, hash, timeStamp, sequence);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj as Token).hash == this.hash;
        }

        private void regenerate()
        {
            string strPreHash = getStringPreHash();
            if (this.keySecret == null || this.keySecret == string.Empty)
            {
                this.hash = Cryptography.GetMd5Hash(strPreHash);
            }
            else
            {
                this.hash = Cryptography.GetHmacSha1(this.keySecret, strPreHash);
            }
        }

        private string getStringPreHash()
        {
            return string.Format("{0}#{1}#{2}#{3}#{4}",
                        this.keyClient,
                        this.url,
                        this.parameters,
                        this.timeStamp,
                        this.sequence);
        }

        public override string ToString()
        {
            return Uri.EscapeDataString(
                string.Format("{0},{1},{2},{3}",
                        this.keyClient,
                        this.hash,
                        this.timeStamp,
                        this.sequence));
        }
    }
}
