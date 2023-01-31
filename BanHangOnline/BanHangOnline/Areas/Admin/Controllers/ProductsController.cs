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
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index(string searchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = _dbContext.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Contains(searchText) || x.Title.ToLower().Contains(searchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            Session["pageAdminProduct"] = page;
            Session["pageAdminSearch"] = searchText;
            ViewBag.SearchText = searchText;
            return View(items);
        }
        public ActionResult ProductOutOfStock( int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = _dbContext.Products.Where(x=> x.Quantity <20).OrderBy(x=>x.Quantity);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult ProductSoldSale(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = _dbContext.Products.OrderByDescending(x => x.ProductSold).Take(10);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Author = new SelectList(_dbContext.Authors.ToList(), "Id", "FullName");
            ViewBag.Publisher = new SelectList(_dbContext.Publishers.ToList(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model,List<string> Images,List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if(Images != null && Images.Count > 0)
                {
                    for(int i=0;i< Images.Count; i++)
                    {
                        if(i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image=Images[i],
                                IsDefault=true
                            });
                        }
                        else
                        {
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                //loai
                //model.IsHome = false;
                //model.IsSale = false;
                //model.IsFeature = false;
                //model.IsHot = false;
                //loai
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                //if (string.IsNullOrEmpty(model.SeoTitle))
                //{
                //    model.SeoTitle = model.Title;
                //}

                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                }
                if(model.PriceSale>0 && model.PriceSale < model.Price)
                {
                    model.PriceM = model.PriceSale.Value;
                }
                else
                {
                    model.PriceM = model.Price;
                }
                _dbContext.Products.Add(model);
                try
                {
                    _dbContext.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Author = new SelectList(_dbContext.Authors.ToList(), "Id", "FullName");
            ViewBag.Publisher = new SelectList(_dbContext.Publishers.ToList(), "Id", "FullName");
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Author = new SelectList(_dbContext.Authors.ToList(), "Id", "FullName");
            ViewBag.Publisher = new SelectList(_dbContext.Publishers.ToList(), "Id", "FullName");
            var item = _dbContext.Products.Where(x=> x.Id ==id).Include(a => a.ProductImages).FirstOrDefault();
            ViewBag.ProductId = id;
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {

                model.ModifiedDate = DateTime.Now;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                if (model.PriceSale > 0 && model.PriceSale < model.Price)
                {
                    model.PriceM = model.PriceSale.Value;
                }
                else
                {
                    model.PriceM = model.Price;
                }
                _dbContext.Products.Attach(model);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                var avcd = int.Parse(Session["pageAdminProduct"].ToString());
                return RedirectToAction("Index" , new { page = int.Parse(Session["pageAdminProduct"].ToString())});
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Author = new SelectList(_dbContext.Authors.ToList(), "Id", "FullName");
            ViewBag.Publisher = new SelectList(_dbContext.Publishers.ToList(), "Id", "FullName");
            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Products.Find(id);
            //if (item != null)
            //{
                _dbContext.Products.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            //}
            //return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
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
                        var obj = _dbContext.Products.Find(Convert.ToInt32(item));
                        _dbContext.Products.Remove(obj);
                        _dbContext.SaveChanges();
                    }

                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        //[HttpPost]
        //public ActionResult IsHome(int id)
        //{
        //    var item = _dbContext.Products.Find(id);
        //    if (item != null)
        //    {
        //        item.IsHome = !item.IsHome;
        //        _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        //        _dbContext.SaveChanges();
        //        return Json(new { success = true, isHome = item.IsHome });
        //    }
        //    return Json(new { success = false });
        //}

        //[HttpPost]
        //public ActionResult IsSale(int id)
        //{
        //    var item = _dbContext.Products.Find(id);
        //    if (item != null)
        //    {
        //        item.IsSale = !item.IsSale;
        //        _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        //        _dbContext.SaveChanges();
        //        return Json(new { success = true, isSale = item.IsSale });
        //    }
        //    return Json(new { success = false });
        //}
    }
}