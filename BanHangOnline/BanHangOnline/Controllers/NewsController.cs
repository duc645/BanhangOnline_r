using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(int? page, int? category, string searchText)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = _dbContext.News.Where(p=> p.IsActive == true).OrderByDescending(p => p.Id);
            if (category != null)
            {
                ViewBag.category = category;
                items = items.Where(p => p.CategoryId== category);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                items = items.Where(p => p.Alias.Contains(searchText) || p.Title.Contains(searchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(items);
        }
        public ActionResult Partial_News3_Home()
        {

            var items = _dbContext.News.Where(p => p.IsActive == true).OrderByDescending(x=>x.CreatedDate).Take(3).ToList();
            return PartialView(items);
        }
        public ActionResult NewDetail(int? id)
        {
            var item = _dbContext.News.Where(p => p.Id == id).Include(n => n.Category).FirstOrDefault();
            if(item!= null)
            {
                _dbContext.News.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                _dbContext.Entry(item).Property(x => x.ViewCount).IsModified = true;
                _dbContext.SaveChanges();
            }
            ViewBag.CategoryToTake3below = item.CategoryId;
            return View(item);
        }

        public ActionResult Take3News_Category(int? id)
        {
            var items = _dbContext.News.Where(p => p.CategoryId == id && p.IsActive == true).Take(3).ToList();
            return PartialView(items);
        }

        public ActionResult SeeMore()
        {
            var item = _dbContext.News.Where(p => p.IsActive == true).OrderByDescending(x => x.ViewCount).Take(3).ToList();
            return PartialView(item);
        }
    }
}