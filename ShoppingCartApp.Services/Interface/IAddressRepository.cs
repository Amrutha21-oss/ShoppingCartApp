using ShoppingCartApp.Services.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services.Interface
{

    public interface IAddressRepository
    {
        /// <summary>
        /// IAddressRepository interface for method declaration to perform CRUD opertaions over Address table.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<AddressView> GetAddressByUserId(int userId);  //using userid
        IEnumerable<AddressView> AddAddress(IEnumerable<AddressView> addressViews);
        public void DeleteAddress(int id);    //using addressid
        AddressView UpdateAddress(int id, AddressView addressView);    //using addressid
    }
}
