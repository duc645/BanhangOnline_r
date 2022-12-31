using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Author
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            //ViewBag.Products = _dbContext.Authors.OrderBy(x => x.CreatedDate).Include(p=>p.Products).Count();
            IEnumerable<Author> items = _dbContext.Authors.OrderByDescending(x => x.Id).Include(p => p.Products);

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.Status = TempData["message"];
            Session["pageAdminAuthor"] = page;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Author model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                _dbContext.Authors.Add(model);
                _dbContext.SaveChanges();
                TempData["message"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = _dbContext.Authors.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author model)
        {
            if (ModelState.IsValid)
            {

                model.ModifiedDate = DateTime.Now;

                _dbContext.Authors.Attach(model);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["message"] = "Sửa thành công";
                return RedirectToAction("Index", new { page = int.Parse(Session["pageAdminAuthor"].ToString()) });

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Authors.Find(id);
            if (item != null)
            {
                _dbContext.Authors.Remove(item);
                _dbContext.SaveChanges();
                //TempData["message"] = "Xóa thành công";
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult deletedAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _dbContext.Authors.Find(Convert.ToInt32(item));
                        _dbContext.Authors.Remove(obj);
                        _dbContext.SaveChanges();
                    }

                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Authors.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

    }
}