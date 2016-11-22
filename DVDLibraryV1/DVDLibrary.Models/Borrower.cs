using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary.Models
{
    [DataContract]
    public class Borrower
    {
        [DataMember]
        [Display(Name = "Borrower Name")]
        [Required(ErrorMessage = "Borrower name is required.")]
        public string Name { get; set; }

        [DataMember]
        [Display(Name = "Date Borrowed")]
        [DataType(DataType.Date)]
        public DateTime? BorrowDate { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [Display(Name = "Date Returned")]
        public DateTime? ReturnDate { get; set; }

    }
}
