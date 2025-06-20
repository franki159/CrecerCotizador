using System;
using System.Collections.Generic;
using System.Domain.Entities.Comercial;
using System.Domain.Entities.Tools;
using System.Linq;
using System.Persistence.Data.Comercial;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Business.Comercial
{
    public class MealerBusiness
    {
        MealerData oMealerData;

        public IEnumerable<LST_GET_MEALER> GetVerificarNombreArchivoMealer(PARAMS_GET_MEALER objParametros)
        {
            oMealerData = new MealerData();
            return oMealerData.GetVerificarNombreArchivoMealer(objParametros);
        }

        public IEnumerable<LST_GET_MEALER> GetListaComercialMealer(PARAMS_GET_MEALER objParametros)
        {
            oMealerData = new MealerData();
            return oMealerData.GetListaComercialMealer(objParametros);
        }

        public IEnumerable<LST_MENSAJE> RegistrarSolicitud(List<solicitudRecibidaEESS> Solicitud, List<afiliado> Afiliado, List<fondo> fondo, List<beneficiario> beneficiario, List<producto> Producto, String NombreArchivo, string NombreArchivoAutogenerado) //, DateTime dFechaCierre)
        {
            oMealerData = new MealerData();
            return oMealerData.RegistrarSolicitud(Solicitud, Afiliado, fondo, beneficiario, Producto, NombreArchivo, NombreArchivoAutogenerado); //, dFechaCierre);
        }

        public IEnumerable<LST_REPORTE_MEALER> GetReporteComercialMealer(PARAMS_GET_MEALER objParameters)
        {
            oMealerData = new MealerData();
            return oMealerData.GetReporteComercialMealer(objParameters);
        }
        public IEnumerable<LST_GET_MEALER_ANIO> GetAnio()
        {
            oMealerData = new MealerData();
            return oMealerData.GetAnio();
        }

        public bool EliminarSolicitud(PARAMS_GET_MEALER objParameters)
        {
            oMealerData = new MealerData();
            return oMealerData.EliminarSolicitud(objParameters);
        }

        public IEnumerable<LST_GET_NROOPERACION_AFILIACION> GetListaNroOpercionAfiliado(PARAMS_GET_MEALER objParametros)
        {
            oMealerData = new MealerData();
            return oMealerData.GetListaNroOpercionAfiliado(objParametros);
        }

        public void InsertarVariableAfiliacion(Int64 idLote, Int64[] listAfiliacion)
        {
            oMealerData = new MealerData();
            oMealerData.InsertarVariableAfiliacion(idLote, listAfiliacion);
        }

        public IEnumerable<LST_GET_VARIABLE_CAB> GetListaVariableCab(PARAMS_GET_MEALER objParametros)
        {
            oMealerData = new MealerData();
            return oMealerData.GetListaVariableCab(objParametros);
        }

        public IEnumerable<LST_REPORTE_VARIABLE_DET> GetReporteVariableDet(PARAMS_GET_MEALER objParameters)
        {
            oMealerData = new MealerData();
            return oMealerData.GetReporteVariableDet(objParameters);
        }

        public IEnumerable<LST_REPORTE_VARIABLE_DET_PROD> GetReporteVariableDetProd(PARAMS_GET_MEALER objParameters)
        {
            oMealerData = new MealerData();
            return oMealerData.GetReporteVariableDetProd(objParameters);
        }

        public IEnumerable<LST_VALIDA_MAX_NROOPERACION> GetValidaMaxCotizacionNroOperacion(Int64 idLote, Int64[] listAfiliacion)
        {
            oMealerData = new MealerData();
            return oMealerData.GetValidaMaxCotizacionNroOperacion(idLote, listAfiliacion);
        }
    }
}
