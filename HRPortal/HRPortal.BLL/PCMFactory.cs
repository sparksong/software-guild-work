using HRPortal.Data.Repositories;
using HRPortal.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.BLL
{
    public static class PCMFactory
    {
        public static PCManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "MockRepo":
                    return new PCManager(new MPolicyRepository(), new MCategoryRepository());
                case "FileRepo":
                    return new PCManager(new FPolicyRepository(), new FCategoryRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }

        }
    }
}
