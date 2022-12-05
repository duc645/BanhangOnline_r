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
    
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {

            //string userId = User.Identity.GetUserId();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Partial_Subcrice()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Subscribe(Subscribe req)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Subscribes.Add(new Subscribe { Email = req.Email, CreatedDate = DateTime.Now });
                _dbContext.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subcrice", req);
        }
    }
}