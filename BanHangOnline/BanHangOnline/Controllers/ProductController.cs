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
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int? page, int? category,string searchText)
        {
            var pageSize = 2;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = _dbContext.Products.OrderByDescending(p => p.Id);
            if (category != null)
            {
                ViewBag.category = category;
                items = items.Where(p => p.ProductCategoryId == category);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(p => p.Alias.Contains(searchText) || p.Title.Contains(searchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchText = searchText;
            return View(items);
            //if (category != null)
            //{
            //    ViewBag.category = category;
            //    var productList = _dbContext.Products.OrderByDescending(p => p.Id)
            //                                .Where(p => p.ProductCategoryId == category)
            //                                .ToPagedList(pageNumber, pageSize);
            //    return View(productList);
            //}
            //else
            //{
            //    var productList = _dbContext.Products.OrderByDescending(p => p.Id)
            //                                .ToPagedList(pageNumber, pageSize);
            //    return View(productList);
            //}
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