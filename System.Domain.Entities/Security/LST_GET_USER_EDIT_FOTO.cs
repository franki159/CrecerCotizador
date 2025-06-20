using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Security
{
   public class LST_GET_USER_EDIT_FOTO
    {
        //Datos de la foto
        public int Ancho { get; set; }
        public int Alto { get; set; }
        public string Formato { get; set; }
        public string Mime { get; set; }
        public string Compresion { get; set; }
        public int Tamano { get; set; }
        public byte[] Img { get; set; }
    }
}
