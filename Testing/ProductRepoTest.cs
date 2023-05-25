using Microsoft.EntityFrameworkCore;
using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Repository;
using NUnit.Framework;
using ShoppingCartApp.Services.ModelViews;

namespace Testing
{
    /// <summary>
    /// ProductReposTest  class to perform nunit testing on ProductRepository
    /// </summary>
    public class FooItemRepoTest
    {
        DatabaseContext databaseContext;
        ProductRepository productrepository;
        List<Product> products;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingCartAPP-db")
                .Options;
            databaseContext = new DatabaseContext(options);
            products = new List<Product>()
            {
                new Product() {ProductId = 1,ProductName = "ipad", ProductDescription ="This is apple product", ProductPrice = 10, ProductCount = 5},
                new Product() {ProductId = 2,ProductName = "Redmi NoteBook", ProductDescription ="This is phone", ProductPrice = 20, ProductCount = 3},
                new Product() {ProductId = 3,ProductName = "Samsung", ProductDescription ="This is phone", ProductPrice = 50, ProductCount = 6},
            };
            databaseContext.Products.AddRange(products);
            databaseContext.SaveChanges();
            productrepository = new ProductRepository(databaseContext);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            databaseContext.RemoveRange(products);
            databaseContext.SaveChanges();
            databaseContext.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetALlProductTest()
        {
            var entity = productrepository.GetAllProducts();
            Assert.NotNull(entity);
            Assert.AreEqual(3, entity.Count());
        }

        [Test]
        [Order(2)]
        public void UpdateProductest()
        {
            int id = 4;
            ProductView productView = new ProductView() { ProductId = 4, ProductName = "NewItem_test", ProductDescription = "This is NewItem", ProductPrice = 20, ProductCount = 2 };
            var entity = productrepository.UpdateProductsById(id, productView);
            databaseContext.SaveChanges();
            Assert.NotNull(entity);
            Assert.AreEqual(4, entity.ProductId);
            Assert.AreEqual("NewItem_test", entity.ProductName);
            Assert.AreEqual("This is NewItem", entity.ProductDescription);
            Assert.AreEqual(20, entity.ProductPrice);
            Assert.AreEqual(2, entity.ProductCount);
        }
        [Test]
        [Order(3)]
        public void DeleteProductTest()
        {
            int id = 4;
            productrepository.DeleteProductsById(id);
            databaseContext.SaveChanges();
            var entity = productrepository.GetAllProducts();
            Assert.AreEqual(3, entity.Count());
        }
    }
}
