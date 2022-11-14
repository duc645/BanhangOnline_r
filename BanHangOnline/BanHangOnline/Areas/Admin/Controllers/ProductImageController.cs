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
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}