using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BanHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        //public ActionResult CheckOut2()
        //{
        //    ShoppingCart cart = (ShoppingCart)Session["Cart"];
        //    if (cart != null && cart.items.Count > 0)
        //    {
        //        ViewBag.CheckCart = cart;
        //    }
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CheckOut2(CustomerViewModel order)
        //{
        //    ShoppingCart cart = (ShoppingCart)Session["Cart"];
        //    if (cart != null && cart.items.Count > 0)
        //    {
        //        ViewBag.CheckCart = cart;
        //    }
        //    return View();
        //}



        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        public ActionResult CheckOutSuccess()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CustomerViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null && cart.items.Count >0)
                {
                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    order.Email = req.Email;
                    order.UserId = User.Identity.GetUserId();
                    cart.items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    order.TotalAmount = cart.items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = req.Payment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.OrderStatusId = 1;
                    //order.CreatedBy = req.Phone;
                    //Random rd = new Random();
                    //order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges();

                    //gui gmail khach khi dat hang xong
                    var strSanPham = "";
                    var thanhtien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach (var s in cart.items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + s.ProductName + "</td>";
                        strSanPham += "<td>x " + s.Quantity + "</td>";
                        strSanPham += "<td>" + BanHangOnline.Common.Common.FormatNumber(s.TotalPrice, 0) + " Đ</td>";
                        strSanPham += "</tr>";
                        thanhtien += s.Price * s.Quantity;
                    }
                    TongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Id.ToString());
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", BanHangOnline.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", BanHangOnline.Common.Common.FormatNumber(TongTien, 0));
                    BanHangOnline.Common.Common.SendMail("SachARS", "Đơn hàng #" + order.Id.ToString(), contentCustomer.ToString(), req.Email);

                    //string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                    //contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    //contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    //contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    //contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    //contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    //contentAdmin = contentAdmin.Replace("{{Email}}", req.Email);
                    //contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    //contentAdmin = contentAdmin.Replace("{{ThanhTien}}", BanHangOnline.Common.Common.FormatNumber(thanhtien, 0));
                    //contentAdmin = contentAdmin.Replace("{{TongTien}}", BanHangOnline.Common.Common.FormatNumber(TongTien, 0));
                    //BanHangOnline.Common.Common.SendMail("ShopOnline", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);

                    foreach(var item in cart.items)
                    {
                        var productc = _dbContext.Products.Find(item.ProductId);
                        if(productc != null)
                        {
                            productc.Quantity = productc.Quantity - item.Quantity;
                            _dbContext.SaveChanges();
                        }
                    }

                    cart.ClearCart();
                    //return Redirect("/ShoppingCart");
                    return Json(new { url = Url.Action("CheckOutSuccess","ShoppingCart") });
                }
            }
            return Json(code);
        }

        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        public ActionResult Partial_Item_Thanhtoan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
                return PartialView(cart.items);
            }
            return PartialView();
        }



        [HttpPost]
        public ActionResult AddToCart(int id,int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.Where(x => x.Id == id).Include(c=>c.ProductCategory).FirstOrDefault();
            if(checkProduct !=null && checkProduct.Quantity < quantity)
            {
                code = new { Success = false, msg = " Số lượng hàng trong kho không đủ ", code = -1, Count = 0};
                return Json(code);
            }
            if(checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    ProductImg = checkProduct.Image,
                    Quantity = quantity
                };
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                code = new { Success = true, msg = " Đã thêm sản phẩm vào giỏ hàng", code = 1, Count = cart.items.Count };
                Session["Cart"] = cart;
            }
            return Json(code);

        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.Where(x => x.Id == id).Include(c => c.ProductCategory).FirstOrDefault();
            if (checkProduct != null && checkProduct.Quantity < quantity)
            {
                return Json(new { Success = false });
            }
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
}