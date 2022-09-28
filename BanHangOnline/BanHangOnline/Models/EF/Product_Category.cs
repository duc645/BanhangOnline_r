using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    public class Product_Category
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 2)]

        public int ProductCategoryId { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}