using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Subscribe")]
    public class Subscribe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150, ErrorMessage = "Không được viết trên 150 ký tự")]
        [EmailAddress(ErrorMessage = "Hãy nhập đúng định dạng email")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}