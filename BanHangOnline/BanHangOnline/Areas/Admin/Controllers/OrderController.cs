using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var items = _dbContext.Orders.OrderByDescending(p => p.CreatedDate).ToList();
            return View();
        }
    }
}