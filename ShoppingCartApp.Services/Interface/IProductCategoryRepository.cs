using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface IProductCatergoryRepository
    {
        /// <summary>
        /// IProductCatergoryRepository interface for method declaration to perform CRUD opertaions over Productcategory table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductCategoryView> DisplayAll();
        public ProductCategoryView AddProductCategory(int productId, int categoryid);
    }
}
