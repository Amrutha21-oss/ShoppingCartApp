using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services.ModelViews
{
    public class ProductCategoryView
    {
        public int ProductCategoryId { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
    }
}
