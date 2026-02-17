using System.Web.Mvc;
using DemoDBFirst.Models;
using DemoDBFirst.Repositories;

namespace DemoDBFirst.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var students = StudentRepository.GetAll();
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            var student = StudentRepository.Get(id);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            StudentRepository.Add(student);
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            var student = StudentRepository.Get(id);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            StudentRepository.Update(student);
            return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            var student = StudentRepository.Get(id);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}