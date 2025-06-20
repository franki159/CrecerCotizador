using System;
using System.Collections.Generic;
using System.Domain.Entities.Actorial;
using System.Linq;
using System.Persistence.Data.Actorial;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Business.Actorial
{
    public class AnualidadBusiness
    {
        AnualidadData oAnualidadData;

        public IEnumerable<LST_GET_ANUALIDAD> GetCalculoAnualidad(Decimal cic, Decimal tasaventa, Decimal ajuste, Int32 edad, Int32 sexo, Int32 condsalud)
        {
            oAnualidadData = new AnualidadData();
            return oAnualidadData.GetCalculoAnualidad(cic, tasaventa, ajuste, edad, sexo, condsalud);
        }
    }
}
