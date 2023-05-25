using Microsoft.EntityFrameworkCore;
using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ShoppingCartApp.Services.ModelViews;

namespace Testing
{
    /// <summary>
    /// OrderReposTest  class to perform nunit testing on OrderRepository
    /// </summary>
    public class OrderReposTest
    {
        DatabaseContext databaseContext;
        OrderRepository orderRepository;
        List<Order> orders;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartAPP-db")
            .Options;
            databaseContext = new DatabaseContext(options);
            orders = new List<Order>()
            {
                new Order() { OrderId = 1, AddressId = 1, CartId = 1, OrderPrice = 100, UserId = 1, OrderStatus = "Delivered" },
                new Order() { OrderId = 2, AddressId = 1, CartId = 1, OrderPrice = 100, UserId = 1, OrderStatus = "Not - Delivered" },
                new Order() { OrderId = 3, AddressId = 1, CartId = 3, OrderPrice = 100, UserId = 3, OrderStatus = "Delivered" },
            };
            databaseContext.Orders.AddRange(orders);
            databaseContext.SaveChanges();
            orderRepository = new OrderRepository(databaseContext);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            databaseContext.RemoveRange(orders);
            databaseContext.SaveChanges();
            databaseContext.Dispose();
        }
        [Test]
        [Order(1)]
        public void GettAllOrdersTest()
        {
            string name = "Delivered";
            var entity = orderRepository.GetAllOrders(name);
            Assert.AreEqual(3, entity.Count);
        }
    }
}
