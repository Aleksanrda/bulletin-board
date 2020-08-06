using BulletinBoard.Api.Adverts;
using BulletinBoard.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Entity.Infrastructure;
using BulletinBoard.DAL;
using System.Threading.Tasks;
using BulletinBoard.Api.Comments;

namespace BulletinBoard.Web.Controllers
{
    [AllowAnonymous]
    public class AdvertController : Controller
    {
        private readonly IAdvertsService _advertsService;
        private readonly ICommentsService _commentsService;
        private readonly UserStore<User> _userStore;
        private UserManager<User> _userManager;

        public AdvertController(IAdvertsService advertsService, ICommentsService commentsService,
            UserStore<User> userStore)
        {
            _advertsService = advertsService;
            _commentsService = commentsService;
            _userStore = userStore;
            _userManager = new UserManager<User>(userStore);
        }

        public ActionResult Index()
        {
            var model = _advertsService.GetAdverts();

            return View(model);
        }

        public ActionResult GetUserAdverts()
        {
            User user = _userManager.FindByName(HttpContext.User.Identity.Name);

            var model = _advertsService.GetUserAdverts(user.Id);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var model = await _advertsService.GetAdvertById(id);

            if (model == null)
            {
                return View("NotFound");
            }

            var advertComments = _commentsService.GetUserComments(id);
       
            ViewBag.Comments = advertComments;

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNewAdvert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewAdvert(Advert advert)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(advert.Photo.FileName);
                string extension = Path.GetExtension(advert.Photo.FileName);

                fileName = fileName + DateTime.UtcNow.ToString("yymmssfff") + extension;
                advert.ImagePath = "~/Image/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);

                advert.Photo.SaveAs(fileName);

                User user = _userManager.FindByName(HttpContext.User.Identity.Name);

                await _advertsService.AddEvent(advert, user.Id);

                ViewData["Created"] = "Event successfully created";
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _advertsService.GetAdvertById(id);

            if (model == null)
            {
                return View("NotFound");
            }

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection form)
        {
            await _advertsService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _advertsService.GetAdvertById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Advert advert)
        {
            if (ModelState.IsValid)
            {
                User user = _userManager.FindByName(HttpContext.User.Identity.Name);

                await _advertsService.Update(advert, user.Id);

                return RedirectToAction("Details", new { id = advert.Id });
            }

            return View(advert);
        }
    }
}