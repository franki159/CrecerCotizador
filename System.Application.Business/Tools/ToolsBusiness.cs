using System.Collections.Generic;
using System.Domain.Entities.Tools;
using System.Domain.Entities.Tools.Parameters;
using System.Persistence.Data.Tools;

namespace System.Application.Business.Tools
{
    public class ToolsBusiness
    {
        ToolsData toolsRequest;

        public IEnumerable<LST_GET_LIST> GetList(PARAMS_GET_LIST objParametros)
        {
            toolsRequest = new ToolsData();
            return toolsRequest.GetList(objParametros);
        }

        public IEnumerable<LST_GET_GROUP_ASI> GetGroupAsi()
        {
            toolsRequest = new ToolsData();
            return toolsRequest.GetGroupAsi();
        }
       
    }
}
