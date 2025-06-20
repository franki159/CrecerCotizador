
using System.Collections.Generic;
using System.Domain.Entities.Security;
using System.Domain.Entities.Security.Parameters;
using System.Persistence.Connection;
using System.Infrastructure.Utilities.Utilities;
using System.Infrastructure.Tools.Extensions;
using Oracle.DataAccess.Client;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.IO;
using Oracle.DataAccess.Types;
using System.Configuration;

namespace System.Persistence.Data.Security
{
    public class SecurityData : DataContextBase
    {
        public IEnumerable<LST_GET_RESOURCE> GetResource(PARAMS_GET_RESOURCE objParametros)
        {
            
           IEnumerable<LST_GET_RESOURCE> entResource = null;
           /* List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCE", parameter))
            {
                try
                {
                    entResource = dr.ReadRows<LST_GET_RESOURCE>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista de recursos: "+ ex.Message);
                    throw;
                }                
            }
            */
            
            List<LST_GET_RESOURCE> lista = new List<LST_GET_RESOURCE>();
            LST_GET_RESOURCE entidad;
            entidad = new LST_GET_RESOURCE();
            entidad.NID = 1;
            entidad.NIDPARENT = 0;
            entidad.SNAME = "Home";
            entidad.SHTML = "Images/Layout/Main/primas_16.png";
            entidad.SSTAG = "../Dashboard/Index";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 2;
            entidad.NIDPARENT = 0;
            entidad.SNAME = "Cotizador";
            entidad.SHTML = "Images/Layout/Main/calculator.png";
            entidad.SSTAG = null;
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 3;
            entidad.NIDPARENT = 2;
            entidad.SNAME = "Meler Input";
            entidad.SHTML = "";
            entidad.SSTAG = "../Comercial/CargaMealer";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 4;
            entidad.NIDPARENT = 2;
            entidad.SNAME = "Meler Output";
            entidad.SHTML = "";
            entidad.SSTAG = "../Actorial/OutputMeler";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 5;
            entidad.NIDPARENT = 2;
            entidad.SNAME = "Calculo Cotizador";
            entidad.SHTML = "";
            entidad.SSTAG = "../Actorial/Calculo";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 6;
            entidad.NIDPARENT = 2;
            entidad.SNAME = "Calculo Pension";
            entidad.SHTML = "";
            entidad.SSTAG = "../Actorial/Anualidad";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 7;
            entidad.NIDPARENT = 0;
            entidad.SNAME = "Mantenimiento";
            entidad.SHTML = "Images/Layout/Main/configuration_16.png";
            entidad.SSTAG = null;
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 8;
            entidad.NIDPARENT = 7;
            entidad.SNAME = "Sepelio";
            entidad.SHTML = "";
            entidad.SSTAG = "../Mantenimiento/Sepelio";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 9;
            entidad.NIDPARENT = 7;
            entidad.SNAME = "Tasa Mercado";
            entidad.SHTML = "";
            entidad.SSTAG = "../Mantenimiento/TasaMercado";
            lista.Add(entidad);

            entidad = new LST_GET_RESOURCE();
            entidad.NID = 10;
            entidad.NIDPARENT = 7;
            entidad.SNAME = "Parametro";
            entidad.SHTML = "";
            entidad.SSTAG = "../Mantenimiento/Parametro";
            lista.Add(entidad);

            entResource = lista;
            return entResource;
        }

        public IEnumerable<LST_GET_USER_PROFILE> GetUser(PARAMS_GET_USER_PROFILE objParametros)
        {
            IEnumerable<LST_GET_USER_PROFILE> entUserProfile = null;
           
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_SUSER", OracleDbType.Varchar2, objParametros.P_SUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_USER_PROFILE", parameter))
            {
                try
                {
                    entUserProfile = dr.ReadRows<LST_GET_USER_PROFILE>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista de usuarios: " + ex.Message);
                    throw new Exception();
                }
                
            }

            //List<LST_GET_USER_PROFILE> lista = new List<LST_GET_USER_PROFILE>();
            //LST_GET_USER_PROFILE entidad = new LST_GET_USER_PROFILE() { NIDPROFILE = 1, NIDUSER = 1, SNAME = "Betzabell", SLASTNAME = "Muñante", SNAME_PROFILE = "Analista de Cauciones", SUSER = "Betzabell" };
            //lista.Add(entidad);
            //entUserProfile = lista;

            return entUserProfile;
        }

        public IEnumerable<LST_GET_PROFILE> GetProfile(PARAMS_GET_PROFILE objParametros)
        {
            IEnumerable<LST_GET_PROFILE> entityProfile = null;
            List<OracleParameter> parameter = new List<OracleParameter>(); 

            parameter.Add(new OracleParameter("P_DESCRIPTION", OracleDbType.Varchar2, objParametros.P_DESCRIPTION, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Decimal, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Decimal, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_PROFILE", parameter))
            {
                try
                {
                    entityProfile = dr.ReadRows<LST_GET_PROFILE>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista de perfiles: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityProfile;
        }

        public void UpdProfileState(PARAMS_UPD_PROFILE_STATE objParametros)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                parameters.Add(new OracleParameter("P_NIDPROFILE", OracleDbType.Int64, objParametros.P_NIDPROFILE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SSTATE", OracleDbType.Int64, objParametros.P_SSTATE, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.UPD_PROFILE_STATE", parameters);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al actualizar el perfil: " + ex.Message);
                throw ex;
            }
        }

        public IEnumerable<LST_GET_RESOURCE_PROFILE> GetResourceProfile()
        {
            IEnumerable<LST_GET_RESOURCE_PROFILE> entResourceProfile = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCE_PROFILE", parameter))
            {
                try
                {
                    entResourceProfile = dr.ReadRows<LST_GET_RESOURCE_PROFILE>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista de opciones del sistema: " + ex.Message);
                    throw new Exception();
                }

            }

            return entResourceProfile;
        }

        public IEnumerable<LST_GET_RESOURCE_PROFILE_ID> GetResourceProfileByID(PARAMS_GET_RESOURCE_PROFILE_ID objParametros)
        {
            IEnumerable<LST_GET_RESOURCE_PROFILE_ID> entityProfileResource = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDPROFILE", OracleDbType.Int64, objParametros.P_NIDPROFILE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCE_PROFILE_ID", parameter))
            {
                try
                {
                    entityProfileResource = dr.ReadRows<LST_GET_RESOURCE_PROFILE_ID>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener el menu por perfil: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityProfileResource;
        }

        public void InsProfile(PARAMS_INS_PROFILE objParametros, Int64[] lstResources)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                parameters.Add(new OracleParameter("P_NIDPROFILE", OracleDbType.Int64, objParametros.P_NIDPROFILE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SNAME", OracleDbType.NVarchar2, objParametros.P_SNAME, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SDESCRIPTION", OracleDbType.NVarchar2, objParametros.P_SDESCRIPTION, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NUSER", OracleDbType.Int64, objParametros.P_NUSER, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SCONSULTA", OracleDbType.Varchar2, objParametros.SCONSULTA, ParameterDirection.Input));
                
                OracleParameter parameterOracleArray = new OracleParameter();
                parameterOracleArray.ParameterName = "P_NIDRESOURCES_LST";
                parameterOracleArray.Direction = ParameterDirection.Input;
                parameterOracleArray.OracleDbType = OracleDbType.Int64;
                parameterOracleArray.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                parameterOracleArray.Value = lstResources;
                parameters.Add(parameterOracleArray);

                this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.INS_PROFILE", parameters);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al registrar un perfil: " + ex.Message);
                throw ex;
            }
        }

        public IEnumerable<LST_DEL_PROFILE> DelProfile(PARAMS_DEL_PROFILE objParametros)
        {
            IEnumerable<LST_DEL_PROFILE> entityDelProfile = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDPROFILE", OracleDbType.Int64, objParametros.P_NIDPROFILE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.DEL_PROFILE", parameter))
            {
                try
                {
                    entityDelProfile = dr.ReadRows<LST_DEL_PROFILE>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al eliminar un perfil: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityDelProfile;
        }

        public IEnumerable<LST_GET_USER> GetUserGrid(PARAMS_GET_USER objParametros)
        {
            IEnumerable<LST_GET_USER> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_DESCRIPTION", OracleDbType.Varchar2, objParametros.P_DESCRIPTION, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Decimal, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Decimal, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_USER", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_USER>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista de usuarios: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityUser;
        }

        public void UpdUserState(PARAMS_UPD_USER_STATE objParametros)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                parameters.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SSTATE", OracleDbType.Int64, objParametros.P_SSTATE, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.UPD_USER_STATE", parameters);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al actualizar el estado del usuario: " + ex.Message);
                throw ex;
            }
        }

        public IEnumerable<LST_GET_PROFILE_USER_PNA> GetProfileUserNotAssigned(PARAMS_GET_PROFILE_USER_PNA objParametros)
        {
            
            IEnumerable<LST_GET_PROFILE_USER_PNA> entityProfile = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_PROFILE_USER_PNA", parameter))
            {
                try
                {
                    entityProfile = dr.ReadRows<LST_GET_PROFILE_USER_PNA>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al actualizar el perfil de usuarios no asignados: " + ex.Message);
                    throw new Exception();
                }
            }

            return entityProfile;
        }

        public IEnumerable<LST_GET_PROFILE_USER_PSA> GetProfileUserAssigned(PARAMS_GET_PROFILE_USER_PSA objParametros) 
        {

            IEnumerable<LST_GET_PROFILE_USER_PSA> entityProfile = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_PROFILE_USER_PSA", parameter))
            {
                try
                {
                    entityProfile = dr.ReadRows<LST_GET_PROFILE_USER_PSA>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener el perfil de usuarios asignados: " + ex.Message);
                    throw new Exception();
                }
            }

            return entityProfile;
        }

        public IEnumerable<LST_GET_USER_AD> GetUserAD(PARAMS_GET_USER_AD objParametros)
        {
            //IEnumerable<LST_GET_USER_AD> entityUser = null;
            List<LST_GET_USER_AD> lista = new List<LST_GET_USER_AD>();
            try
            {
                //string cadena = "Efitec.lan";
                string cadena = ConfigurationManager.AppSettings["DomainSispocAD"];
                string lastname;
                string firstname;
                using (var context = new PrincipalContext(ContextType.Domain, cadena))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        var list = searcher.FindAll();
                        foreach (var result in list)
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                            lastname = (string)de.Properties["sn"].Value;
                            firstname = (string)de.Properties["givenName"].Value;

                            var item = new LST_GET_USER_AD
                            {
                                SLASTNAME = lastname==null?string.Empty: lastname.Split(' ')[0],
                                SLASTNAME2 = lastname == null ? string.Empty :(lastname.Split(' ').Length>1? lastname.Split(' ')[1]: string.Empty),
                                SNAME = firstname==null?string.Empty : firstname,
                                SUSER = (string)de.Properties["samAccountName"].Value,
                                SEMAIL = (string)de.Properties["userPrincipalName"].Value
                            };
                            

                            if (item.SUSER.ToUpper().Contains(objParametros.P_SUSER.ToUpper()) ||
                                item.SLASTNAME.ToUpper().Contains(objParametros.P_SUSER.ToUpper()) ||
                                item.SNAME.ToUpper().Contains(objParametros.P_SUSER.ToUpper()))
                                   lista.Add(item);
                        }
                    }
                }                
                
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al obtener el usuario del Active Directory: " + ex.Message);
                throw ex;
            }
            return lista;
        }

        public IEnumerable<LST_GET_USER_AD_DATA> GetUserADData(PARAMS_GET_USER_AD_DATA objParametros)
        {
            IEnumerable<LST_GET_USER_AD_DATA> entityUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_USER_AD_DATA", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_USER_AD_DATA>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener el usuario del Active Directory: " + ex.Message);
                    throw new Exception();
                }
            }

            return entityUser;
        }

        public IEnumerable<LST_GET_RESOURCES> GetResourcesGrid(PARAMS_GET_RESOURCES objParametros)
        {
            IEnumerable<LST_GET_RESOURCES> entityResource = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_DESCRIPTION", OracleDbType.Varchar2, objParametros.P_DESCRIPTION, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGESIZE", OracleDbType.Decimal, objParametros.P_NPAGESIZE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("P_NPAGENUM", OracleDbType.Decimal, objParametros.P_NPAGENUM, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCES_MODULO", parameter))
            {
                try
                {
                    entityResource = dr.ReadRows<LST_GET_RESOURCES>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener las opciones del sistema: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityResource;
        }

        public void UpdResourcesState(PARAMS_UPD_RESOURCES_STATE objParametros)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                parameters.Add(new OracleParameter("P_NIDRESOURCE", OracleDbType.Int64, objParametros.P_NIDRESOURCE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SSTATE", OracleDbType.Int64, objParametros.P_SSTATE, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.UPD_RESOURCES_STATE", parameters);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al actualizar el estado de las opciones del sistema: " + ex.Message);
                throw ex;
            }
        }

        public void InsUser(PARAMS_INS_USER objParametros)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();


            //FileStream file = objParametros.FilesToUpload1.InputStream;
            //MemoryStream target = new MemoryStream();
            //objParametros.FilesToUpload1.InputStream.CopyTo(target);
            //byte[] data = target.ToArray();

            //BinaryReader b = new BinaryReader(objParametros.FilesToUpload1.InputStream);
            //byte[] data = b.ReadBytes(objParametros.FilesToUpload1.ContentLength);

            try
            {
                parameters.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SUSER", OracleDbType.NVarchar2, objParametros.P_SUSER, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SSTATE", OracleDbType.Char, objParametros.P_SSTATE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NUSERREG", OracleDbType.Int64, objParametros.P_NUSERREG, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SNAME", OracleDbType.NVarchar2, objParametros.P_SNAME, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SLASTNAME", OracleDbType.NVarchar2, objParametros.P_SLASTNAME, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SLASTNAME2", OracleDbType.NVarchar2, objParametros.P_SLASTNAME2, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SSEX", OracleDbType.Char, objParametros.P_SSEX, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SADDRESS", OracleDbType.NVarchar2, objParametros.P_SADDRESS, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SEMAIL", OracleDbType.NVarchar2, objParametros.P_SEMAIL, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SPHONE1", OracleDbType.NVarchar2, objParametros.P_SPHONE1, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NIDPROFILE", OracleDbType.Int64, objParametros.P_NIDPROFILE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NAREA", OracleDbType.Int64, objParametros.P_NAREA, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NIDCHARGE", OracleDbType.NVarchar2, objParametros.P_NIDCHARGE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_FFOTO", OracleDbType.Blob, null, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.INS_USER", parameters);
            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al registrar un usuario: " + ex.Message);
                throw ex;
            }
        }

        public IEnumerable<LST_GET_USER_EDIT> GetUserEdit(PARAMS_GET_USER_EDIT objParametros)
        {
            IEnumerable<LST_GET_USER_EDIT> entityUser = null;
            //List<LST_GET_USER_EDIT> lista = null;
            //LST_GET_USER_EDIT entidad = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_USER_EDIT", parameter))
            {
                try
                {
                    entityUser = dr.ReadRows<LST_GET_USER_EDIT>();
                    /*
                    entidad = new LST_GET_USER_EDIT();
                    dr.Read();
                    entidad.NIDUSER = Convert.ToInt64(dr[0].ToString()); // .GetOracleString(0).Value;
                    entidad.SUSER = dr[1].ToString();
                    entidad.DFECREG= dr[2].ToString();
                    entidad.SNAME = dr[3].ToString();
                    entidad.SLASTNAME = dr[4].ToString();
                    entidad.SLASTNAME2 = dr[5].ToString();
                    entidad.SSEX = dr[6].ToString();
                    entidad.SADDRESS = dr[7].ToString();
                    entidad.SEMAIL = dr[8].ToString();
                    entidad.NIDPROFILE = Convert.ToInt64(dr[9].ToString());
                    entidad.NAREA = Convert.ToInt64(dr[10].ToString());
                    entidad.NIDCHARGE = dr[11].ToString();
                    */
                    
                    //entityUser = dr.ReadRows<LST_GET_USER_EDIT>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al actualizar un usuario: " + ex.Message);
                    throw new Exception();
                }
            }

            return entityUser;
        }


        public LST_GET_USER_EDIT_FOTO GetUserEditFoto(PARAMS_GET_USER_EDIT objParametros)
        {
            //IEnumerable<LST_GET_USER_EDIT> entityUser = null;
            List<LST_GET_USER_EDIT_FOTO> lista = null;
            LST_GET_USER_EDIT_FOTO entidad = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_USER_EDIT_FOTO", parameter))
            {
                try
                {
                    entidad = new LST_GET_USER_EDIT_FOTO();
                    dr.Read();
                    entidad.Ancho = Convert.ToInt32(dr["FFOTO_WIDTH"].ToString());
                    entidad.Alto = Convert.ToInt32(dr["FFOTO_HEIGHT"].ToString());
                    OracleBlob foto = dr.GetOracleBlob(0);
                
                        entidad.Img = new byte[foto.Length];
                        foto.Read(entidad.Img, 0, Convert.ToInt32(foto.Length));
                    
                        entidad.Formato = dr["FFOTO_FORMAT"].ToString();
                        entidad.Mime = dr["FFOTO_MIMETYPE"].ToString();
                        entidad.Compresion = dr["FFOTO_COMPRESS"].ToString();
                        entidad.Tamano = Convert.ToInt32(dr["FFOTO_LENGTH"].ToString());
                   

                    //entityUser = dr.ReadRows<LST_GET_USER_EDIT>();
                }
                catch (Exception ex)
                {
                    Utilities.GuardarLog("Error al actualizar un usuario: " + ex.Message);
                    throw new Exception();
                }
            }

            return entidad;
        }



        public IEnumerable<LST_DEL_USER> DelUser(PARAMS_DEL_USER objParametros)
        {
            IEnumerable<LST_DEL_USER> entityDelUser = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDUSER", OracleDbType.Int64, objParametros.P_NIDUSER, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.DEL_USER", parameter))
            {
                try
                {
                    entityDelUser = dr.ReadRows<LST_DEL_USER>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al eliminar un usuario: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityDelUser;
        }

        public IEnumerable<LST_GET_RESOURCE_FATHER> GetResourceFather()
        {
            IEnumerable<LST_GET_RESOURCE_FATHER> entityResource = null;
            List<OracleParameter> parameter = new List<OracleParameter>();
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCE_FATHER", parameter))
            {
                try
                {
                    entityResource = dr.ReadRows<LST_GET_RESOURCE_FATHER>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista del menu principal: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityResource;
        }

        public IEnumerable<LST_GET_RESOURCE_IMAGE> GetResourceImage()
        {
            IEnumerable<LST_GET_RESOURCE_IMAGE> entityResource = null;
            List<OracleParameter> parameter = new List<OracleParameter>();
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCE_IMAGE", parameter))
            {
                try
                {
                    entityResource = dr.ReadRows<LST_GET_RESOURCE_IMAGE>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener la lista de las imagenes del menu: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityResource;
        }

        public void InsResource(PARAMS_INS_RESOURCES objParametros)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();

            try
            {
                parameters.Add(new OracleParameter("P_NIDRESOURCE", OracleDbType.Int64, objParametros.P_NIDRESOURCE, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NIDPARENT", OracleDbType.Int64, objParametros.P_NIDPARENT, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SNAME", OracleDbType.NVarchar2, objParametros.P_SNAME, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SDESCRIPTION", OracleDbType.NVarchar2, objParametros.P_SDESCRIPTION, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_SHTML", OracleDbType.NVarchar2, objParametros.P_SHTML, ParameterDirection.Input));
                parameters.Add(new OracleParameter("P_NUSERREG", OracleDbType.Int64, objParametros.P_NUSERREG, ParameterDirection.Input));

                this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.INS_RESOURCES", parameters);

            }
            catch (Exception ex)
            {
                Utilities.GuardarLog("Error al registrar las opciones del menu: " + ex.Message);
                throw ex;
            }
        }

        public IEnumerable<LST_GET_RESOURCES_EDIT> GetResourceEdit(PARAMS_GET_RESOURCES_EDIT objParametros)
        {
            IEnumerable<LST_GET_RESOURCES_EDIT> entityResource = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDRESOURCE", OracleDbType.Int64, objParametros.P_NIDRESOURCE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.GET_RESOURCES_EDIT", parameter))
            {
                try
                {
                    entityResource = dr.ReadRows<LST_GET_RESOURCES_EDIT>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al obtener una opcion del menu: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityResource;
        }

        public IEnumerable<LST_DEL_RESOURCE> DelResource(PARAMS_DEL_RESOURCE objParametros)
        {
            IEnumerable<LST_DEL_RESOURCE> entityDelResource = null;
            List<OracleParameter> parameter = new List<OracleParameter>();

            parameter.Add(new OracleParameter("P_NIDRESOURCE", OracleDbType.Int64, objParametros.P_NIDRESOURCE, ParameterDirection.Input));
            parameter.Add(new OracleParameter("C_TABLE", OracleDbType.RefCursor, ParameterDirection.Output));

            using (OracleDataReader dr = (OracleDataReader)this.ExecuteByStoredProcedure("PKG_SISPOC_SECURITY.DEL_RESOURCE", parameter))
            {
                try
                {
                    entityDelResource = dr.ReadRows<LST_DEL_RESOURCE>();
                }
                catch(Exception ex)
                {
                    Utilities.GuardarLog("Error al eliminar  una opcion del menu: " + ex.Message);
                    throw new Exception();
                }

            }

            return entityDelResource;
        }

    }
}
