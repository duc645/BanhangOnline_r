using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        //cac san pham o trang chu dc lay ra boi danh muc () nam trong phan view _MenuProductCategory
        //lay san pham o trang chu , ten ham viet sai
        public ActionResult Partial_ItemsByCateId()
        {
            var items = _dbContext.Products.Where(p => p.IsHome ==true && p.IsActive ==true).Take(12).Include(c => c.ProductImages).ToList();
            return PartialView(items);

        }

        public ActionResult Partial_ProductSale()
        {
            var items = _dbContext.Products.Where(p => p.IsSale == true && p.IsActive == true && p.PriceSale>0).Take(4).Include(c => c.ProductImages).ToList();
            return PartialView(items);

        }
    }
}