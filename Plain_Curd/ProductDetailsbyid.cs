    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plain_Curd
{
    public class ProductDetailsbyid
    {
        public int Slno { get; set; }
        public Guid uniquekey { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal weight { get; set; }
        public decimal height { get; set; }
        public decimal width { get; set; }
        public long quantity { get; set; }
        public string image { get; set; }
        public string color { get; set; }
        public string itemcode { get; set; }
    }
}