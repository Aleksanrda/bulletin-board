using BulletinBoard.Core.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoard.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;

        public ActionResult LogIn()
        {
            return View();
        }
    }
}