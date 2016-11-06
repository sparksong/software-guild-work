
using HRPortal.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Data.Repositories
{
    public class AppRepository
    {
        public const string applicationPath = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\HRPortal\Applications.json";

        private List<Application> LoadApplications()
        {
            List<Application> result = new List<Application>();

            try
            {
                using (FileStream stream = new FileStream(applicationPath, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer cereal = new DataContractJsonSerializer(typeof(List<Application>));

                    object read = cereal.ReadObject(stream);

                    result = read as List<Application>;
                }

                return result;
            }
            catch
            {
                return result;
            }

        }

        private void SaveApplications(List<Application> applications)
        {
            using (FileStream stream = new FileStream(applicationPath, FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer cereal = new DataContractJsonSerializer(typeof(List<Application>));

                cereal.WriteObject(stream, applications);
            }
        }

        public void Add(Application application)
        {
            List<Application> applications = LoadApplications();

            applications.Add(application);

            SaveApplications(applications);
        }
    }

}
