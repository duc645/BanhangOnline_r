﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Product")]
    public class Product:CommonAbstract
    {

        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.ProductImages = new HashSet<ProductImage>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string Alias { get; set; }
        [StringLength(50)]

        public string Description { get; set; }
        [AllowHtml]

        public string Detail { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public decimal PriceM { get; set; }



        public bool IsActive { get; set; }





        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<ProductImage>  ProductImages { get; set; }

        public int ViewCout { get; set; }

        public int ProductSold { get; set; }
    }
}