using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// IFoodItemsRepository interface for method declaration to perform CRUD opertaions over FoodItems table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductView> GetAllProducts();
        IEnumerable<ProductView> AddProducts(IEnumerable<ProductView> productView);
        public ProductView UpdateProductsById(int id, ProductView productView);
        void DeleteProductsById(int id);
    }
}
