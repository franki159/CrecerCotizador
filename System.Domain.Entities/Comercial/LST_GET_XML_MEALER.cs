using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Comercial
{
    public class LST_GET_XML_MEALER
    {
        public Int32 idMealerCotizadorXml { get; set; }
        public String idGrupo { get; set; }
        public String sGrupo { get; set; }
        public String idSubGrupo { get; set; }
        public String sSubGrupo { get; set; }
        public String StringMealer { get; set; }
        public String StringMealerAfiliado { get; set; }
        public String StringMealerBeneficio { get; set; }
        public String StringMealerFondo { get; set; }
        public String StringMealerProducto { get; set; }
        public Decimal nroOperacion { get; set; }
        public String AFP { get; set; }
        public String CUSPP { get; set; }
        public String tipoBeneficio { get; set; }
        public String cambioModalidad { get; set; }
        public String pensionPreliminar { get; set; }
        public Decimal tasaRPyRT { get; set; }
        public DateTime fechaDevengue { get; set; }
        public DateTime fechaSuscripcionIII { get; set; }
        public DateTime devengueSolicitud { get; set; }
        public DateTime fechaEnvio { get; set; }
        public DateTime fechaCierre { get; set; }
        public Decimal tipoCambio { get; set; }
        public DateTime diaCita { get; set; }
        public String horaCita { get; set; }
        public String lugarCita { get; set; }
        public Int32 numeroMensualidad { get; set; }
        public String tipoFondo { get; set; }
    }

    public class LST_GET_XML_MEALER_AFILIADO
    {
        public Decimal nroOperacion { get; set; }
        public String tipoDoc { get; set; }
        public String nroDoc { get; set; }
        public String apellidoPaterno { get; set; }
        public String apellidoMaterno { get; set; }
        public String primerNombre { get; set; }
        public String segundoNombre { get; set; }
        public String genero { get; set; }
        public String fechaNacimiento { get; set; }
        public String estadoSobrevivencia { get; set; }
        public String condicionInvalidez { get; set; }
        public String gradoInvalidez { get; set; }
    }

    public class LST_GET_XML_MEALER_BENEFICIO
    {
        public Decimal nroOperacion { get; set; }
        public String apellidoMaterno { get; set; }
        public String apellidoPaterno { get; set; }
        public String condicionInvalidez { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public String genero { get; set; }
        public String parentesco { get; set; }
        public String primerNombre { get; set; }
        public String segundoNombre { get; set; }
    }

    public class LST_GET_XML_MEALER_FONDO
    {
        public Decimal nroOperacion { get; set; }
        public Decimal aporteAdicional { get; set; }
        public String bonoActualizado { get; set; }
        public Decimal capitalPension { get; set; }
        public String EESScobertura { get; set; }
        public String moneda { get; set; }
        public Decimal saldoCic { get; set; }
        public Decimal saldoCuotas { get; set; }
        public String tieneCobertura { get; set; }
        public Decimal tipoCambioCompraAA { get; set; }
        public Decimal valorCuota { get; set; }
    }

    public class LST_GET_XML_MEALER_PRODUCTO
    {
        public Decimal nroOperacion { get; set; }
        public Int32 anosRT { get; set; }
        public String derechoCrecer { get; set; }
        public String gratificacion { get; set; }
        public String modalidad { get; set; }
        public String moneda { get; set; }
        public Int32 periodoGarantizado { get; set; }
        public Decimal porcentajeRVD { get; set; }
    }
}
