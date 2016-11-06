using HRPortal.Data.Repositories;
using HRPortal.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.BLL
{
    public class AppManager
    {
        private AppRepository _appRepo = new AppRepository();

        public void Add(Application application)
        {
            _appRepo.Add(application);
        }

    }
}
