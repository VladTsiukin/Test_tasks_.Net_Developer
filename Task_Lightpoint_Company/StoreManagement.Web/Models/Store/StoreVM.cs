using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Web.Models.Store
{
    public class StoreVM
    {
        public int StoreId { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        [MaxLength(256)]
        public string StoreHours { get; set; }
        public IEnumerable<int> ProductsIds { get; set; }
    }
}
