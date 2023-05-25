using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;
using ShoppingCartApp.Services.Utility;

namespace ShoppingCartApp.Services.Repository
{
    public class ProductCatergoryRepository : IProductCatergoryRepository
    {
        /// <summary>
        /// ProductCatergoryRepository implements IFoodItemsCaregoryRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        ProductCategory productCategory;
        Product product;
        Category category;
        public ProductCatergoryRepository(DatabaseContext _databaseContext)
        {
            dataBaseContext = _databaseContext;
            productCategory = new ProductCategory();
            product = new Product();
            category = new Category();
        }

        public ProductCategoryView AddProductCategory(int productId, int categoryid)
        {
            ProductCategory mapingData = dataBaseContext.ProductCategories.FirstOrDefault(fc => fc.ProductId == productId && fc.ProductCategoryId == categoryid);
            ProductCategoryView iView = new ProductCategoryView();
            if (mapingData != null)
            {
                Console.WriteLine("already exists");
            }
            else
            {
                iView.ProductId = productId;
                iView.CategoryId = categoryid;
                PropertyCopy<ProductCategoryView, ProductCategory>.Copy(iView, productCategory);
                iView.ProductName = dataBaseContext.Products.FirstOrDefault(fc => fc.ProductId == iView.ProductId).ProductName;
                iView.CategoryName = dataBaseContext.Categories.FirstOrDefault(fc => fc.CategoryId == iView.CategoryId).CategoryName;
                iView.ProductDescription = dataBaseContext.Products.FirstOrDefault(fc => fc.ProductId == iView.ProductId).ProductDescription;
                iView.ProductPrice = dataBaseContext.Products.FirstOrDefault(fc => fc.ProductId == iView.ProductId).ProductPrice;
                dataBaseContext.ProductCategories.Add(productCategory);
                dataBaseContext.SaveChanges();
            }
            return iView;
        }

        public IEnumerable<ProductCategoryView> DisplayAll()
        {
            return dataBaseContext.ProductCategories.Select(productCategory =>
            new ProductCategoryView
            {
                ProductCategoryId = productCategory.ProductCategoryId,
                ProductId = productCategory.ProductId,
                CategoryId = productCategory.CategoryId,
                ProductName = dataBaseContext.Products.FirstOrDefault(f => f.ProductId == productCategory.ProductId).ProductName,
                CategoryName = dataBaseContext.Categories.FirstOrDefault(f => f.CategoryId == productCategory.ProductCategoryId).CategoryName,
                ProductPrice = dataBaseContext.Products.FirstOrDefault(f => f.ProductId == productCategory.ProductId).ProductPrice,
                ProductDescription = dataBaseContext.Products.FirstOrDefault(f => f.ProductId == productCategory.ProductId).ProductDescription

            }
            ).ToList();
        }
    }
}
