using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Repository
{
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// OrderRepository implements IOrderrepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        Order order;
        OrderView orderView;
        Product product;
        List<OrderView> orderview;
        public OrderRepository(DatabaseContext _dataBaseContext)
        {
            dataBaseContext = _dataBaseContext;
            order = new Order();
            product = new Product();
        }
        public Order AddOrder(int userId, int addressId)
        {
            Order order = new Order();
            Cart cart = dataBaseContext.Cart.FirstOrDefault(fc => fc.UserId == userId);
            order.CartId = userId;
            order.AddressId = addressId;
            order.UserId = userId;
            order.OrderPrice = cart.CartPrice;
            order.OrderStatus = "Not Delivered";
            dataBaseContext.Add(order);
            dataBaseContext.SaveChanges();

            List<ProductsCart> productsCart = dataBaseContext.ProductsCart.Where(fc => fc.CartId == cart.CartId).ToList();
            dataBaseContext.ProductsCart.RemoveRange(productsCart);
            dataBaseContext.SaveChanges();

            cart.CartPrice = 0;
            dataBaseContext.Cart.Update(cart);
            dataBaseContext.SaveChanges();

            return order;

        }
        public List<OrderView> GetOrderByUserid(int userId)
        {
            Address address = dataBaseContext.Addresses.FirstOrDefault(a => a.UserId == userId);
            List<Order> orders = dataBaseContext.Orders.Where(o => o.UserId == userId).ToList();
            if (order != null)
            {
                orderview = orders.Select(o =>
                new OrderView
                {
                    OrderID = o.OrderId,
                    OrderPrice = o.OrderPrice,
                    OrderStatus = o.OrderStatus,
                    DeliveryAddress = address.UserAddress,
                }
                ).ToList();
            }
            return orderview;

        }
        public List<Order> GetAllOrders(string deliveryStatus)
        {
            List<Order> orders = dataBaseContext.Orders.Where(o => o.OrderStatus.Contains(deliveryStatus)).ToList();
            return orders;
        }

        public void UpdateOrderStatus(int orderId)
        {
            var newOrder = dataBaseContext.Orders.Find(orderId);
            newOrder.OrderStatus = "Delivered";
            dataBaseContext.SaveChanges();
        }
    }
}
