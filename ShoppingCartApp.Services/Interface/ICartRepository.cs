using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface ICartRespository
    {
        /// <summary>
        /// IFoodCartRepository interface for method declaration to perform CRUD opertaions over FoodCart table.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Tuple<List<ProductCartView>, float> GetCartByUserId(int userId);
    }
}
