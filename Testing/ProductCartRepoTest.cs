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
    /// ProductCartRepoTest  class to perform nunit testing on ProductCartRepository
    /// </summary>
    public class ItemCartReposTest
    {
        DatabaseContext databaseContext;
        ProductRepository productRepository;
        CartRepository cartRepository;
        ProductCartRepository ProductCartRepository;
        List<Product> products;
        List<ProductsCart> productsCarts;
        List<Cart> carts;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartAPP-db")
            .Options;
            databaseContext = new DatabaseContext(options);

            productsCarts = new List<ProductsCart>()
            {
                new ProductsCart(){ProductId =1, CartId = 1, CartCount = 2 },
                new ProductsCart(){ ProductId =2, CartId = 2, CartCount = 1 },
                new ProductsCart(){ ProductId =2, CartId = 3, CartCount = 1 },
            };
            carts = new List<Cart>()
            {
                new Cart() { CartId = 1, UserId = 1, CartPrice = 20 },
                new Cart() { CartId = 2, UserId = 2, CartPrice = 40 },
                new Cart() { CartId = 3, UserId = 3, CartPrice = 210 },
            };
            products = new List<Product>()
            {
                new Product() {ProductId = 1,ProductName = "ipad", ProductDescription ="This is apple product", ProductPrice = 10, ProductCount = 5},
                new Product() {ProductId = 2,ProductName = "Redmi NoteBook", ProductDescription ="This is phone", ProductPrice = 20, ProductCount = 3},
                new Product() {ProductId = 3,ProductName = "Samsung", ProductDescription ="This is phone", ProductPrice = 50, ProductCount = 6},
            };
            databaseContext.Products.AddRange(products);
            databaseContext.Cart.AddRange(carts);
            databaseContext.ProductsCart.AddRange(productsCarts);
            databaseContext.SaveChanges();

            productRepository = new ProductRepository(databaseContext);
            cartRepository = new CartRepository(databaseContext);
            ProductCartRepository = new ProductCartRepository(databaseContext, cartRepository);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            databaseContext.RemoveRange(products);
            databaseContext.RemoveRange(productsCarts);
            databaseContext.RemoveRange(cartRepository);
            databaseContext.SaveChanges();
            databaseContext.Dispose();
        }
        [Test]
        [Order(2)]
        public void AddProductstoCartTest()
        {
            var entity = ProductCartRepository.AddProductInCart(3, 2, 1);
            var entity1 = entity.Item1.Where(i => i.ProductId == 3).Select(i => i.CartCount).FirstOrDefault();
            Assert.NotNull(entity);
            Assert.AreEqual(1, entity1);
        }
        [Test]
        [Order(1)]
        public void GetProductCartByUserIdTest()
        {
            int userId = 2;
            var entity = cartRepository.GetCartByUserId(userId);
            Assert.NotNull(entity);
            Assert.AreEqual(1, entity.Item1.Count);
        }
    }
}
