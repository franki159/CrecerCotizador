using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Tools.Services.Models
{
    public class VIEW_Respuesta
    {
        /// <summary>
        /// Exito 1: se dio todo bien 0: No dio respuesta correcta 
        /// </summary>
        public int Exito { get; set; }
        /// <summary>
        /// Mensaje que acompaña la realización de la operación
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Data si hay alguna información que acopañe el mensaje
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Ok sin data 
        /// </summary>
        public void Ok()
        {
            Exito = 1;
            Mensaje = "";
            Data = true;
        }
        /// <summary>
        /// Ok con mensaje 
        /// </summary>
        /// <param name="mensaje"></param>
        public void Ok(string mensaje)
        {
            Exito = 1;
            Mensaje = mensaje;
            Data = true;
        }
        /// <summary>
        /// Ok con mensaje e información acompañando el mensaje
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="info"></param>
        public void Ok(string mensaje, object info)
        {
            Exito = 1;
            Mensaje = mensaje;
            Data = info;
        }
        /// <summary>
        /// Error
        /// </summary>
        public void Error()
        {
            Exito = 0;
            Mensaje = "";
            Data = false;
        }
        /// <summary>
        /// Error con un mensaje especifico
        /// </summary>
        /// <param name="mensaje"></param>
        public void Error(string mensaje)
        {
            Exito = 0;
            Mensaje = mensaje;
            Data = false;
        }
        /// <summary>
        /// Error con mensaje especifico y objeto recibido
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="info"></param>
        public void Error(string mensaje, object info)
        {
            Exito = 0;
            Mensaje = mensaje;
            Data = info;
        }
        /// <summary>
        /// Error Recibiendo una excepción
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="info"></param>
        public void Error(string mensaje, Exception info)
        {
            Exito = 0;
            Mensaje = mensaje;
            Data = info.ToString();
        }
        /// <summary>
        /// Retorna toda la respuesta como un string
        /// </summary>
        /// <returns></returns>
        public string ToCadena()
        {
            //return JsonSerializer.Serialize(this);
            return JsonConvert.SerializeObject(this);
        }
    }
}
