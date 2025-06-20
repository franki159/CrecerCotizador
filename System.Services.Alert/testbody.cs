using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Services.Alert
{
    public class testbody
    {
        public string htmlbody { get; set; }
        public string style { get; set; }

        public testbody()
        {
            style = "" +
                "<style>" +
            ".color-red{" +
            "    background - color: red;" +
            "}" +
        "</ style > ";
        }
    }
}
