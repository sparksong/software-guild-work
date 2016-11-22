using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DVDLibrary.Models
{
    [DataContract]
    public class DVD
    {
        [DataMember]
        public int DvdId { get; set; }

        [DataMember]
        [Display(Name = "DVD Title")]
        [Required(ErrorMessage = "DVD title is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You must enter at least 2 characters and less than 50.")]
        public string Title { get; set; }

        [DataMember]
        [Required(ErrorMessage = "You must input the release year.")]
        [DisplayFormat(DataFormatString = "{0:D4}")]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [DataMember]
        [Required(ErrorMessage = "You must input the Director's name.")]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "You must input the studio.")]
        public string Studio { get; set; }

        [DataMember]
        [Display(Name = "User Rating")]
        [Range(0, 10, ErrorMessage = "The rating can not be lower than 0 and higher than 10.")]
        public int UserRating { get; set; }

        [DataMember]
        [Display(Name = "User Notes")]
        public string UserNotes { get; set; }

        [DataMember]
        [Display(Name = "List of Actors")]
        public List<string> ActorList { get; set; }

        [DataMember]
        [Display(Name = "List of Borrowers")]
        public List<Borrower> BorrowerList { get; set; }

        [DataMember]
        [Display(Name = "Current Borrower")]
        public string CurrentBorrower { get; set; }

        [DataMember]
        [Display(Name = "MPAA Rating")]
        public MPAA MPAARating { get; set; }

        public DVD()
        {
            ActorList = new List<string>();

            BorrowerList = new List<Borrower>();

            Borrower Borrower = new Borrower();
        }
    }
}