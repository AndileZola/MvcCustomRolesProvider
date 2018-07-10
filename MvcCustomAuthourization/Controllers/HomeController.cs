using MvcCustomAuthourization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcCustomAuthourization.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model,string returnUrl)
        {
            CustomRolesConceptEntities db = new CustomRolesConceptEntities();
            var dataItem = db.Users.Where(x => x.EmailAddress == model.EmailAddress && x.Password == x.Password).First();
            if(dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.EmailAddress,false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("","Invalid user/pass");
                return View();
            }
        }
        [Authorize]
        [AllowAnonymous]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Home");
        }
    }
}