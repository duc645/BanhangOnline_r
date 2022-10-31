﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory:CommonAbstract
    {
        public ProductCategory()
        {
            //this.Product_Categories = new HashSet<Product_Category>();
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string SeoKeywords { get; set; }

        //public ICollection<Product_Category> Product_Categories { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}