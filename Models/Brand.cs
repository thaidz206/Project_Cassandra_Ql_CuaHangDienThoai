using System;

namespace Project_Cassandra.Models
{
    public class Brand
    {
        public Guid BrandId { get; set; } // UUID cho brand_id

        public string BrandName { get; set; } // Thêm modifier 'required'
    }
}
