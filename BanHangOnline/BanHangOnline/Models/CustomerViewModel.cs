using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        public int Payment { get; set; }
    }
}