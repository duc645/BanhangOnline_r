using BanHangOnline.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var items = _dbContext.Contacts.OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 5;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult View(int id)
        {
            var item = _dbContext.Contacts.Find(id);
            item.IsRead = true;
            _dbContext.Contacts.Attach(item);
            _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return View(item);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Contacts.Find(id);
            if (item != null)
            {
                _dbContext.Contacts.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}