using DVDLibrary.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary.BLL
{
    public static class LibraryManagerFactory
    {
        public static LibraryManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "MockRepo":
                    return new LibraryManager(new MemoryDVDRepository());
                
                //case "DBRepo":
                //    return new LibraryManager(new DataBaseDVDRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }

        }

    }
}
