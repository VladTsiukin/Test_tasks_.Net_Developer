using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.Core.Dto
{
    public class StoreDto
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string StoreHours { get; set; }
        public IEnumerable<int> ProductsIds { get; set; }
    }
}