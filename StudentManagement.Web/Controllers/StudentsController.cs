using StudentManagement.Entities;
using StudentManagement.Services;
using StudentManagement.Services.Interfaces;
using StudentManagement.Web.Resources;
using System;
using System.Net;
using System.Web.Mvc;

namespace StudentManagement.Web.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private IStudentService _studentService;

        /// <summary>
        /// Default constructor for class
        /// </summary>
        public StudentsController() :
          this(new StudentService())
        { }

        /// <summary>
        /// Constructor receiving a service parameter to initialize
        /// </summary>
        /// <param name="studentService"></param>
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Index view of student list
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "StudentAdmin,StaffMember")]
        public ActionResult Index()
        {
            return View(_studentService.GetStudents());
        }
        
        /// <summary>
        /// Details view for student. If Id is modified for non-existing one, redirects to list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "StudentAdmin,StaffMember")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentService.FindById(id);
            if (student == null)
            {
                RedirectToAction("Index");
            }
            return View(student);
        }

        /// <summary>
        /// Create student view, only available for Admin
        /// </summary>
        /// <returns></returns>

        [Authorize(Roles = "StudentAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create student method, only available for Admin
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StudentAdmin")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Birthday,NickNames,StudentNumber,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                //Validate that the user email does not exist in database
                if (_studentService.ValidateNonDuplicateEmail(student.Email, -1))
                {
                    try
                    {
                        _studentService.AddStudent(student);
                        return RedirectToAction("Index");
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", GlobalResources.ExistingEmailError);
                }
            }

            return View(student);
        }

        /// <summary>
        /// Edit student details view. Only available for Admin
        /// If id is hardcoded for a non-existing one, redirects to list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "StudentAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentService.FindById(id);
            if (student == null)
            {
                RedirectToAction("Index");
            }
            return View(student);
        }

        /// <summary>
        /// Edit student method, only available for Admin
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StudentAdmin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Birthday,NickNames,StudentNumber,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                //Validate the edited email does not exist in database
                if (_studentService.ValidateNonDuplicateEmail(student.Email, student.Id))
                {
                    try
                    {
                        _studentService.SaveStudent(student);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", GlobalResources.ExistingEmailError);
                }
            }
            return View(student);
        }

        /// <summary>
        /// Delete student view. Only available for Admin
        /// If id is hardcoded for a non-existing one, redirects to list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "StudentAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentService.FindById(id);
            if (student == null)
            {
                RedirectToAction("Index");
            }
            return View(student);
        }

        /// <summary>
        /// Delete student action. Only available for Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StudentAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            _studentService.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
