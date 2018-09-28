using StudentManagement.Entities;
using StudentManagement.Services;
using StudentManagement.Services.Interfaces;
using StudentManagement.Web.Resources;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentApp.Web.Controllers
{
    public class ActorsController : Controller
    {
        private IActorService _actorService;

        /// <summary>
        /// Default constructor for class
        /// </summary>
        public ActorsController() :
          this(new ActorService())
        { }

        /// <summary>
        /// Constructor receiving a service parameter to initialize
        /// </summary>
        /// <param name="actorService"></param>
        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Login view. This Method shows the username/password screen
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //If User is still logged, redirect to index. This prevents a bug where the user logs in
            //closes the browser, and when re-opens it, the cookie still exist but it still sent to the login page
            if (User != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Students");

            return View();
        }

        /// <summary>
        /// Login method to validate username/password and create a cookie for authentication
        /// </summary>
        /// <param name="LogActor"></param>
        /// <returns>Redirects to student list if the user is valid within the Actor table</returns>
        [HttpPost]
        public ActionResult Login(Actor LogActor)
        {
            ViewBag.ErrorMessage = "";
            var actor = _actorService.GetValidActor(LogActor);

            if (actor != null)
            {
                //Create a cookie with the information of the logged user 
                this.GenerateAuthenticationCookie(actor);
                return RedirectToAction("Index", "Students");
            }
            else
            {
                ViewBag.ErrorMessage = GlobalResources.InvalidUserOrPassword;
                return View();
            }
        }

        /// <summary>
        /// Logout method. Should only be called if there is an user logged in
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                //If there is no user logged, this would throw an exception
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            catch
            {
                //This error is caused by the browser maintaining the user cookie when the application
                //is closed (until it expires). Need to improve the logic behind this bug
                Console.WriteLine(GlobalResources.NoUserLogged);
            }
            return RedirectToAction("Login", "Actors");
        }

        /// <summary>
        /// Creates a cookie with the information of the logged actor
        /// </summary>
        /// <param name="actor"></param>
        private void GenerateAuthenticationCookie(Actor actor)
        {
            FormsAuthentication.SetAuthCookie(actor.UserName, false);

            var authTicket = new FormsAuthenticationTicket(1, actor.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, actor.Role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }
    }
}

