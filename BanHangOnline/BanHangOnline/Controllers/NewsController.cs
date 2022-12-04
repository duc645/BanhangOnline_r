using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_News3_Home()
        {

            var items = _dbContext.News.OrderByDescending(x=>x.CreatedDate).Take(3).ToList();
            return PartialView(items);
        }
    }
}