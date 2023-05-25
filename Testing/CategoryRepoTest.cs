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
    /// categoryReposTest  class to perform nunit testing on CategoryRepository
    /// </summary>
    public class FoodCategoryReposTest
    {
        DatabaseContext databaseContext;
        List<Category> categories;
        CategoryRepository categoryRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                         .UseInMemoryDatabase(databaseName: "ShoppingCartAPP-db")
                         .Options;
            databaseContext = new DatabaseContext(options);
            categories = new List<Category>()
            {
                new Category() {CategoryId = 1, CategoryName="Beauty"},
                new Category() {CategoryId = 2, CategoryName="Electronics"},
                new Category() {CategoryId = 3, CategoryName="Home Appliances"},
            };
            databaseContext.Categories.AddRange(categories);
            databaseContext.SaveChanges();
            categoryRepository = new CategoryRepository(databaseContext);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            databaseContext.RemoveRange(categories);
            databaseContext.SaveChanges();
            databaseContext.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetAllCategoryTest()
        {
            var entiry = categoryRepository.GetAllCategory();
            Assert.NotNull(entiry);
            Assert.AreEqual(3, entiry.Count());
        }
        [Test]
        [Order(2)]
        public void AddCategoriesTest()
        {
            List<CategoryView> categoryViews = new List<CategoryView>()
            {
                new CategoryView(){CategoryId=4, CategoryName="Watches"}
            };
            var entity = categoryRepository.AddCategory(categoryViews);
            var entity1 = categoryRepository.GetAllCategory();
            Assert.NotNull(entity);
            Assert.AreEqual(4, entity1.Count());
        }
        [Test]
        [Order(3)]
        public void UpdateCategoriesTest()
        {
            int id = 4;
            CategoryView categoryView = new CategoryView()
            {
                CategoryId = 4,
                CategoryName = "Test_Watches"
            };
            var entity = categoryRepository.UpdateCategoryById(id, categoryView);
            databaseContext.SaveChanges();
            Assert.NotNull(entity);
            Assert.AreEqual(4, entity.CategoryId);
            Assert.AreEqual("Test_Watches", entity.CategoryName);
        }
        [Test]
        [Order(4)]
        public void DeleteCategoryTest()
        {
            int id = 4;
            categoryRepository.DeleteCategoryById(id);
            var entity = categoryRepository.GetAllCategory();
            Assert.AreEqual(3, entity.Count());
        }
    }
}
