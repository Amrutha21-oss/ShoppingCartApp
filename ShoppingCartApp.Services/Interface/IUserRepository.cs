using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// IUserRepository interface for method declaration to perform CRUD opertaions over User table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserView> GettAllUsers();
        public IEnumerable<UserView> AddUsers(IEnumerable<UserView> userView);
        public UserView UpdateUserById(int id, UserView userView);
        void DeleteUserById(int id);
    }
}
