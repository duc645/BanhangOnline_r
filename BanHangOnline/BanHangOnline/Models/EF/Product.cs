using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Product")]
    public class Product:CommonAbstract
    {

        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Product_Categories = new HashSet<Product_Category>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }

        public string ProductCode { get; set; }
        public string Description { get; set; }

        public string Detail { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Quantity { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }

        public bool IsHome { get; set; }
        public bool IsSale { get; set; }

        public bool IsFeature { get; set; }

        public bool IsHot { get; set; }


        

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string SeoKeywords { get; set; }

        //public int ProductCategoryId { get; set; }
        //public virtual ProductCategory ProductCategory { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<Product_Category> Product_Categories { get; set; }
    }
}