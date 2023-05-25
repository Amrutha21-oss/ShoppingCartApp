using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services.ModelViews
{
    /// <summary>
    /// UserView, contains columns of user table
    /// </summary>
    public class UserView
    {
        public int UserId { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public string UserContact { get; set; }
        public string UserEmail { get; set; }
    }
}
