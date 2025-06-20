using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Entities.Comercial
{
    public class LST_REPORTE_MEALER
    {
        public Decimal SNROOPERACION { get; set; }
        public String SAFP { get; set; }
        public String SCUSPP { get; set; }
        public String STIPOBENEFICIO { get; set; }
        public String SCAMBIOMODALIDAD { get; set; }
        public String SPENSIONPRELIMINAR { get; set; }
        public Decimal STASARPYRT { get; set; }
        public DateTime? SFECHADEVENGUE { get; set; }
        public DateTime? SFECHASUSCRIPCIONIII { get; set; }
        public DateTime? SDEVENGUESOLICITUD { get; set; }
        public DateTime? SFECHAENVIO { get; set; }
        public DateTime? SFECHACIERRE { get; set; }
        public Decimal STIPOCAMBIO { get; set; }
        public DateTime? SDIACITA { get; set; }
        public String SHORACITA { get; set; }
        public String SLUGARCITA { get; set; }
        public Decimal SNUMEROMENSUALIDAD { get; set; }
        public String STIPOFONDO { get; set; }
        public String STIPODOC { get; set; }
        public String SNRODOC { get; set; }
        public String AFI_SAPELLIDOPATERNO { get; set; }
        public String AFI_SAPELLIDOMATERNO { get; set; }
        public String AFI_SPRIMERNOMBRE { get; set; }
        public String AFI_SGENERO { get; set; }
        public DateTime? AFI_SFECHANACIMIENTO { get; set; }
        public String SESTADOSOBREVIVENCIA { get; set; }
        public String AFI_SSEGUNDONOMBRE { get; set; }
        public String SGRADOINVALIDEZ { get; set; }
        public String AFI_SCONDICIONINVALIDEZ { get; set; }
        public String FON_SMONEDA { get; set; }
        public Decimal SCAPITALPENSION { get; set; }
        public Decimal SSALDOCIC { get; set; }
        public Decimal SVALORCUOTA { get; set; }
        public Decimal SSALDOCUOTAS { get; set; }
        public String STIENECOBERTURA { get; set; }
        public Decimal STIPOCAMBIOCOMPRAAA { get; set; }
        public String SEESSCOBERTURA { get; set; }
        public Decimal SAPORTEADICIONAL { get; set; }
        public Decimal SBONOACTUALIZADO { get; set; }
        public Decimal NCORRELATIVOBEN { get; set; }
        public String BENEFICIO { get; set; }
        public String SAPELLIDOPATERNO { get; set; }
        public String SAPELLIDOMATERNO { get; set; }
        public String SPRIMERNOMBRE { get; set; }
        public String SSEGUNDONOMBRE { get; set; }
        public String SPARENTESCO { get; set; }
        public String SCONDICIONINVALIDEZ { get; set; }
        public DateTime? SFECHANACIMIENTO { get; set; }
        public String SGENERO { get; set; }
        public String PRODUCTO { get; set; }
        public Decimal NCORRELATIVOPROD { get; set; }
        public String SMODALIDAD { get; set; }
        public String SMONEDA { get; set; }
        public String SDERECHOCRECER { get; set; }
        public String SGRATIFICACION { get; set; }
        public Decimal SANOSRT { get; set; }
        public Decimal SPORCENTAJERVD { get; set; }
        public Decimal SPERIODOGARANTIZADO { get; set; }
    }

    public class LST_REPORTE_VARIABLE_DET
    {
        public String NROOPERACION { get; set; }
        public Decimal NAFILIADO { get; set; }
        public Decimal NEDADMESAFILIADO { get; set; }
        public Decimal NEDADANIOAFILIADO { get; set; }
        public String NESTSALUDAFILIADO { get; set; }
        public Decimal NLIMITEMESAFILIADO { get; set; }
        public Decimal NBENEFICIARIO { get; set; }
        public Decimal NEDADMESBENEFICIARIO { get; set; }
        public Decimal NEDADANIOBENEFICIARIO { get; set; }
        public String NESTSALUDBENEFICIARIO { get; set; }
        public Decimal NLIMITMESBENEFICIO { get; set; }
        public String VAFILIADO { get; set; }
        public String VBENEFICIARIO { get; set; }
        public Decimal FILA { get; set; }
        public String CORRELATIVOOPERACION { get; set; }
        public Double NTASAVENTAMENS { get; set; }
        public Double NTASAAFPMENS { get; set; }
        public DateTime DFECHCALCAJUST { get; set; }
        public Decimal NPERDIFMESES { get; set; }
        public DateTime DFECHVITAL { get; set; }
        public Decimal NPERRETRASODEV { get; set; }
        public Decimal NPERGARANTMESES { get; set; }
        public Decimal NPERDIFREST { get; set; }
        public Decimal NPERDIFGARANTREST { get; set; }
        public Decimal NPERRETRASOCS { get; set; }
        public Decimal NPERRETRASOAFP { get; set; }
        public Single NMONTOGS { get; set; }
        public Decimal NRELACRENTATEMP { get; set; }
        public Double NTASACEMENS { get; set; }
        public Double NTASACE { get; set; }
        public Double NTASALRMENS { get; set; }
        public Double NTASALR { get; set; }
        public Double NTASAPROMMERC { get; set; }
        public Double NFACTORAJUSTMENS { get; set; }
        public String VMODALIDAD { get; set; }
        public String VMONEDA { get; set; }
        public Decimal NRT { get; set; }
        public Decimal NPERGAR { get; set; }
        public Decimal FILAPROD { get; set; }
    }

    public class LST_REPORTE_VARIABLE_DET_PROD
    {
        public String NROOPERACION { get; set; }
        public Double NTASAVENTAMENS { get; set; }
        public Double NTASAAFPMENS { get; set; }
        public DateTime DFECHCALCAJUST { get; set; }
        public Decimal NPERDIFMESES { get; set; }
        public DateTime DFECHVITAL { get; set; }
        public Decimal NPERRETRASODEV { get; set; }
        public Decimal NPERGARANTMESES { get; set; }
        public Decimal NPERDIFREST { get; set; }
        public Decimal NPERDIFGARANTREST { get; set; }
        public Decimal NPERRETRASOCS { get; set; }
        public Decimal NPERRETRASOAFP { get; set; }
        public Single NMONTOGS { get; set; }
        public Decimal NRELACRENTATEMP { get; set; }
        public Double NTASACEMENS { get; set; }
        public Double NTASACE { get; set; }
        public Double NTASALRMENS { get; set; }
        public Double NTASALR { get; set; }
        public Double NTASAPROMMERC { get; set; }
        public Double NFACTORAJUSTMENS { get; set; }
        public String VMODALIDAD { get; set; }
        public String VMONEDA { get; set; }
        public Decimal NRT { get; set; }
        public Decimal NPERGAR { get; set; }
        public Decimal FILA { get; set; }
    }
    public class LST_VALIDA_MAX_NROOPERACION
    {
        public String NROOPERACION { get; set; }
        public Decimal CORRELATIVO { get; set; }
    }
}
