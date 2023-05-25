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
    /// UserReposTest  class to perform nunit testing on UserRepository 
    /// </summary>
    public class UserReposTest
    {
        DatabaseContext databaseContext;
        List<User> users;
        UserRepository userRepos;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartAPP-db")
            .Options;
            databaseContext = new DatabaseContext(options);
            users = new List<User>()
            {
                new User() { UserId = 1,UserFName ="Amrutha",UserLName = "Ganesh", UserContact = "9015715522", UserEmail = "Ammu@gmail.com"},
                new User() {UserId = 2, UserFName = "Akash", UserLName = "M", UserContact ="7253727228", UserEmail= "AM@gmail.com"},
                new User() {UserId = 3, UserFName = "Preeti", UserLName = "Thakur", UserContact = "7654321890", UserEmail = "preeti@gmail.com"},
            };
            databaseContext.Users.AddRange(users);
            databaseContext.SaveChanges();
            userRepos = new UserRepository(databaseContext);

        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            databaseContext.RemoveRange(users);
            databaseContext.SaveChanges();
            databaseContext.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetAllUserTest()
        {
            var entity = userRepos.GettAllUsers();
            Assert.NotNull(entity);
            Assert.AreEqual(3, entity.Count());
        }
        [Test]
        [Order(2)]
        public void AddUsersTest()
        {
            List<UserView> users = new List<UserView>()
            {
                new UserView() { UserId = 4, UserFName = "TestName", UserLName = "UserLName", UserContact = "9786544456", UserEmail = "test@gmail.com" }
            };
            var entity = userRepos.AddUsers(users);
            var entity1 = userRepos.GettAllUsers();
            Assert.NotNull(entity);
            Assert.AreEqual(4, entity1.Count());
        }
        [Test]
        [Order(3)]
        public void UpdateUsersDetailsTest()
        {
            int id = 1;
            UserView userView = new UserView() { UserId = 1, UserFName = "Kalpana", UserLName = "Baradwaj", UserContact = "9015715522", UserEmail = "baradwaj@gmail.com" };
            var entity = userRepos.UpdateUserById(id, userView);
            databaseContext.SaveChanges();
            Assert.NotNull(entity);
            Assert.AreEqual(1, entity.UserId);
            Assert.AreEqual("Kalpana", entity.UserFName);
            Assert.AreEqual("Baradwaj", entity.UserLName);
            Assert.AreEqual("9015715522", entity.UserContact);
            Assert.AreEqual("baradwaj@gmail.com", entity.UserEmail);
        }
        [Test]
        [Order(4)]
        public void DeleteByIdTest()
        {
            int id = 4;
            userRepos.DeleteUserById(id);
            var entity = userRepos.GettAllUsers();
            Assert.AreEqual(3, entity.Count());
        }

    }
}
