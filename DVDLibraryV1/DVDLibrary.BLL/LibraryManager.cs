using DVDLibrary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLibrary.DAL.Repositories;
using DVDLibrary.Models;

namespace DVDLibrary.BLL
{
    public class LibraryManager
    {
        public LibraryManager(IDVDRepository dvdRepo)
        {
            _dvdRepo = dvdRepo;
        }

        private IDVDRepository _dvdRepo;

        public IEnumerable<DVD> GetAll()
        {
            return _dvdRepo.GetAll();
        }

        public DVD Get(int dvdId)
        {
            return _dvdRepo.Get(dvdId);
        }

        public void AddDVD(DVD newDVD)
        {
            _dvdRepo.Add(newDVD);
        }

        public void Manage(DVD dvd)
        {
            _dvdRepo.Manage(dvd);
        }

        public void Delete(int dvdId)
        {
            _dvdRepo.Delete(dvdId);
        }

        public void LendDVD(DVD dvd, Borrower borrower)
        {
            _dvdRepo.LendDVD(dvd.DvdId, borrower);
        }

        public void ReturnDVD(int dvdId, Borrower borrower)
        {
            _dvdRepo.ReturnDVD(dvdId, borrower);
        }
    }
}
