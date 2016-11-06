using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HRPortal.Models
{
    [DataContract]
    public class Category
    {
        [DataMember]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required!")]
        [RegularExpression(@"^[a-zA-z]+[ a-zA-z]*$", ErrorMessage = "Category must not contain numbers or special characters.")]
        public string CategoryName { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public bool IsDeletable { get; set; }  
    }
}