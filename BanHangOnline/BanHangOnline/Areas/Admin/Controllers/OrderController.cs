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
    public class OrderController : Controller
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = _dbContext.Orders.Where(x=>x.OrderStatusId ==1 || x.OrderStatusId==2).OrderByDescending(x => x.CreatedDate).ToList();

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

        public ActionResult IndexOrderCancel(int? page)
        {
            var items = _dbContext.Orders.Where(x => x.OrderStatusId == 3).OrderByDescending(x => x.CreatedDate).ToList();

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
            var item = _dbContext.Orders.Find(id);
            return View(item);
        }
        public ActionResult Partial_SanPham(int id)
        {
            var items = _dbContext.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }
        public ActionResult EditStatus(int id)
        {
            ViewBag.Status = new SelectList(_dbContext.OrderStatuss.ToList(), "Id", "Title");
            var item = _dbContext.Orders.Find(id);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStatus(Order model)
        {
            if (ModelState.IsValid)
            {

                model.ModifiedDate = DateTime.Now;
                
                _dbContext.Orders.Attach(model);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();

                if(model.OrderStatusId == 3)
                {
                    var orderD = _dbContext.Orders.Where(o => o.Id == model.Id).Include(x => x.OrderDetails).FirstOrDefault();
                    if(orderD != null)
                    {
                        foreach (var item_details in orderD.OrderDetails)
                        {
                            var product = _dbContext.Products.Where(x => x.Id == item_details.ProductId).FirstOrDefault();
                            if (product != null)
                            {
                                product.Quantity += item_details.Quantity;
                                _dbContext.Products.Attach(product);
                                _dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                                _dbContext.SaveChanges();
                            }
                        }
                    }

                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}