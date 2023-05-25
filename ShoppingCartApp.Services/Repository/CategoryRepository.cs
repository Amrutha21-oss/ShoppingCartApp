using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;
using ShoppingCartApp.Services.Utility;

namespace ShoppingCartApp.Services.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        /// <summary>
        /// Implements IFoodCategoryRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        Category category;
        public CategoryRepository(DatabaseContext _databaseContext)
        {
            dataBaseContext = _databaseContext;
            category = new Category();
        }
        public IEnumerable<CategoryView> AddCategory(IEnumerable<CategoryView> categoryView)
        {
            try
            {
                foreach (CategoryView fc in categoryView)
                {
                    PropertyCopy<CategoryView, Category>.Copy(fc, category);
                }
                dataBaseContext.Categories.AddRange(category);
                dataBaseContext.SaveChanges();
                return categoryView;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void DeleteCategoryById(int id)
        {
            try
            {
                Category category = dataBaseContext.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (category == null)
                {
                    Console.WriteLine("category not found");
                }
                else
                {
                    dataBaseContext.Categories.Remove(category);
                    dataBaseContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public IEnumerable<CategoryView> GetAllCategory()
        {
            try
            {
                return dataBaseContext.Categories.Select(category =>
                new CategoryView
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                }
                ).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CategoryView>();
            }
        }
        public CategoryView UpdateCategoryById(int id, CategoryView categoryView)
        {
            try
            {
                var newCategory = dataBaseContext.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (newCategory != null)
                {
                    PropertyCopy<CategoryView, Category>.Copy(categoryView, newCategory);
                    dataBaseContext.Entry<Category>(newCategory).CurrentValues.SetValues(newCategory);
                    dataBaseContext.SaveChanges();
                }
                return categoryView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
