using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Interface
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// IFoodCategoriesRepository interface for method declaration to perform CRUD opertaions over FoodCategory table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryView> GetAllCategory();
        public IEnumerable<CategoryView> AddCategory(IEnumerable<CategoryView> categoryView);
        public CategoryView UpdateCategoryById(int id, CategoryView categoryView);
        void DeleteCategoryById(int id);
    }
}
