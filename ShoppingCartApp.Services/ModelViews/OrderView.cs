using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services.ModelViews
{
    /// <summary>
    /// OrderView
    /// Display column of address, Foodcart,FoodcartItem tables
    /// </summary>
    public class OrderView
    {
        public int OrderID { get; set; }
        public string DeliveryAddress { get; set; }
        public float OrderPrice { get; set; }
        public string OrderStatus { get; set; }
    }
}
