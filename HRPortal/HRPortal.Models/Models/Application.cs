using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models.Models
{
    [DataContract]
    public class Application
    {
        [DataMember]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = "Phone Number")]
        [Required]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [DataMember]
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a vaild email address.")]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "Work History")]
        [Required(ErrorMessage = "Please input your work history!")]
        public string WorkHistory { get; set; }
    }
}

