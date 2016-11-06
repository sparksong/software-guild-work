    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

namespace HRPortal.Models
{
    [DataContract]
    public class Policy
    {
        [DataMember]
        [Display(Name = "Policy Name")]
        [Required(ErrorMessage = "Policy Name is required.")]
        [RegularExpression(@"^[a-zA-z]+[ a-zA-z]*$", ErrorMessage = "Policy must not contain numbers or special characters.")]
        public string PolicyName { get; set; }

        [DataMember]
        public int PolicyNumber { get; set; }

        [DataMember]
        [Display(Name = "Policy Category")]
        [Required(ErrorMessage = "A policy is required to be a part of a category.")]
        public int PolicyCategory { get; set; }

        [DataMember]
        [Display(Name = "Policy Content")]
        [Required(ErrorMessage = "You must fill in details about the policy.")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "You must enter at least 20 characters and less than 1000.")]
        public string Content { get; set; }

    }
}