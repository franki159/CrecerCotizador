using System;
using System.Collections.Generic;
using System.Domain.Entities.Actorial;
using System.Domain.Entities.Actorial.Parameters;
using System.Domain.Entities.Tools;
using System.Linq;
using System.Persistence.Data.Actorial;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Business.Actorial
{
    public class ActorialBusiness
    {
        ActorialData oActorialData;

        public IEnumerable<LST_GET_OUTPUT_MELER> GetVerificarNombreArchivoOutputMealer(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            oActorialData = new ActorialData();
            return oActorialData.GetVerificarNombreArchivoOutputMealer(objParametros);
        }

        public IEnumerable<LST_GET_OUTPUT_MELER> GetListaActorialOutputMealer(PARAMS_GET_OUTPUT_MELER objParametros)
        {
            oActorialData = new ActorialData();
            return oActorialData.GetListaActorialOutputMealer(objParametros);
        }

        public IEnumerable<LST_MENSAJE> RegistrarSolicitud(String idLote, List<solicitudRecibida> Solicitud, List<SolicitudRecibidaProducto> Producto, List<SolicitudRecibidaCotizacion> Cotizacion, String NombreArchivo, string NombreArchivoAutogenerado)
        {
            oActorialData = new ActorialData();
            return oActorialData.RegistrarSolicitud(idLote, Solicitud, Producto, Cotizacion, NombreArchivo, NombreArchivoAutogenerado);
        }

        public IEnumerable<LST_REPORTE_OUTPUT_MELER> GetReporteOutputMeler(PARAMS_GET_OUTPUT_MELER objParameters)
        {
            oActorialData = new ActorialData();
            return oActorialData.GetReporteOutputMeler(objParameters);
        }

        public bool EliminarSolicitud(PARAMS_GET_OUTPUT_MELER objParameters)
        {
            oActorialData = new ActorialData();
            return oActorialData.EliminarSolicitud(objParameters);
        }
    }
}
