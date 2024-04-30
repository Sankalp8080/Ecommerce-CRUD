using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plain_Curd
{
    public partial class ProductDetails
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ItemCode { get; set; }
        public string UniqueKey { get; set; }
        public string image { get; set; }
    }
}