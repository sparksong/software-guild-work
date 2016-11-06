using HRPortal.Data.Interfaces;
using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPortal.Repositories
{
    public class MPolicyRepository : IPolicyRepository
    {
        private static List<Policy> _policies;

        static MPolicyRepository()
        {
            // sample data
            _policies = new List<Policy>
                {
                    new Policy { PolicyNumber=1, PolicyName="Employee Information", PolicyCategory = 1, Content = "This is just general information."},
                    new Policy { PolicyNumber=2, PolicyName="Dental Plan", PolicyCategory = 2, Content = "There is no dental plan. Your teeth will rot."},
                    new Policy { PolicyNumber=3, PolicyName="Drug and Alcohol Policy", PolicyCategory = 3, Content = "If you get your work done, I don't care."},
                    new Policy { PolicyNumber=4, PolicyName="Vacation Time", PolicyCategory = 2, Content = "No."}
                };
        }

        public IEnumerable<Policy> GetAllPolicies()
        {
            return _policies;
        }

        public Policy GetPolicy(int policyNumber)
        {
            return _policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
        }

        public Policy AddPolicy(Policy policy)
        {
            policy.PolicyNumber = _policies.Max(p => p.PolicyNumber) + 1;

            _policies.Add(policy);

            return policy;
        }

        public void DeletePolicy(int policyNumber)
        {
            _policies.RemoveAll(p => p.PolicyNumber == policyNumber);
        }
    }
}