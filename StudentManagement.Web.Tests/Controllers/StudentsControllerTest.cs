using Moq;
using NUnit.Framework;
using StudentManagement.Entities;
using StudentManagement.Services.Interfaces;
using StudentManagement.Web.Controllers;
using System;
using System.Web.Mvc;

namespace StudentManagement.Web.Tests.Controllers
{
    [TestFixture]
    class StudentControllerTest
    {
        /// <summary>
        /// Test of Index action. Must return a view
        /// </summary>
        [Test]
        public void TestIndexAction()
        {
            var controller = new StudentsController();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test of Create action. Must return a view
        /// </summary>
        [Test]
        public void TestCreateAction()
        {
            var controller = new StudentsController();
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test of Details action with valid id. Must return a view with a model
        /// </summary>
        [Test]
        public void TestDetailsView()
        {
            var controller = new StudentsController();
            var result = controller.Details(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((Student)result.Model).Id);
        }

        /// <summary>
        /// Test of Details action with invalid id. Must return a view without model
        /// </summary>
        [Test]
        public void TestDetailsViewFail()
        {
            var controller = new StudentsController();
            var result = controller.Details(-1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        /// <summary>
        /// Test of Delete action with valid id. Must return a view with model
        /// </summary>
        [Test]
        public void TestDeleteView()
        {
            var controller = new StudentsController();
            var result = controller.Delete(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((Student)result.Model).Id);
        }

        /// <summary>
        /// Test of Delete action with invalid id. Must return a view without model
        /// </summary>
        [Test]
        public void TestDeleteViewFail()
        {
            var controller = new StudentsController();
            var result = controller.Delete(-1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        /// <summary>
        /// Test of Edit action with a valid id. Must return a view with model
        /// </summary>
        [Test]
        public void TestEditView()
        {
            var controller = new StudentsController();
            var result = controller.Edit(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((Student)result.Model).Id);
        }

        /// <summary>
        /// Test of Edit action with an invalid id. Must return a view without model
        /// </summary>
        [Test]
        public void TestEditViewFail()
        {
            var controller = new StudentsController();
            var result = controller.Edit(-1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        /// <summary>
        /// Test of Create action mocking a student with invalid birthday (less than 18 years)
        /// </summary>
        [Test]
        public void TestCreateStudentInvalidBirthday()
        {
            var controller = new StudentsController();
            Student student = this.CreateMockedStudent();
            student.Birthday = DateTime.Now;

            var result = controller.Create(student) as ViewResult;

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        /// <summary>
        /// Test of Create action mocking a student with invalid phone format
        /// </summary>
        [Test]
        public void TestCreateStudentInvalidPhone()
        {
            var controller = new StudentsController();
            Student student = this.CreateMockedStudent();
            student.PhoneNumber = "556677889900";

            var result = controller.Create(student) as ViewResult;

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        /// <summary>
        /// Test of Create action mocking a student with empty first name
        /// </summary>
        [Test]
        public void TestCreateStudentEmptyName()
        {
            var controller = new StudentsController();
            Student student = this.CreateMockedStudent();
            student.FirstName = "";

            var result = controller.Create(student) as ViewResult;

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        /// <summary>
        /// Test of Create action mocking a student with empty last name
        /// </summary>
        [Test]
        public void TestCreateStudentEmptyLastName()
        {
            var controller = new StudentsController();
            Student student = this.CreateMockedStudent();
            student.LastName = "";

            var result = controller.Create(student) as ViewResult;

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        /// <summary>
        /// Test of Delete action mocking service and a sucessfull deletion
        /// </summary>
        [Test]
        public void TestDeleteConfirmation()
        {
            Mock<IStudentService> studentService = new Mock<IStudentService>();
            var controller = new StudentsController(studentService.Object);

            var result = (RedirectToRouteResult)controller.DeleteConfirmed(2);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        /// <summary>
        /// Function to created a mocked student entity with valid information
        /// </summary>
        private Student CreateMockedStudent()
        {
            Student student = new Student();
            student.Email = "pachomar2@gmail.com";
            student.Birthday = new DateTime(1984, 1, 24);
            student.FirstName = "Omar";
            student.LastName = "Pacheco";
            student.PhoneNumber = "+17863406239";

            return student;
        }
    }
}
