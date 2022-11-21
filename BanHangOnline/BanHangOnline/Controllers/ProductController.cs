using BanHangOnline.Models;
using PagedList;
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
        public ActionResult Index(int? page, int? category)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            if (category != null)
            {
                ViewBag.category = category;
                var productList = _dbContext.Products.OrderByDescending(p => p.Id)
                                            .Where(p => p.ProductCategoryId == category)
                                            .ToPagedList(pageNumber, pageSize);
                return View(productList);
            }
            else
            {
                var productList = _dbContext.Products.OrderByDescending(p => p.Id)
                                            .ToPagedList(pageNumber, pageSize);
                return View(productList);
            }
            //return View();
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