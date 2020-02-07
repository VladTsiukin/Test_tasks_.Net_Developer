using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Web.Models.Product
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        public IEnumerable<int> StoresIds { get; set; }
    }
}
