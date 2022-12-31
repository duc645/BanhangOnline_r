using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int? page, int? category,string searchText,string sortText,int? author,int? publisher)
        {
            var pageSize = 9;
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
            if (author != null)
            {
                ViewBag.author = author;
                items = items.Where(p => p.AuthorId == author);
            }
            if (publisher != null)
            {
                ViewBag.publisher = publisher;
                items = items.Where(p => p.PublisherId == publisher);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                items = items.Where(p => p.Alias.Contains(searchText) || p.Title.Contains(searchText));
            }
            if (!string.IsNullOrEmpty(sortText))
            {
                switch (sortText)
                {
                    case "price_desc":
                        ViewBag.SortText = sortText;
                        items = items.OrderByDescending(s => s.PriceM);
                        break;
                    case "price_asc":
                        ViewBag.SortText = sortText;
                        items = items.OrderBy(s => s.PriceM);
                        break;
                    case "product_sold":
                        ViewBag.SortText = sortText;
                        items = items.OrderByDescending(s => s.ProductSold);
                        break;
                    case "view_count":
                        ViewBag.SortText = sortText;
                        items = items.OrderByDescending(s => s.ViewCout);
                        break;
                    case "z-a":
                        ViewBag.SortText = sortText;
                        items = items.OrderByDescending(s => s.Title);
                        break;
                    case "a-z":
                        ViewBag.SortText = sortText;
                        items = items.OrderBy(s => s.Title);
                        break;
                    case "pub_year":
                        ViewBag.SortText = sortText;
                        items = items.OrderByDescending(s => s.PublishedYear);
                        break;

                }

            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

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
        //public ActionResult Partial_ItemsByCateId()
        //{
        //    var items = _dbContext.Products.Where(p => p.IsHome ==true && p.IsActive ==true).Take(12).Include(c => c.ProductImages).ToList();
        //    return PartialView(items);

        //}

        public ActionResult Partial_ProductSale()
        {
            var items = _dbContext.Products.Where(p =>  p.IsActive == true && p.PriceSale>0).Take(4).Include(c => c.ProductImages).ToList();
            return PartialView(items);

        }

        public ActionResult Partial_TopProductSold()
        {
            var items = _dbContext.Products.Where(p => p.IsActive == true).OrderByDescending(p=>p.ProductSold).Take(5).Include(c => c.ProductImages).ToList();
            return PartialView(items);

        }
        public ActionResult Partial_TopViewCount()
        {
            var items =  _dbContext.Products.Where(p => p.IsActive == true).OrderByDescending(p => p.ViewCout).Take(5).Include(c => c.ProductImages).ToList();
            return PartialView(items);

        }

        public ActionResult Partial_TopProductNew()
        {
            var items = _dbContext.Products.Where(p => p.IsActive == true).OrderByDescending(p => p.CreatedDate).Take(5).Include(c => c.ProductImages).ToList();
            return PartialView(items);

        }
        public ActionResult ProductDetail(int? id)
        {
            var item = _dbContext.Products.Where(p=> p.Id==id).Include(x => x.ProductImages).FirstOrDefault();
            if (item != null)
            {
                _dbContext.Products.Attach(item);
                item.ViewCout = item.ViewCout + 1;
                _dbContext.Entry(item).Property(x =>x.ViewCout).IsModified = true;
                _dbContext.SaveChanges();
            }
            return View(item);
        }
    }
}