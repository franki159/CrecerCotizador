using System;
using System.Collections.Generic;
using System.Domain.Entities.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Domain.Entities.Comercial
{
    public class descargaSolicitudesEESS
    {
        [XmlElement(ElementName = "solicitudRecibidaEESS")]
        public List<solicitudRecibidaEESS> solicitudRecibidaEESS { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
    }

    public class solicitudRecibidaEESS : EntityBase
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }

        [XmlElement(ElementName = "nroOperacion")]
        public string nroOperacion { get; set; }

        [XmlElement(ElementName = "AFP")]
        public string AFP { get; set; }

        [XmlElement(ElementName = "CUSPP")]
        public string CUSPP { get; set; }

        [XmlElement(ElementName = "tipoBeneficio")]
        public string tipoBeneficio { get; set; }

        [XmlElement(ElementName = "cambioModalidad")]
        public string cambioModalidad { get; set; }

        [XmlElement(ElementName = "pensionPreliminar")]
        public string pensionPreliminar { get; set; }

        [XmlElement(ElementName = "tasaRPyRT")]
        public string tasaRPyRT { get; set; }

        [XmlElement(ElementName = "fechaDevengue")]
        public string fechaDevengue { get; set; }

        [XmlElement(ElementName = "fechaSuscripcionIII")]
        public string fechaSuscripcionIII { get; set; }

        [XmlElement(ElementName = "devengueSolicitud")]
        public string devengueSolicitud { get; set; }

        [XmlElement(ElementName = "fechaEnvio")]
        public string fechaEnvio { get; set; }

        [XmlElement(ElementName = "fechaCierre")]
        public string fechaCierre { get; set; }

        [XmlElement(ElementName = "tipoCambio")]
        public string tipoCambio { get; set; }

        [XmlElement(ElementName = "diaCita")]
        public string diaCita { get; set; }

        [XmlElement(ElementName = "horaCita")]
        public string horaCita { get; set; }

        [XmlElement(ElementName = "lugarCita")]
        public string lugarCita { get; set; }

        [XmlElement(ElementName = "numeroMensualidad")]
        public string numeroMensualidad { get; set; }

        [XmlElement(ElementName = "tipoFondo")]
        public string tipoFondo { get; set; }

        [XmlElement(ElementName = "afiliado")]
        public List<afiliado> afiliado { get; set; }

        [XmlElement(ElementName = "fondo")]
        public List<fondo> fondo { get; set; }

        [XmlElement(ElementName = "beneficiario")]
        public List<beneficiario> beneficiario { get; set; }

        [XmlElement(ElementName = "producto")]
        public List<producto> producto { get; set; }

    }

    public class afiliado : EntityBase
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public Int64 NCORRELATIVO { get; set; }

        [XmlElement(ElementName = "tipoDoc")]
        public string tipoDoc { get; set; }

        [XmlElement(ElementName = "nroDoc")]
        public string nroDoc { get; set; }

        [XmlElement(ElementName = "apellidoPaterno")]
        public string apellidoPaterno { get; set; }

        [XmlElement(ElementName = "apellidoMaterno")]
        public string apellidoMaterno { get; set; }

        [XmlElement(ElementName = "primerNombre")]
        public string primerNombre { get; set; }

        [XmlElement(ElementName = "genero")]
        public string genero { get; set; }

        [XmlElement(ElementName = "fechaNacimiento")]
        public string fechaNacimiento { get; set; }

        [XmlElement(ElementName = "estadoSobrevivencia")]
        public string estadoSobrevivencia { get; set; }

        [XmlElement(ElementName = "segundoNombre")]
        public string segundoNombre { get; set; }

        [XmlElement(ElementName = "gradoInvalidez")]
        public string gradoInvalidez { get; set; }

        [XmlElement(ElementName = "condicionInvalidez")]
        public string condicionInvalidez { get; set; }
    }

    public class fondo : EntityBase
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public Int64 NCORRELATIVO { get; set; }

        [XmlElement(ElementName = "moneda")]
        public string moneda { get; set; }

        [XmlElement(ElementName = "capitalPension")]
        public string capitalPension { get; set; }

        [XmlElement(ElementName = "saldoCic")]
        public string saldoCic { get; set; }

        [XmlElement(ElementName = "valorCuota")]
        public string valorCuota { get; set; }

        [XmlElement(ElementName = "saldoCuotas")]
        public string saldoCuotas { get; set; }

        [XmlElement(ElementName = "tieneCobertura")]
        public string tieneCobertura { get; set; }

        [XmlElement(ElementName = "tipoCambioCompraAA")]
        public string tipoCambioCompraAA { get; set; }

        [XmlElement(ElementName = "EESScobertura")]
        public string EESScobertura { get; set; }

        [XmlElement(ElementName = "aporteAdicional")]
        public string aporteAdicional { get; set; }

        [XmlElement(ElementName = "bonoActualizado")]
        public string bonoActualizado { get; set; }
    }

    public class beneficiario : EntityBase
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public Int64 NCORRELATIVO { get; set; }

        [XmlElement(ElementName = "apellidoPaterno")]
        public string apellidoPaterno { get; set; }

        [XmlElement(ElementName = "apellidoMaterno")]
        public string apellidoMaterno { get; set; }

        [XmlElement(ElementName = "primerNombre")]
        public string primerNombre { get; set; }

        [XmlElement(ElementName = "segundoNombre")]
        public string segundoNombre { get; set; }

        [XmlElement(ElementName = "parentesco")]
        public string parentesco { get; set; }

        [XmlElement(ElementName = "condicionInvalidez")]
        public string condicionInvalidez { get; set; }

        [XmlElement(ElementName = "fechaNacimiento")]
        public string fechaNacimiento { get; set; }

        [XmlElement(ElementName = "genero")]
        public string genero { get; set; }
    }

    public class producto : EntityBase
    {
        public decimal NID { get; set; }
        public Int64 IDLOTE { get; set; }
        public Int64 NFILA { get; set; }
        public Int64 NCORRELATIVO { get; set; }

        [XmlElement(ElementName = "modalidad")]
        public string modalidad { get; set; }

        [XmlElement(ElementName = "moneda")]
        public string moneda { get; set; }

        [XmlElement(ElementName = "derechoCrecer")]
        public string derechoCrecer { get; set; }

        [XmlElement(ElementName = "gratificacion")]
        public string gratificacion { get; set; }

        [XmlElement(ElementName = "anosRT")]
        public string anosRT { get; set; }

        [XmlElement(ElementName = "porcentajeRVD")]
        public string porcentajeRVD { get; set; }

        [XmlElement(ElementName = "periodoGarantizado")]
        public string periodoGarantizado { get; set; }
    }
}
