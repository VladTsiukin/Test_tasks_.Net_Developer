﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.Core.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> StoresIds { get; set; }
    }
}