using HRPortal.BLL;
using HRPortal.Data.Interfaces;
using HRPortal.Models;
using HRPortal.Repositories;
using HRPortal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.WebUI.Controllers
{
    public class PolicyController : Controller
    {
        PCManager _manager = PCMFactory.Create();

        [HttpGet]
        public ActionResult ViewPolicies()
        {
            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();
            return View(vm);
        }

        [HttpPost]
        public ActionResult ViewPolicies(int CategoryId)
        {

            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();
            vm.Policies = _manager.GetAllPolicies().Where(p => p.PolicyCategory == CategoryId).ToList();
            return View(vm);
        }

        [HttpGet]
        public ActionResult ViewPolicy(int id)
        {
            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();

            vm.Policy = _manager.GetPolicy(id);

            return View(vm);
        }

        [HttpGet]
        public ActionResult ManagePolicies()
        {
            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();

            List<Policy> AllPolicies = _manager.GetAllPolicies().ToList();
            foreach (var category in vm.Categories)
            {
                category.IsDeletable = !(AllPolicies.Any(p => p.PolicyCategory == category.CategoryId));
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult ManagePolicies(int CategoryId)
        {
            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();
            List<Policy> AllPolicies = _manager.GetAllPolicies().ToList();
            foreach (var category in vm.Categories)
            {
                category.IsDeletable = !(AllPolicies.Any(p => p.PolicyCategory == category.CategoryId));
            }

            vm.Policies = AllPolicies.Where(p => p.PolicyCategory == CategoryId).ToList();

            return View(vm);
        }

        [HttpGet]
        public ActionResult ManageCategories()
        {
            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();

            List<Policy> AllPolicies = _manager.GetAllPolicies().ToList();
            foreach (var category in vm.Categories)
            {
                category.IsDeletable = !(AllPolicies.Any(p => p.PolicyCategory == category.CategoryId));
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _manager.AddCategory(category);

                return RedirectToAction("ManageCategories");
            }
            else
            {
                return View(category);
            }
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var category = _manager.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(Category category)
        {
            _manager.DeleteCategory(category.CategoryId);
            return RedirectToAction("ManageCategories");
        }

        [HttpGet]
        public ActionResult DeletePolicy(int id)
        {
            var policy = _manager.GetPolicy(id);
            return View(policy);
        }

        [HttpPost]
        public ActionResult DeletePolicy(Policy policy)
        {
            _manager.DeletePolicy(policy.PolicyNumber);
            return RedirectToAction("ManagePolicies");
        }

        [HttpGet]
        public ActionResult AddPolicy()
        {
            PolicyListVM vm = new PolicyListVM();
            vm.Categories = _manager.GetAllCategories().ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddPolicy(PolicyListVM policyvm)
        {
            if (ModelState.IsValid)
            {
                Policy newPolicy = new Policy();
                newPolicy.PolicyName = policyvm.Policy.PolicyName;
                newPolicy.Content = policyvm.Policy.Content;
                newPolicy.PolicyCategory = policyvm.Policy.PolicyCategory;

                _manager.AddPolicy(newPolicy);

                return RedirectToAction("ManagePolicies");
            }
            else
            {
                policyvm.Categories = _manager.GetAllCategories().ToList();
                return View(policyvm);
            }
        }
    }
}