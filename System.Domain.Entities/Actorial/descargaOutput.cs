using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Actorial
{
    public class descargaOutput
    {
        public List<solicitudRecibida> solicitudRecibida { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
    }

    public class solicitudRecibida
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public string nroOperacion { get; set; }
        public string CUSPP { get; set; }
    }

    public class SolicitudRecibidaProducto 
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public Int64 NCORRELATIVO { get; set; }
        public string modalidad { get; set; }
        public string moneda { get; set; }
        public string derechoCrecer { get; set; }
        public string gratificacion { get; set; }
        public string anosRT { get; set; }
        public string porcentajeRVD { get; set; }
        public string periodoGarantizado { get; set; }
    }

    public class SolicitudRecibidaCotizacion
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public Int64 NCORRELATIVO { get; set; }
        public string siCotizaNoCotiza { get; set; }
        public string nroCotizacion { get; set; }
        public string primaUnicaEESS { get; set; }
        public string primeraPensionRV { get; set; }
        public string tasaInteresRV { get; set; }
        public string primaUnicaAFPEESS { get; set; }
        public string primeraPensionRT { get; set; }
        public string tasaInteresRT { get; set; }
        public string primeraPensionRVD { get; set; }
        public string tasaInteresRVD { get; set; }
    }
}
