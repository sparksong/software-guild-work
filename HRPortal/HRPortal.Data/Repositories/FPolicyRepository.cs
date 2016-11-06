using HRPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Models;
using System.Runtime.Serialization.Json;
using System.IO;

namespace HRPortal.Data.Repositories
{

    public class FPolicyRepository : IPolicyRepository
    {
        public const string policyPath = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\HRPortal\Policies.json";

        private List<Policy> LoadPolicies()
        {
            List<Policy> policies = new List<Policy>();

            try
            {
                using (FileStream stream = new FileStream(policyPath, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer cereal = new DataContractJsonSerializer(typeof(List<Policy>));

                    object read = cereal.ReadObject(stream);

                    policies = read as List<Policy>;
                }

                return policies;
            }
            catch
            {
                return policies;
            }

        }

        private void SavePolicies(List<Policy> policies)
        {
            using (FileStream stream = new FileStream(policyPath, FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer cereal = new DataContractJsonSerializer(typeof(List<Policy>));

                cereal.WriteObject(stream, policies);
            }
        }

        public Policy AddPolicy(Policy policy)
        {
            List<Policy> allOfThePolicies = LoadPolicies();

            if (allOfThePolicies.Count > 0)
            {
                policy.PolicyNumber = allOfThePolicies.Max(p => p.PolicyNumber) + 1;
            }
            else
            {
                policy.PolicyNumber = 1;
            }

            allOfThePolicies.Add(policy);

            SavePolicies(allOfThePolicies);

            return policy;
        }

        public void DeletePolicy(int PolicyNumber)
        {
            List<Policy> allOfThePolicies = LoadPolicies();
            allOfThePolicies.RemoveAll(p => p.PolicyNumber == PolicyNumber);
        }

        public IEnumerable<Policy> GetAllPolicies()
        {
            return LoadPolicies();
        }

        public Policy GetPolicy(int PolicyNumber)
        {
            List<Policy> allOfThePolicies = LoadPolicies();

            Policy policy = allOfThePolicies.FirstOrDefault(p => p.PolicyNumber == PolicyNumber);

            return policy;
        }
    }
}
