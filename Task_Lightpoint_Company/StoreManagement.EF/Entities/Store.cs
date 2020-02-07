using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace StoreManagement.EF.Entities
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(256)]
        public string StoreHours { get; set; }       
        public IList<StoreProduct> StoresProducts { get; set; }
    }
}