using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface IProductCartRepository
    {
        /// <summary>
        /// IFoodItemCartRepository interface for method declaration to perform CRUD opertaions over FoodItemCart table.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="userId"></param>
        /// <param name="cartCount"></param>
        /// <returns></returns>
        public Tuple<List<ProductCartView>, float> AddProductInCart(int productId, int userId, int cartCount);
        public Tuple<List<ProductCartView>, float> RemoveProductFromcart(int productId, int userId, int cartCount);
    }
}
