using HRPortal.BLL;
using HRPortal.Data.Interfaces;
using HRPortal.Models;
using HRPortal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.BLL
{
    public class PCManager
    {
        public PCManager(IPolicyRepository policyRepo, ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _policyRepo = policyRepo;
        }
        
        private ICategoryRepository _categoryRepo;
        private IPolicyRepository _policyRepo;

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepo.GetAllCategories();
        }

        public Category GetCategory(int categoryId)
        {
            return _categoryRepo.GetCategory(categoryId);
        }

        public Category AddCategory(Category category)
        {
            return _categoryRepo.AddCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepo.DeleteCategory(categoryId);
        }

        public IEnumerable<Policy> GetAllPolicies()
        {
            return _policyRepo.GetAllPolicies();
        }

        public Policy GetPolicy(int policyNumber)
        {
            return _policyRepo.GetPolicy(policyNumber);
        }

        public Policy AddPolicy(Policy policy)
        {
            return _policyRepo.AddPolicy(policy);
        }

        public void DeletePolicy(int policyNumber)
        {
            _policyRepo.DeletePolicy(policyNumber);
        }

    }
}
