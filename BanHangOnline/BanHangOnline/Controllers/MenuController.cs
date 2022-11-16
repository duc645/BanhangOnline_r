using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop",items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory",items);
        }
    }
}