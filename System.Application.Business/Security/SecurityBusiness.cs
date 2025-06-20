using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Domain.Entities.Security;
using System.Domain.Entities.Security.Parameters;
using System.Persistence.Data.Security;
using System.Infrastructure.Tools.Security;

namespace System.Application.Business.Security
{
    public class SecurityBusiness
    {      

        SecurityData SecurityRequest;

        public bool TokenValidate(string Token, DateTime Time)
        {
            if (Token == null || Token == string.Empty)
                return false;

            string[] arrToken = Token.Split('#');
            if (arrToken.Count() != 3)
            {
                return false;
            }
            DateTime datetime = DateTime.Parse(arrToken[2]);
            datetime = datetime.AddMinutes(3);

            if (datetime < Time)
            {
                return false;

            }
            return true;
        }

        public IEnumerable<LST_GET_RESOURCE> GetResource(PARAMS_GET_RESOURCE objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResource(objParametros);
        }

        public IEnumerable<LST_GET_USER_PROFILE> GetUser(PARAMS_GET_USER_PROFILE objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetUser(objParametros); 
        }

        public IEnumerable<LST_GET_PROFILE> GetProfile(PARAMS_GET_PROFILE objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetProfile(objParametros);
        }

        public void UpdProfileState(PARAMS_UPD_PROFILE_STATE objParametros)
        {
            SecurityRequest = new SecurityData();
            SecurityRequest.UpdProfileState(objParametros);
        }

        public IEnumerable<LST_GET_RESOURCE_PROFILE> GetResourceProfile()
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResourceProfile();
        }

        public IEnumerable<LST_GET_RESOURCE_PROFILE_ID> GetResourceProfileByID(PARAMS_GET_RESOURCE_PROFILE_ID objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResourceProfileByID(objParametros);
        }

        public void InsProfile(PARAMS_INS_PROFILE objParametros, Int64[] lstResources)
        {
            SecurityRequest = new SecurityData();
            SecurityRequest.InsProfile(objParametros,lstResources);
        }

        public IEnumerable<LST_DEL_PROFILE> DelProfile(PARAMS_DEL_PROFILE objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.DelProfile(objParametros);
        }

        public IEnumerable<LST_GET_USER> GetUserGrid(PARAMS_GET_USER objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetUserGrid(objParametros);
        }

        public void UpdUserState(PARAMS_UPD_USER_STATE objParametros)
        {
            SecurityRequest = new SecurityData();
            SecurityRequest.UpdUserState(objParametros);
        }

        public IEnumerable<LST_GET_PROFILE_USER_PNA> GetProfileUserNotAssigned(PARAMS_GET_PROFILE_USER_PNA objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetProfileUserNotAssigned(objParametros);
        }

        public IEnumerable<LST_GET_PROFILE_USER_PSA> GetProfileUserAssigned(PARAMS_GET_PROFILE_USER_PSA objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetProfileUserAssigned(objParametros);
        }

        public IEnumerable<LST_GET_USER_AD> GetUserAD(PARAMS_GET_USER_AD objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetUserAD(objParametros);
        }

        public IEnumerable<LST_GET_USER_AD_DATA> GetUserADData(PARAMS_GET_USER_AD_DATA objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetUserADData(objParametros);
        }

        public IEnumerable<LST_GET_RESOURCES> GetResourcesGrid(PARAMS_GET_RESOURCES objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResourcesGrid(objParametros);
        }

        public void UpdResourcesState(PARAMS_UPD_RESOURCES_STATE objParametros)
        {
            SecurityRequest = new SecurityData();
            SecurityRequest.UpdResourcesState(objParametros);
        }

        public void InsUser(PARAMS_INS_USER objParametros)
        {
            SecurityRequest = new SecurityData();
            SecurityRequest.InsUser(objParametros);
        }

        public IEnumerable<LST_GET_USER_EDIT> GetUserEdit(PARAMS_GET_USER_EDIT objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetUserEdit(objParametros);
        }

        public LST_GET_USER_EDIT_FOTO GetUserEditFoto(PARAMS_GET_USER_EDIT objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetUserEditFoto(objParametros);
        }

        public IEnumerable<LST_DEL_USER> DelUser(PARAMS_DEL_USER objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.DelUser(objParametros);
        }

        public IEnumerable<LST_GET_RESOURCE_FATHER> GetResourceFather()
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResourceFather();
        }

        public IEnumerable<LST_GET_RESOURCE_IMAGE> GetResourceImage()
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResourceImage();
        }

        public void InsResource(PARAMS_INS_RESOURCES objParametros)
        {
            SecurityRequest = new SecurityData();
            SecurityRequest.InsResource(objParametros);
        }

        public IEnumerable<LST_GET_RESOURCES_EDIT> GetResourceEdit(PARAMS_GET_RESOURCES_EDIT objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.GetResourceEdit(objParametros);
        }

        public IEnumerable<LST_DEL_RESOURCE> DelResource(PARAMS_DEL_RESOURCE objParametros)
        {
            SecurityRequest = new SecurityData();
            return SecurityRequest.DelResource(objParametros);
        }
    }
}
