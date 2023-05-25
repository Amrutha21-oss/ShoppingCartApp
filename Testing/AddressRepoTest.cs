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
    /// AddressReposTest class to perform nunit testing on Addressrepository
    /// </summary>
    public class AddressReposTest
    {
        DatabaseContext databaseContext;
        AddressRepository addressRepository;
        List<Address> addresses;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartAPP-db")
            .Options;
            databaseContext = new DatabaseContext(options);
            addresses = new List<Address>
            {
                new Address() {AddressId = 1, UserAddress = "Noid,UP", UserId = 1},
                new Address() {AddressId = 2,UserAddress = "Lkhnw,UP", UserId = 1},
                new Address() {AddressId = 3,UserAddress ="Delhi,New Delhi", UserId = 2},
            };
            databaseContext.Addresses.AddRange(addresses);
            databaseContext.SaveChanges();
            addressRepository = new AddressRepository(databaseContext);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            databaseContext.RemoveRange(addresses);
            databaseContext.SaveChanges();
            databaseContext.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetAddressByUserIdTest()
        {
            var userId = 1;
            var entity = addressRepository.GetAddressByUserId(userId);
            Assert.NotNull(entity);
            Assert.AreEqual(2, entity.Count());
        }
        [Test]
        [Order(2)]
        public void AddAddressTest()
        {
            List<AddressView> addressViews = new List<AddressView>()
            {
                new AddressView() {AddressId = 4, UserAddress = "Noid,UP", UserId = 2}
            };
            var entity = addressRepository.AddAddress(addressViews);
            var entity1 = addressRepository.GetAddressByUserId(2);
            Assert.NotNull(entity);
            Assert.AreEqual(2, entity1.Count);
        }
        [Test]
        [Order(3)]
        public void UpdateAdressbyUserIdTest()
        {
            int id = 4;
            AddressView addressView = new AddressView() { AddressId = 4, UserAddress = "Agra,UP", UserId = 2 };
            var entity = addressRepository.UpdateAddress(id, addressView);
            databaseContext.SaveChanges();
            Assert.NotNull(entity);
            Assert.AreEqual(4, entity.AddressId);
            Assert.AreEqual("Agra,UP", entity.UserAddress);
            Assert.AreEqual(2, entity.UserId);
        }
        [Test]
        [Order(4)]
        public void DeleteAddressTest()
        {
            int id = 4;
            addressRepository.DeleteAddress(id);
            var entity = addressRepository.GetAddressByUserId(2);
            Assert.AreEqual(1, entity.Count);
        }
    }
}