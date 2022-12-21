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
        public ActionResult MenuLeftProductCategories(int? id,string SearchText, string SortText)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                ViewBag.SearchText = SearchText;
            }
            if (!string.IsNullOrEmpty(SortText))
            {
                ViewBag.SortText = SortText;
            }
            if (id != null)
            {
                ViewBag.category = id;
            }
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuLeftProductCategories", items);
        }
        public ActionResult MenuLeftCategories(int? id, string SearchText)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                ViewBag.SearchText = SearchText;
            }
            if (id != null)
            {
                ViewBag.category = id;
            }
            var items = _dbContext.Categories.ToList();
            return PartialView("_MenuLeftCategories", items);
        }
    }
}