using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;
using ShoppingCartApp.Services.Utility;


namespace ShoppingCartApp.Services.Repository
{
    public class AddressRepository : IAddressRepository
    {
        /// <summary>
        /// implements IAddressRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        Address address;
        List<AddressView> view;

        public AddressRepository(DatabaseContext _dataBaseContext)
        {
            dataBaseContext = _dataBaseContext;
            address = new Address();
        }
        public IEnumerable<AddressView> AddAddress(IEnumerable<AddressView> addressViews)
        {
            try
            {
                foreach (AddressView view in addressViews)
                {
                    PropertyCopy<AddressView, Address>.Copy(view, address);

                }
                dataBaseContext.Addresses.AddRange(address);
                dataBaseContext.SaveChanges();
                return addressViews;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<AddressView>();
            }

        }

        public void DeleteAddress(int id)
        {
            try
            {
                address = dataBaseContext.Addresses.FirstOrDefault(a => a.AddressId == id);
                if (address != null)
                {
                    dataBaseContext.Addresses.Remove(address);
                    dataBaseContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public List<AddressView> GetAddressByUserId(int userId)
        {
            try
            {
                var user = dataBaseContext.Addresses.FirstOrDefault(a => a.UserId == userId);
                if (user != null)
                {
                    view = (from address in dataBaseContext.Addresses
                            where address.UserId == userId
                            select new AddressView
                            {
                                UserId = userId,
                                AddressId = address.AddressId,
                                UserAddress = address.UserAddress
                            }
                            ).ToList();

                }
                return view;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public AddressView UpdateAddress(int id, AddressView addressView)
        {

            var addressData = dataBaseContext.Addresses.FirstOrDefault(a => a.AddressId == id);
            if (addressData != null)
            {
                PropertyCopy<AddressView, Address>.Copy(addressView, addressData);
                dataBaseContext.Entry<Address>(addressData).CurrentValues.SetValues(addressData);
                dataBaseContext.SaveChanges();
            }
            return addressView;
        }
    }
}
