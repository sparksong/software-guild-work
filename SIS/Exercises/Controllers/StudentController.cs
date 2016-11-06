using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            studentVM.Student.Address = new Address();

            StudentRepository.Add(studentVM.Student);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var studentVM = new StudentVM();
            studentVM.SetCourseItems(CourseRepository.GetAll());
            studentVM.SetMajorItems(MajorRepository.GetAll());
            studentVM.SetStateItems(StateRepository.GetAll());
            studentVM.Student = StudentRepository.Get(id);

            if (studentVM.Student.Courses != null)
            {
                foreach (Course course in studentVM.Student.Courses)
                {
                    studentVM.SelectedCourseIds.Add(course.CourseId);
                }
            }

            return View(studentVM);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            studentVM.Student.Address.State = StateRepository.Get(studentVM.Student.Address.State.StateAbbreviation);

            StudentRepository.SaveAddress(studentVM.Student.StudentId, studentVM.Student.Address);

            StudentRepository.Edit(studentVM.Student);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            var student = StudentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}