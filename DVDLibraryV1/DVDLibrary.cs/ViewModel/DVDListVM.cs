using DVDLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibrary.ViewModel
{
    public class DVDListVM
    {
        public List<DVD> DVDs { get; set; }

        public DVD dvd { get; set; }
        
        public Borrower borrower { get; set; }
    }
}