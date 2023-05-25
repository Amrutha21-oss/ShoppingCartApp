using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;
using ShoppingCartApp.Services.Utility;


namespace ShoppingCartApp.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// UserRepository implements IUserRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        User user;
        public UserRepository(DatabaseContext _dataBaseContext)
        {
            dataBaseContext = _dataBaseContext;
            user = new User();
        }
        public IEnumerable<UserView> AddUsers(IEnumerable<UserView> userView)
        {
            try
            {
                foreach (UserView u in userView)
                {
                    PropertyCopy<UserView, User>.Copy(u, user);
                }
                dataBaseContext.Users.AddRange(user);
                dataBaseContext.SaveChanges();
                return userView;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public void DeleteUserById(int id)
        {
            try
            {
                User user = dataBaseContext.Users.FirstOrDefault(u => u.UserId == id);
                if (user != null)
                {
                    dataBaseContext.Remove(user);
                    dataBaseContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("User with  could not be found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public IEnumerable<UserView> GettAllUsers()
        {
            try
            {
                return dataBaseContext.Users.Select(u =>
                new UserView
                {
                    UserId = u.UserId,
                    UserFName = u.UserFName,
                    UserLName = u.UserLName,
                    UserContact = u.UserContact,
                    UserEmail = u.UserEmail
                }
                ).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<UserView>();
            }
        }

        public UserView UpdateUserById(int id, UserView userView)
        {
            try
            {
                var userData = dataBaseContext.Users.FirstOrDefault(u => u.UserId == id);
                if (userData != null)
                {
                    PropertyCopy<UserView, User>.Copy(userView, userData);
                    dataBaseContext.Entry<User>(userData).CurrentValues.SetValues(userData);
                    dataBaseContext.SaveChanges();
                }
                return userView;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }

}
