using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var item = _dbContext.ProductImages.Where(X => X.ProductId == id).ToList();
            return View(item);
        }

        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            _dbContext.ProductImages.Add(new ProductImage { 
                ProductId=productId,
                Image=url,
                IsDefault=false
            });
            _dbContext.SaveChanges();
            return Json(new
            {
                success = true});
        }

        [HttpPost]
        public ActionResult Delete(int id) 
        {
            var item = _dbContext.ProductImages.Find(id);
            if (item.IsDefault == true)
            {
                return Json(new { success = false });
            }
            _dbContext.ProductImages.Remove(item);
            _dbContext.SaveChanges();
            return Json(new {success =true });
        }

        [HttpPost]
        public ActionResult ChangeMainImage(int productId, int id, bool isDefault)
        {
            var items = _dbContext.ProductImages.Where(i=> i.ProductId ==  productId).ToList();
            var mainImage = _dbContext.ProductImages.Where(i => i.ProductId == productId && i.Id == id).FirstOrDefault();
            if (mainImage.IsDefault == true)
            {
                return Json(new { success = false });
            }
            if (items.Count > 0 && mainImage.IsDefault == false)
            {

                foreach (var item in items)
                {
                    item.IsDefault = false;
                    _dbContext.SaveChanges();
                }
                mainImage.IsDefault = true;

                //sau khi thay đổi ảnh đại diện xong là phải lưu vào bảng product - truong Image - Phan nay van chua lam
                var product = _dbContext.Products.Where(p => p.Id == mainImage.ProductId).FirstOrDefault();
                product.Image = mainImage.Image;
                _dbContext.Products.Attach(product);
                _dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                //da xong phan chuyen anh dai dien thi cung chuyen du lieu sang truong Image bảng product
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
            }

            return Json(new { success = true });
        }
    }
}