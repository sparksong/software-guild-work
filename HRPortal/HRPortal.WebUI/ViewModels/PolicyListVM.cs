using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPortal.WebUI.ViewModels
{
    public class PolicyListVM
    {
        public List<Category> Categories { get; set; }

        public List<Policy> Policies { get; set; }

        public Policy Policy { get; set; }
    }

}