using StoreManagement.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Web.Models.Product
{
    public class ProductsVM
    {
        public int StoreId { get; set; }
        public List<ProductVM> Products { get; set; }

        public ProductsVM(List<ProductVM> storesModel, int storeId)
        {
            this.Products = storesModel;
            this.StoreId = storeId;
        }

        public ProductsVM()
        {
        }
    }
}