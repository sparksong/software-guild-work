using DVDLibrary.BLL;
using DVDLibrary.Models;
using DVDLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDLibrary.Controllers
{
    public class LibraryController : Controller
    {
        LibraryManager _dvdManager = LibraryManagerFactory.Create();

        [HttpGet]
        public ActionResult Collection()
        {
            DVDListVM vm = new DVDListVM();
            vm.DVDs = _dvdManager.GetAll().ToList();

            return View(vm);
        }

        [HttpGet]
        public ActionResult AddDVD()
        {
            DVDListVM vm = new DVDListVM();

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddDVD(DVDListVM model)
        {
            if (ModelState.IsValid)
            {
                DVD newDVD = new DVD();

                newDVD.Title = model.dvd.Title;
                newDVD.ReleaseYear = model.dvd.ReleaseYear;
                newDVD.DirectorName = model.dvd.DirectorName;
                newDVD.Studio = model.dvd.Studio;
                newDVD.MPAARating = model.dvd.MPAARating;
                
                newDVD.ActorList = model.dvd.ActorList;
                
                newDVD.UserNotes = model.dvd.UserNotes;
                newDVD.UserRating = model.dvd.UserRating;

                _dvdManager.AddDVD(newDVD);

                return RedirectToAction("Collection");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult ManageDVD(int id)
        {
            var model = new DVDListVM();
            model.dvd = _dvdManager.Get(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult ManageDVD(DVDListVM model)
        {
            if (ModelState.IsValid)
            {
                _dvdManager.Manage(model.dvd);
                return RedirectToAction("Collection");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Get(int dvdId)
        {
            _dvdManager.Get(dvdId);

            return View(_dvdManager);
        }

        [HttpGet]
        public ActionResult DeleteDVD(int DvdId)
        {
            var model = new DVDListVM();
            model.dvd = _dvdManager.Get(DvdId);

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteDVD(DVD dvd)
        {
            _dvdManager.Delete(dvd.DvdId);
            return RedirectToAction("Collection");
        }

        [HttpGet]
        public ActionResult ManageDVDs()
        {
            DVDListVM model = new DVDListVM();
            model.DVDs = _dvdManager.GetAll().ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult ManageDVDs(DVDListVM model)
        {
            return RedirectToAction("ManageDVD", new { id = model.dvd.DvdId });

        }

        [HttpGet]
        public ActionResult LendDvd(int DvdId)
        {
            var lendModel = new DVDListVM();
            lendModel.dvd = _dvdManager.Get(DvdId);

            return View(lendModel);
        }

        [HttpPost]
        public ActionResult LendDvd(DVDListVM lendModel)
        {
            if (ModelState.IsValid)
            {
                _dvdManager.LendDVD(lendModel.dvd, lendModel.borrower);
                return RedirectToAction("Collection");
            }
            else
            {
                return View(lendModel);
            }
        }

        [HttpGet]
        public ActionResult ReturnDvd(int DvdId)
        {
            var returnModel = new DVDListVM();
            returnModel.dvd = _dvdManager.Get(DvdId);
            
            returnModel.borrower = returnModel.dvd.BorrowerList.FirstOrDefault(b => b.ReturnDate == null);

            _dvdManager.ReturnDVD(returnModel.dvd.DvdId, returnModel.borrower);
            return RedirectToAction("Collection");
        }
    }
}