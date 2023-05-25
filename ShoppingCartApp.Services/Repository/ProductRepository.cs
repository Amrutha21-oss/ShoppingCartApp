using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;
using ShoppingCartApp.Services.Utility;

namespace ShoppingCartApp.Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// FoodItemsRepository implements IFoodItemRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        Product product;
        public ProductRepository(DatabaseContext _databaseContext)
        {
            dataBaseContext = _databaseContext;
            product = new Product();
        }

        public IEnumerable<ProductView> AddProducts(IEnumerable<ProductView> productView)
        {
            try
            {
                foreach (ProductView fi in productView)
                {
                    PropertyCopy<ProductView, Product>.Copy(fi, product);
                }
                dataBaseContext.Products.AddRange(product);
                dataBaseContext.SaveChanges();
                return productView;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void DeleteProductsById(int id)
        {
            try
            {
                Product product = dataBaseContext.Products.FirstOrDefault(i => i.ProductId == id);
                if (product != null)
                {
                    dataBaseContext.Remove(product);
                    dataBaseContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Items with the respective item id could not be found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public IEnumerable<ProductView> GetAllProducts()
        {
            try
            {
                return dataBaseContext.Products.Select(product =>
                new ProductView
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    ProductCount = product.ProductCount,
                }
                ).ToList();

            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ProductView>();
            }
        }

        public ProductView UpdateProductsById(int id, ProductView productView)
        {
            try
            {
                var newItem = dataBaseContext.Products.FirstOrDefault(i => i.ProductId == id);
                if (newItem != null)
                {
                    PropertyCopy<ProductView, Product>.Copy(productView, newItem);

                    dataBaseContext.Entry<Product>(newItem).CurrentValues.SetValues(newItem);
                    dataBaseContext.SaveChanges();
                }
                return productView;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
