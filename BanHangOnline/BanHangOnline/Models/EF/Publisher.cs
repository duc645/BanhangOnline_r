using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Publisher")]
    public class Publisher: CommonAbstract
    {
        public Publisher()
        {
            //this.Product_Categories = new HashSet<Product_Category>();
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(150, ErrorMessage = "Không vượt quá 150 ký tự")]

        public string FullName { get; set; }
        [StringLength(150, ErrorMessage = "Không vượt quá 150 ký tự")]
        public string Description { get; set; }

        public bool IsActive { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}