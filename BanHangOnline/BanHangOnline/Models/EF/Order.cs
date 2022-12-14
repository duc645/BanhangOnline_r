using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Order")]
    public class Order : CommonAbstract
    {

        public Order() {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public string Code { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = " CustomerName nằm từ {1} đến {0}")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [Phone(ErrorMessage = "Hãy nhâp đúng định dạng số điện thoại")]
        public string Phone {get;set;}
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Address { get; set; }
        public string Email { get; set; }


        public decimal TotalAmount { get; set; }

        public int Quantity { get; set; }
        public int TypePayment { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public string UserId { get; set; }



    }
}