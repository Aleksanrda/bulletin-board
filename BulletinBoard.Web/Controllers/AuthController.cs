using BulletinBoard.Api.Accounts;
using BulletinBoard.Api.Accounts.DTO;
using BulletinBoard.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoard.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly UserStore<User> userStore;
        private readonly RoleStore<Role> roleStore;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;

        public AuthController(IAccountsService accountsService,
            UserStore<User> userStore, RoleStore<Role> roleStore)
        {
            _accountsService = accountsService;
            this.userStore = userStore;
            this.roleStore = roleStore;
            userManager = new UserManager<User>(userStore);
            roleManager = new RoleManager<Role>(roleStore);
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(PostLoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await _accountsService.Login(model);

                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(PostRegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var registerResult = await _accountsService.Register(model);

                if (registerResult.Succeeded)
                {
                    //userManager.AddToRole(user.Id, "Administrator");
                    return RedirectToAction("LogIn", "Auth");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Error while creating the user!");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PostChangePasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = userManager.FindByName(HttpContext.User.Identity.Name);
                IdentityResult result = userManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Error while changing the password.");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ChangeProfile()
        {
            User user = userManager.FindByName(HttpContext.User.Identity.Name);

            PostChangeProfileDTO model = new PostChangeProfileDTO();
            model.FullName = user.FullName;
            model.BirthDate = user.BirthDate;
            model.Bio = user.Bio;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(PostChangeProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = userManager.FindByName(HttpContext.User.Identity.Name);

                user.FullName = model.FullName;
                user.BirthDate = model.BirthDate;
                user.Bio = model.Bio;
                IdentityResult result = userManager.Update(user);

                if (result.Succeeded)
                {
                    ViewBag.Message = "Profile updated successfully.";
                }
                else
                {
                    ModelState.AddModelError("", "Error while saving profile.");
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}