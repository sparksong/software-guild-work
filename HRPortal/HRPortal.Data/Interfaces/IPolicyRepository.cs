using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Data.Interfaces
{
    public interface IPolicyRepository
    {
        IEnumerable<Policy> GetAllPolicies();

        Policy GetPolicy(int PolicyNumber);

        Policy AddPolicy(Policy policy);
        
        void DeletePolicy(int PolicyNumber);
    }
}
