using Moq;
using NUnit.Framework;
using StudentApp.Web.Controllers;
using StudentManagement.Entities;
using StudentManagement.Services.Interfaces;
using System.Web.Mvc;

namespace StudentManagement.Web.Tests.Controllers
{
    [TestFixture]
    class ActorsControllerTest
    {
        /// <summary>
        /// Test of Login action. Must return a view
        /// </summary>
        [Test]
        public void TestLoginAction()
        {
            var controller = new ActorsController();
            var result = controller.Login() as ViewResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test of Login action with invalid user information. Must return a view wih empty model
        /// </summary>
        [Test]
        public void TestLoginFail()
        {
            Actor actor = new Actor();
            actor.UserName = "sarah@example.edu";
            actor.Password = "studentAdmi";

            Mock<IActorService> actorService = new Mock<IActorService>();
            var controller = new ActorsController(actorService.Object);
            var result = controller.Login(actor) as ViewResult;
   
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        /// <summary>
        /// Test of Logout method. Must validate the redirection to login page
        /// </summary>
        [Test]
        public void TestLogoutAction()
        {
            var controller = new ActorsController();
            var result = (RedirectToRouteResult)controller.Logout();

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }
    }
}
