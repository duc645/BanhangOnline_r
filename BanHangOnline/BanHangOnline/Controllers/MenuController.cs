using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            var items = _dbContext.Categories.ToList();
            return PartialView("_MenuTop",items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory",items);
        }
        public ActionResult MenuLeftAuthor(int? id, string SearchText, string SortText,int? category, int? publisher)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                ViewBag.SearchText = SearchText;
            }
            if (!string.IsNullOrEmpty(SortText))
            {
                ViewBag.SortText = SortText;
            }
            if (publisher != null)
            {
                ViewBag.publisher = publisher;
            }
            if (category != null)
            {
                ViewBag.category = category;
            }
            if (id != null)
            {
                ViewBag.author = id;
            }
            var items = _dbContext.Authors.ToList();
            return PartialView("_MenuLeftAuthor", items);
        }
        public ActionResult MenuLeftPublisher(int? id, string SearchText, string SortText, int? category, int? author)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                ViewBag.SearchText = SearchText;
            }
            if (!string.IsNullOrEmpty(SortText))
            {
                ViewBag.SortText = SortText;
            }
            if (author != null)
            {
                ViewBag.author = author;
            }
            if (category != null)
            {
                ViewBag.category = category;
            }
            if (id != null)
            {
                ViewBag.publisher = id;
            }
            var items = _dbContext.Publishers.ToList();
            return PartialView("_MenuLeftPublisher", items);
        }

        public ActionResult MenuLeftProductCategories(int? id,string SearchText, string SortText, int? author, int? publisher)
        {
            if (publisher != null)
            {
                ViewBag.publisher = publisher;
            }
            if (author != null)
            {
                ViewBag.author = author;
            }
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
        public ActionResult MenuMainProductCategories(int? id, string SearchText, string SortText)
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
            return PartialView("_MenuMainProductCategories", items);
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