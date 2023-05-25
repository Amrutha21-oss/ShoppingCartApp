using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services.ModelViews
{
    public class ProductCartView
    {
        public int? ProductId { get; set; }
        public int? CartId { get; set; }
        public int CartCount { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }

    }
}
