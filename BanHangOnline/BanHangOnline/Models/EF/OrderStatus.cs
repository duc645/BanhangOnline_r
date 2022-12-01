using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_OrderStatus")]
    public class OrderStatus
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên trạng thái không được để trống")]
        [StringLength(50)]
        public string Title { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}