using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HRPortal.BLL;
using HRPortal.Models;

namespace HRPortal.BLL.Tests
{
    [TestFixture]
    public class PolicyTests
    {
        [TestFixture]
        public class BLLTests
        {
            [Test]
            public void PCManagerLoadsPolicies()
            {
                PCManager manager = PCMFactory.Create();

                List<Policy> policies = manager.GetAllPolicies().ToList();

                Assert.AreEqual(4, policies.Count);
            }

            [Test]
            [TestCase("TestyTestThatTestsPolicies", 1, "This is test content added to the Policy")]

            public void PCManagerCanAddPolicy(string policyName, int categoryNum, string policyContent)
            {
                PCManager manager = PCMFactory.Create();

                Policy newPolicy = new Policy();
                newPolicy.PolicyName = policyName;
                newPolicy.PolicyCategory = categoryNum;
                newPolicy.Content = policyContent;

                manager.AddPolicy(newPolicy);

                List<Policy> policies = manager.GetAllPolicies().ToList();

                Assert.AreEqual(5, policies.Count);
            }

            [Test]
            [TestCase(2)]
            public void PCManagerCanDeletePolicy(int policyNumber)
            {
                PCManager manager = PCMFactory.Create();

                manager.DeletePolicy(policyNumber);
                List<Policy> policies = manager.GetAllPolicies().ToList();

                Assert.AreEqual(3, policies.Count);
            }

        }
    }
}