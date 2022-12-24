using BanHangOnline.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        [Obsolete]
        public async Task<ActionResult> Index()
        {
            //Tổng số đơn hàng : các trạng thái
            var OrderCompleted = await _dbContext.Orders.Where(p => p.OrderStatusId == 2).ToListAsync();
            ViewBag.AllOrderComPlete = OrderCompleted.Count();
            var OrderShip = await _dbContext.Orders.Where(p => p.OrderStatusId == 4).ToListAsync();
            ViewBag.AllOrderShipping = OrderShip.Count();
            var OrderHandling = await _dbContext.Orders.Where(p => p.OrderStatusId == 1).ToListAsync();
            ViewBag.AllOrderHandling = OrderHandling.Count();
            var OrderCancel = await _dbContext.Orders.Where(p => p.OrderStatusId == 3).ToListAsync();
            ViewBag.AllOrderCancel = OrderCancel.Count();

            //Tổng số đơn hàng trong tháng : các trạng thái
            //trạng thái thành công
            var OrderOfMonthCompleted = await _dbContext.Orders.Where(u => u.CreatedDate.Month == DateTime.Now.Month && u.OrderStatusId == 2).ToListAsync();
            ViewBag.OrderOfMonthCompleted = OrderOfMonthCompleted.Count();
            //đang xử lý
            var OrderOfMonthHandling = await _dbContext.Orders.Where(u => u.CreatedDate.Month == DateTime.Now.Month && u.OrderStatusId == 1).ToListAsync();
            ViewBag.OrderOfMonthHandling = OrderOfMonthHandling.Count();
            //hủy
            var OrderOfMonthCancel = await _dbContext.Orders.Where(u => u.CreatedDate.Month == DateTime.Now.Month && u.OrderStatusId == 3).ToListAsync();
            ViewBag.OrderOfMonthCancel = OrderOfMonthCancel.Count();
            //đang giao
            var OrderOfMonthShipping = await _dbContext.Orders.Where(u => u.CreatedDate.Month == DateTime.Now.Month && u.OrderStatusId == 4).ToListAsync();
            ViewBag.OrderOfMonthShipping = OrderOfMonthShipping.Count();






            //Tổng doanh thu
            var totalOrderSale = await _dbContext.Orders.Where(p => p.OrderStatusId == 2).SumAsync(i => i.TotalAmount);
            ViewBag.totalOrderSale = totalOrderSale;

            //Doanh thu ngày
            var timeDateOfNow = DateTime.Now.Day;
            var totalOrderOfDateTestDay = await _dbContext.Orders.Where(u => u.CreatedDate.Day == timeDateOfNow && u.OrderStatusId == 2).ToListAsync();
            if(totalOrderOfDateTestDay.Count > 0)
            {
                var totalOrderOfDate = await _dbContext.Orders.Where(u => u.CreatedDate.Day == timeDateOfNow && u.OrderStatusId == 2).SumAsync(u => (decimal?)u.TotalAmount);
                ViewBag.totalOrderOfDate = totalOrderOfDate;
            }


            //Doanh thu tháng
            var timeMonthOfNow = DateTime.Now.Month;
            var totalOrderOfMonthTestDay = await _dbContext.Orders.Where(u => u.CreatedDate.Month == timeMonthOfNow && u.OrderStatusId == 2).ToListAsync();
            if (totalOrderOfMonthTestDay.Count > 0)
            {
                var totalOrderOfMonth = await _dbContext.Orders.Where(u => u.CreatedDate.Month == timeMonthOfNow && u.OrderStatusId == 2).SumAsync(u => (decimal?)u.TotalAmount);
                ViewBag.totalOrderOfMonth = totalOrderOfMonth;
            }

            //Doanh thu năm
            var timeYearOfNow = DateTime.Now.Year;
            var totalOrderOfYearTestDay = await _dbContext.Orders.Where(u => u.CreatedDate.Year == timeYearOfNow && u.OrderStatusId == 2).ToListAsync();
            if (totalOrderOfMonthTestDay.Count > 0)
            {
                var totalOrderOfYear = await _dbContext.Orders.Where(u => u.CreatedDate.Year == timeYearOfNow && u.OrderStatusId == 2).SumAsync(u => (decimal?)u.TotalAmount);
                ViewBag.totalOrderOfYear = totalOrderOfYear;
            }

            //Số thành viên
            var items = _dbContext.Users.ToList();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
            var userRoles = roleManager.FindByName("Customer").Users;
            var ListUser = new List<ApplicationUser>();
            foreach (var item in userRoles)
            {
                foreach (var user in items)
                {
                    if (item.UserId == user.Id)
                    {
                        ListUser.Add(user);
                    }
                }
            }
            ViewBag.ListUser = ListUser.Count();

            //Số thành viên bị chặn
            var itemsBlock = _dbContext.Users.ToList();
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
            var userRolesBlock = roleManager.FindByName("Customer").Users;
            var ListUserBlock = new List<ApplicationUser>();
            foreach (var item in userRolesBlock)
            {
                foreach (var user in itemsBlock)
                {
                    if (item.UserId == user.Id)
                    {
                        ListUserBlock.Add(user);
                    }
                }
            }
            var ListUserBlockView = ListUserBlock.Where(p => p.Block == 1).ToList();
            ViewBag.ListUserBlockView = ListUserBlockView.Count();
            return View();
        }
    }
}