using DVDLibrary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLibrary.Models;

namespace DVDLibrary.DAL.Repositories
{
    public class MemoryDVDRepository : IDVDRepository
    {
        private static List<DVD> _dvdList;

        static MemoryDVDRepository()
        {
            _dvdList = new List<DVD>();

            DVD lorax = new DVD() { DvdId = 1, Title = "The Lorax", ReleaseYear = 2012, DirectorName = "Chris Renaud, Kyle Balda", Studio = "Illumination Entertainment", UserNotes = "Twelve-year-old Ted will do anything to find a real live Truffula Tree in order to impress the girl of his dreams. As he embarks on his journey, Ted discovers the incredible story of the Lorax, a grumpy but charming creature who speaks for the trees.", MPAARating = MPAA.PG, UserRating = 8, CurrentBorrower = null };
            lorax.ActorList.Add("Taylor Swift");
            lorax.ActorList.Add("Danny Devito");
            lorax.ActorList.Add("Zac Efron");
            lorax.BorrowerList.Add(new Borrower()
            {
                Name = "The Onceler",
                BorrowDate = DateTime.Parse("3/22/2016"),
                ReturnDate = DateTime.Parse("3/28/2016")
            });

            DVD blades = new DVD() { DvdId = 2, Title = "Blades", ReleaseYear = 1989, DirectorName = "Thomas R. Rondinella", Studio = "Troma Entertainment", UserNotes = "A peaceful country club becomes the hunting ground for a demonically possessed lawnmower with a taste for human flesh.", MPAARating = MPAA.R, UserRating = 1, CurrentBorrower = null };
            blades.ActorList.Add("Killer Lawnmower");

            DVD deadpool = new DVD() { DvdId = 3, Title = "Deadpool", ReleaseYear = 2016, DirectorName = "Tim Miller", Studio = "Marvel", UserNotes = "Wade Wilson (Ryan Reynolds) is a former Special Forces operative who now works as a mercenary. His world comes crashing down when evil scientist Ajax (Ed Skrein) tortures, disfigures and transforms him into Deadpool.", MPAARating = MPAA.PG13, UserRating = 7, CurrentBorrower = "Steve Jackson" };
            deadpool.ActorList.Add("Ryan Reynolds");
            deadpool.BorrowerList.Add(new Borrower()
            {
                Name = "Steve Jackson",
                BorrowDate = DateTime.Parse("5/10/2016")

            });

            DVD casshern = new DVD() { DvdId = 4, Title = "Casshern", ReleaseYear = 2004, DirectorName = "Kazuaki Kiriya", Studio = "Shochiku", UserNotes = "Genetically modified human mutants threaten the future of the world, and the only hope for mankind rests on the shoulders of Casshern, a warrior who has been reincarnated.", MPAARating = MPAA.R, UserRating = 10, CurrentBorrower = "Steve Jackson" };
            casshern.ActorList.Add("Yusuke Iseya");
            casshern.ActorList.Add("Kumiko Aso");
            casshern.BorrowerList.Add(new Borrower()
            {
                Name = "Steve Jackson",
                BorrowDate = DateTime.Parse("5/10/2016")

            });

            _dvdList.Add(lorax);
            _dvdList.Add(blades);
            _dvdList.Add(deadpool);
            _dvdList.Add(casshern);
        }

        public IEnumerable<DVD> GetAll()
        {
            return _dvdList;
        }

        public DVD Get(int dvdId)
        {
            return _dvdList.FirstOrDefault(d => d.DvdId == dvdId);
        }

        public void Add(DVD newDVD)
        {
            if (_dvdList.Count == 0 || _dvdList == null)
            {
                newDVD.DvdId = 1;
            }
            else
            {
                newDVD.DvdId = _dvdList.Max(d => d.DvdId) + 1;
            }
            
            _dvdList.Add(newDVD);
        }

        public void Manage(DVD dvd)
        {
            var selectedDVD = _dvdList.First(d => d.DvdId == dvd.DvdId);

            selectedDVD.Title = dvd.Title;
            selectedDVD.ReleaseYear = dvd.ReleaseYear;
            selectedDVD.MPAARating = dvd.MPAARating;
            selectedDVD.Studio = dvd.Studio;
            selectedDVD.UserRating = dvd.UserRating;
            selectedDVD.UserNotes = dvd.UserNotes;
            selectedDVD.DirectorName = dvd.DirectorName;

            selectedDVD.ActorList = dvd.ActorList;
            selectedDVD.CurrentBorrower = dvd.CurrentBorrower;
            selectedDVD.BorrowerList = dvd.BorrowerList;

        }

        public void Delete(int dvdId)
        {
            _dvdList.RemoveAll(d => d.DvdId == dvdId);
        }

        public void LendDVD(int dvdId, Borrower borrower)
        {
            DVD lentDvd = Get(dvdId);
            lentDvd.CurrentBorrower = borrower.Name;
            borrower.BorrowDate = DateTime.Today;
            borrower.ReturnDate = null;

            lentDvd.BorrowerList.Add(borrower);
        }

        public void ReturnDVD(int dvdId, Borrower borrower)
        {
            DVD returnedDVD = Get(dvdId);
            
            borrower.ReturnDate = DateTime.Today;

            returnedDVD.CurrentBorrower = null;
        }
    }
}
