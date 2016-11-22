using DVDLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary.DAL.Interfaces
{
    public interface IDVDRepository
    {
        IEnumerable<DVD> GetAll();

        DVD Get(int dvdId);

        void Add(DVD dvd);

        void Delete(int dvdId);

        void Manage(DVD dvd);

        void LendDVD(int dvdId, Borrower borrower);

        void ReturnDVD(int dvdId, Borrower borrower);
    }
}
