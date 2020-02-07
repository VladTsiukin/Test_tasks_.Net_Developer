using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Web.Models.Product
{
    public class EditProductVM
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(256)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(256)]
        public string ProductDescription { get; set; }
    }
}
