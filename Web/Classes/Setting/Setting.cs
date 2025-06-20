using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Domain.Entities.Security;
using System.Application.Business.Security;
using System.Domain.Entities.Security.Parameters;
using System.Domain.Entities.Common.Param;

namespace Web.Classes.Setting
{
    public class Setting
    {
        private string CodeHTML;
        private string Path;
        private Int64 ContHTML;
        private Int64 UlHTML;
        public bool Incio = false;
        public void StorageSession(LST_GET_USER_PROFILE objSession)
        {
            CRE_SESSION objUserSession = new CRE_SESSION();
            objUserSession.NIDUSER = objSession.NIDUSER;
            objUserSession.SUSER = objSession.SUSER;
            objUserSession.SNAME = objSession.SNAME;
            objUserSession.SLASTNAME = objSession.SLASTNAME;
            objUserSession.SLASTNAME2 = objSession.SLASTNAME2;
            objUserSession.SEMAIL = objSession.SEMAIL;
            objUserSession.NIDPROFILE = objSession.NIDPROFILE;
            objUserSession.SNAME_PROFILE = objSession.SNAME_PROFILE;

            //Setear variables de session
            HttpContext.Current.Session["Username"] = objSession.SNAME;
            HttpContext.Current.Session["UsernameWindows"] = objSession.SUSER;
            HttpContext.Current.Session["ProfileName"] = objSession.SNAME_PROFILE;
            HttpContext.Current.Session["ObjUsuarioSession"] = objUserSession;

            //Get Main
            GetMain(objUserSession.NIDUSER);
        }

        public void StorageSession(IEnumerable<LST_GET_PARAM> objSession)
        {
            //Dictionary<string,string> lista = new Dictionary<string,string>();
            //lista = (Dictionary<string, string>)(from x in objSession select new {key= x.PSVALOR ,value= x.NVALOR});
            HttpContext.Current.Session["IdsAsi"] = objSession;
            //(from x in objSession select new { PSVALOR = x.PSVALOR, NVALOR = x.NVALOR }).ToList();
        }

        public string Username(string UserWindows)
        {

            string dato = UserWindows.Replace("\\","/"); 
            string NameUser = dato.Split('/')[1];
                //.Substring(ConfigurationManager.AppSettings["DomainSigma"].Length, UserWindows.Length - ConfigurationManager.AppSettings["DomainSigma"].Length);
            return NameUser;
        }

        public string DomainPath()
        {
            string domain = ConfigurationManager.AppSettings["DomainPATH"].ToString();
            return domain;            
        }

        public void GetMain(Int64 NIDUSER)
        {
            //Object for send
            PARAMS_GET_RESOURCE objParametros = new PARAMS_GET_RESOURCE();
            objParametros.P_NIDUSER = NIDUSER;

            SecurityBusiness SecurityRequest = new SecurityBusiness();
            IEnumerable<LST_GET_RESOURCE> entityResource = SecurityRequest.GetResource(objParametros);

            HttpContext.Current.Session["ObjMain"] = entityResource;
            AssembleMain();
        }

        public void AssembleMain()
        {
            IEnumerable<LST_GET_RESOURCE> entityMain = (IEnumerable<LST_GET_RESOURCE>)HttpContext.Current.Session["ObjMain"];
            
            //Generate code html
            string CodeMain = string.Empty;
            string[,] LstResource = null;
            Int64 ContResource = 0;
            Int64 AcumResource = 0;
            Int64 FlagFor = 0;
            LstResource = new string[entityMain.Count(),4];
            AcumResource = entityMain.Count();
            Path = ConfigurationManager.AppSettings["PathLocal"].ToString();

            foreach (LST_GET_RESOURCE MainItem in entityMain)
            {
                FlagFor = 0;
                if (MainItem.NID == 63)
                {
                    FlagFor = 0;
                }
                if (MainItem.NIDPARENT == 0)
                {
                    if (MainItem.SSTAG == null)
                    {

                        var filteredMain = entityMain.Where(x => x.NIDPARENT == MainItem.NID); 

                        if (filteredMain.Count() > 0)
                        {
                            CodeMain = "<li class='li-first open-ul' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NID.ToString() + "'><div><a><img src='" + Path + MainItem.SHTML + "' /><span class='textLi' >" + MainItem.SNAME + "</span></a><div class='img-arrow'><img src='" + Path + "Images/Layout/Main/arrow.png' /></div></li>";
                            //CodeMain = "<li class='li-first open-ul' id='ul-parent-" + MainItem.NIDRESOURCE.ToString() + "'><a href='#'><i class=''></i><span class='textLi' >" + MainItem.SNAME + "</span><span class='img-arrow'><img src='" + Path + "Images/Layout/Main/arrow.png' /></span></a></li>";                            
                        }
                        else
                        {
                            CodeMain = "<li class='li-first open-ul' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NID.ToString() + "'><a><img src='" + Path + MainItem.SHTML + "' /><span class='textLi' >" + MainItem.SNAME + "</span></a></li>";                            
                        }

                    }
                    else
                    {
                        CodeMain = "<li class='li-first open-ul' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NID.ToString() + "'><a href='" + MainItem.SSTAG + "'><img src='" + Path + MainItem.SHTML + "' /><span class='textLi' >" + MainItem.SNAME + "</span></a></li>";
                    }

                    LstResource[ContResource, 0] = MainItem.NID.ToString();
                    LstResource[ContResource, 1] = CodeMain;
                    LstResource[ContResource, 2] = "0";
                    ContResource += 1;
                }
                else
                {
                    for (int i = 0; i < ContResource; i++)
                    {
                        if (LstResource[i,0] == MainItem.NIDPARENT.ToString())
                        {
                            if (!ValidateFatherAndSon(LstResource, LstResource[i, 3]))
                            {
                                if (MainItem.SSTAG == null)
                                {
                                    var filteredMain = entityMain.Where(x => x.NIDPARENT == MainItem.NID);

                                    if (filteredMain.Count() > 0)
                                    {
                                        if (MainItem.SNAME.ToString().Length > 24)
                                        {
                                            CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><div><a><img /><span style='display:block; width:150px;'>" + MainItem.SNAME + "</span></a></div><div class='img-arrow-sec'><img src='" + Path + "Images/Layout/Main/arrow.png' /></div></li>";                                            
                                        }
                                        else
                                        {
                                            CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><div><a><img /><span>" + MainItem.SNAME + "</span></a></div><div class='img-arrow-sec'><img src='" + Path + "Images/Layout/Main/arrow.png' /></div></li>";
                                        }
                                    }
                                    else
                                    {
                                        if (MainItem.SNAME.ToString().Length > 24)
                                        {
                                            CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><a><img /><span style='display:block; width:150px;'>" + MainItem.SNAME + "</span></a></li>";
                                        }
                                        else
                                        {
                                            CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><a><img /><span>" + MainItem.SNAME + "</span></a></li>";
                                        }
                                    }
                                }
                                else
                                {
                                    var filteredMain = entityMain.Where(x => x.NIDPARENT == MainItem.NID);

                                     if (filteredMain.Count() > 0)
                                     {
                                         if (MainItem.SNAME.ToString().Length > 24)
                                         {
                                             CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><div><a href='" + MainItem.SSTAG + "'><img /><span style='display:block; width:150px;'>" + MainItem.SNAME + "</span></a><div class='img-arrow-sec'><img src='" + Path + "Images/Layout/Main/arrow.png' /></div></li>";
                                         }
                                         else
                                         {
                                             CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><div><a href='" + MainItem.SSTAG + "'><img /><span>" + MainItem.SNAME + "</span></a><div class='img-arrow-sec'><img src='" + Path + "Images/Layout/Main/arrow.png' /></div></li>";
                                         }
                                     }
                                     else
                                     {
                                         if (MainItem.SNAME.ToString().Length > 24)
                                         {
                                             CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><a href='" + MainItem.SSTAG + "'><img /><span style='display:block; width:150px;'>" + MainItem.SNAME + "</span></a></li>";
                                         }
                                         else
                                         {
                                             CodeMain = "<li class='li-ref-sec open-ul-ter' alt='" + MainItem.NIDASI.ToString() + "' id='ul-parent-" + MainItem.NIDPARENT.ToString() + "-" + MainItem.NID.ToString() + "-ter'><a href='" + MainItem.SSTAG + "'><img /><span>" + MainItem.SNAME + "</span></a></li>";
                                         }
                                     }
                                    
                                }

                                LstResource[ContResource, 0] = MainItem.NID.ToString();
                                LstResource[ContResource, 1] = CodeMain;
                                LstResource[ContResource, 2] = "1";
                                LstResource[ContResource, 3] = MainItem.NIDPARENT.ToString();
                                ContResource += 1;
                                FlagFor = 1;
                            }
                        }
                    }

                    if (FlagFor == 0)
                    {
                        if (MainItem.SSTAG == null)
                        {
                            if (MainItem.SNAME.ToString().Length > 24)
                            {
                                CodeMain = "<li class='li-ref-ter' alt='" + MainItem.NIDASI.ToString() + "'><a><img /><span style='display:block; width:150px;'>" + MainItem.SNAME + "</span></a></li>";
                            }
                            else
                            {
                                CodeMain = "<li class='li-ref-ter' alt='" + MainItem.NIDASI.ToString() + "'><a><img /><span>" + MainItem.SNAME + "</span></a></li>";
                            }
                        }
                        else
                        {
                            if (MainItem.SNAME.ToString().Length > 24)
                            {
                                CodeMain = "<li class='li-ref-ter' alt='" + MainItem.NIDASI.ToString() + "'><a href='" + MainItem.SSTAG + "'><img /><span style='display:block; width:150px;'>" + MainItem.SNAME + "</span></a></li>";
                            }
                            else
                            {
                                CodeMain = "<li class='li-ref-ter' alt='" + MainItem.NIDASI.ToString() + "'><a href='" + MainItem.SSTAG + "'><img /><span>" + MainItem.SNAME + "</span></a></li>";
                            }
                        }

                        LstResource[ContResource, 0] = MainItem.NID.ToString();
                        LstResource[ContResource, 1] = CodeMain;
                        LstResource[ContResource, 2] = "2";
                        LstResource[ContResource, 3] = MainItem.NIDPARENT.ToString();
                        ContResource += 1;
                    }
                }
            }

            HttpContext.Current.Session["MainContent"] = LogicalAssembleMain(LstResource, AcumResource);

        }

        public bool ValidateFatherAndSon(string[,] LstResource,string NumberContain)
        {
            bool Flag = false;

            for (int i = 0; i < LstResource.Length; i++)
            {
                if (NumberContain != null)
                {
                    if (LstResource[i, 3] == NumberContain)
                    {
                        Flag = true;
                        break;
                    }
                }
            }

            return Flag;
        }

        public string LogicalAssembleMain(string[,] LstResource, Int64 ContNumber)
        {
            CodeHTML = string.Empty;
            ContHTML = 0;
            UlHTML = 0;

            for (int i = 0; i < ContNumber; i++)
            {
                if (LstResource[i, 2] == "0")
                {
                    CodeHTML += LstResource[i, 1];

                    for (int x = 0; x < ContNumber; x++)
                    {
                        if (LstResource[x, 3] == LstResource[i, 0] && LstResource[x, 2] == "1")
                        {
                            if (ContHTML == 0)
                            {
                                CodeHTML += "<ul class='ul-sec-main ul-parent-" + LstResource[i,0] + "'>";
                                CodeHTML += LstResource[x, 1];
                                ContHTML = 1;

                                for (int z = 0; z < ContNumber; z++)
                                {
                                    if (LstResource[z, 3] == LstResource[x, 0] && LstResource[z, 2] == "2")
                                    {
                                        if (UlHTML == 0)
                                        {
                                            CodeHTML += "<ul class='ul-ter-main ul-parent-" + LstResource[x, 0] + "-" + LstResource[z, 3] + "-ter'>";
                                            CodeHTML += LstResource[z, 1];
                                            UlHTML = 1;
                                        }
                                        else
                                        {
                                            CodeHTML += LstResource[z, 1];
                                        }
                                    }
                                }

                                if (UlHTML == 1)
                                {
                                    CodeHTML += "</ul>";
                                }

                            }
                            else
                            {
                                CodeHTML += LstResource[x, 1];

                                for (int z = 0; z < ContNumber; z++)
                                {
                                    if (LstResource[z, 3] == LstResource[x, 0] && LstResource[z, 2] == "2")
                                    {
                                        if (UlHTML == 0)
                                        {
                                            CodeHTML += "<ul class='ul-ter-main ul-parent-" + LstResource[x, 3] + "-" + LstResource[z, 3] + "-ter'>";
                                            CodeHTML += LstResource[z, 1];
                                            UlHTML = 1;
                                        }
                                        else
                                        {
                                            CodeHTML += LstResource[z, 1];
                                        }
                                    }
                                }

                                if (UlHTML == 1) 
                                {
                                    CodeHTML += "</ul>";
                                    UlHTML = 0;
                                }
                            }
                        }
                    }

                    if (ContHTML == 1)
                    {
                        CodeHTML += "</ul>";
                        ContHTML = 0;
                    }
                }
                //BEGIN LL
                //else
                //{
                //    break;
                //}
                //END LL
            }

            return CodeHTML;
        }

        public int GetIdAsi(string key)
        {
            IEnumerable<LST_GET_PARAM> lista = null;
            int PNIDASI=0;
            lista = (IEnumerable<LST_GET_PARAM>)HttpContext.Current.Session["IdsAsi"];
            if (HttpContext.Current.Session["IdsAsi"] != null)
            {

                var query = lista.Where(p => p.PSVALOR == key).FirstOrDefault();
                if (query != null) PNIDASI = Convert.ToInt32(query.NVALOR);
            }
            return PNIDASI;
        }

        
    }
}