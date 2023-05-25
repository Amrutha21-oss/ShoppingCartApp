using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services.ModelViews
{ 
 public class AddressView
  {
    public int AddressId { get; set; }
    public string UserAddress { get; set; }
    public int? UserId { get; set; }

   }
}
