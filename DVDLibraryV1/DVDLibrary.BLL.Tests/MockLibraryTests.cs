using DVDLibrary.BLL;
using DVDLibrary.Models;
using DVDLibrary.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DVDLibrary.BLL.Tests
{
    [TestFixture]
    public class MockLibraryTests
    {
        //Note: If I were to redo this, I would allow for responses and check for them in the tests and allow for better testing.
        //but due to time constraints, I will do minimal unit testing on this project, and do more for the next.
        //When I have time, I wish to refactor this project and this is a note to myself to do so.
        //*Better unit tests, *add responses for *better validation in the logic layer.

        [Test]        
        public void CanViewDVDList()
        {
            LibraryManager manager = LibraryManagerFactory.Create();
            
            Assert.AreEqual(manager.GetAll().ToList().Count, 4);
        }
        
        [Test]
        [TestCase("FakeMovie", 1991, "Paul Rudd", "The Garage", MPAA.PG13, true)] //Correct information added. Pass.

        public void CanAddDVDTest(string title, int year, string director, string studio, MPAA rating, bool expectedResult)
        {
            bool result;
            LibraryManager manager = LibraryManagerFactory.Create();
            DVD mockDVD = new DVD();
            mockDVD.Title = title;
            mockDVD.ReleaseYear = year;
            mockDVD.DirectorName = director;
            mockDVD.Studio = studio;
            mockDVD.MPAARating = rating;


            manager.AddDVD(mockDVD);

            if (manager.GetAll().ToList().Count == 5)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(mockDVD.DvdId, 5);
        }

        [Test]
        [TestCase(1, "The Snorlax", 2016, "Stinky Pete", "The Garage", MPAA.R, true)]          //Correct date. Pass.

        public void CanEditDVDTest(int dvdId, string title, int year, string director, string studio, MPAA rating, bool expectedResult)
        {
            bool result;
            LibraryManager manager = LibraryManagerFactory.Create();
            DVD editDVD = manager.Get(dvdId);
            
            editDVD.Title = title;
            editDVD.ReleaseYear = year;
            editDVD.DirectorName = director;
            editDVD.Studio = studio;
            editDVD.MPAARating = rating;
            

            manager.Manage(editDVD);

            if (manager.GetAll().ToList().Count == 4)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(editDVD.DvdId, dvdId);
            Assert.AreEqual(editDVD.Title, title);
            Assert.AreEqual(editDVD.ReleaseYear, year);
            Assert.AreEqual(editDVD.DirectorName, director);
            Assert.AreEqual(editDVD.Studio, studio);
            Assert.AreEqual(editDVD.MPAARating, rating);
        }

        [Test]
        [TestCase(7, false)]         //Only 4 DVD's in memory, fail.
        [TestCase(1, true)]          //Delete first DVD, pass.

        public void DeleteDVDTest(int dvdId, bool expectedResult)
        {
            bool result;
            LibraryManager manager = LibraryManagerFactory.Create();

            manager.Delete(dvdId);

            if (manager.GetAll().ToList().Count == 3)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            Assert.AreEqual(expectedResult, result);
        }
    }
}