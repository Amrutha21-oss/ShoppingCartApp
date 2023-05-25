using ShoppinCartApp.DataAccess.Context;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Repository
{
    public class CartRepository : ICartRespository
    {
        /// <summary>
        /// Implements IFoodCartRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        List<ProductCartView> view;
        public CartRepository(DatabaseContext _databaseContext)
        {
            dataBaseContext = _databaseContext;
        }
        public Tuple<List<ProductCartView>, float> GetCartByUserId(int userId)
        {
            var user = dataBaseContext.Cart.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                view = (from cart1 in dataBaseContext.Cart
                        join productcart1 in dataBaseContext.ProductsCart on cart1.CartId equals productcart1.CartId
                        join product1 in dataBaseContext.Products on productcart1.ProductId equals product1.ProductId
                        where cart1.UserId == userId
                        select new ProductCartView
                        {
                            ProductId = productcart1.ProductId,
                            CartCount = productcart1.CartCount,
                            ProductName = product1.ProductName,
                            ProductPrice = product1.ProductPrice,

                        }

                       ).ToList();

            }
            return new Tuple<List<ProductCartView>, float>(view, user.CartPrice);
        }
    }
}
