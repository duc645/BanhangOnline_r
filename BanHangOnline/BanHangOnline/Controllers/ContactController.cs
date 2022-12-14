using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddContact()
        {
            if(User.Identity.GetUserId() != null) {

                var id = User.Identity.GetUserId();
                var item = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.User = item;
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddContact(Contact model)
        {
            if (User.Identity.GetUserId() != null)
            {

                var id = User.Identity.GetUserId();
                var item = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.User = item;
            }
            if (ModelState.IsValid)
            {
                if(User.Identity.GetUserId() != null)
                {
                    model.CreateBy = User.Identity.GetUserId();
                }
                model.IsRead = false;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                _dbContext.Contacts.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}