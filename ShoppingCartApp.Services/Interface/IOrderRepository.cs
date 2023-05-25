using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface IOrderRepository
    {
        /// <summary>
        /// IOrderRepository interface for method declaration to perform CRUD opertaions over Order table.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public Order AddOrder(int userId, int addressId);
        public void UpdateOrderStatus(int orderId);
        public List<OrderView> GetOrderByUserid(int userId);
        public List<Order> GetAllOrders(string deliveryStatus);
    }
}
