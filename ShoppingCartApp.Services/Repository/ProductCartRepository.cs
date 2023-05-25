using ShoppinCartApp.DataAccess.Context;
using ShoppinCartApp.DataAccess.Models;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;


namespace ShoppingCartApp.Services.Repository
{
    public class ProductCartRepository : IProductCartRepository
    {
        /// <summary>
        /// Implements IFoodItemcartRepository interface to define the mthods to perform CRUD operations.
        /// </summary>
        DatabaseContext dataBaseContext;
        ProductsCart productsCart;
        ICartRespository icartRepository;
        public ProductCartRepository(DatabaseContext _databaseContext, ICartRespository _ifcrepos)
        {
            dataBaseContext = _databaseContext;
            productsCart = new ProductsCart();
            icartRepository = _ifcrepos;
        }
        public Tuple<List<ProductCartView>, float> AddProductInCart(int productId, int userId, int cartCount)
        {
            Product product = dataBaseContext.Products.FirstOrDefault(i => i.ProductId == productId);
            Cart cart = dataBaseContext.Cart.FirstOrDefault(i => i.UserId == userId);
            if (cart != null && product.ProductCount >= cartCount)
            {
                ProductsCart productsCart = dataBaseContext.ProductsCart.FirstOrDefault(fc => fc.ProductId == productId && fc.CartId == cart.CartId);
                if (productsCart != null)
                {
                    productsCart.CartCount = productsCart.CartCount + cartCount;
                    product.ProductCount = product.ProductCount - cartCount;
                    productsCart.ProductId = productId;
                    productsCart.CartId = productsCart.CartId;
                    cart.CartPrice += cartCount * product.ProductPrice;
                    dataBaseContext.Update(product);
                    dataBaseContext.Update(cart);
                    dataBaseContext.Update(productsCart);
                    dataBaseContext.SaveChanges();
                }
                else if (productsCart == null)
                {
                    ProductsCart productsCart1 = new ProductsCart();
                    productsCart1.CartCount = cartCount;
                    product.ProductCount = product.ProductCount - cartCount;
                    cart.CartPrice += cartCount * product.ProductPrice;
                    productsCart1.ProductId = productId;
                    productsCart1.CartId = cart.CartId;
                    dataBaseContext.Add(productsCart1);
                    dataBaseContext.Update(product);
                    dataBaseContext.Update(cart);
                    dataBaseContext.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("Inventry is less than the requirement");
            }
            return icartRepository.GetCartByUserId(userId);
        }

        public Tuple<List<ProductCartView>, float> RemoveProductFromcart(int productId, int userId, int cartCount)
        {
            Product product = dataBaseContext.Products.FirstOrDefault(i => i.ProductId == productId);
            Cart cart = dataBaseContext.Cart.FirstOrDefault(i => i.UserId == userId);
            if (cart != null)
            {
                ProductsCart productsCart = dataBaseContext.ProductsCart.FirstOrDefault(fc => fc.ProductId == productId && fc.CartId == cart.CartId);
                if (productsCart != null && productsCart.CartCount >= cartCount)
                {
                    productsCart.CartCount = productsCart.CartCount - cartCount;
                    cart.CartPrice -= cartCount * product.ProductPrice;
                    product.ProductCount = product.ProductCount + cartCount;
                    if (productsCart.CartCount >= 1)
                    {
                        productsCart.CartId = productsCart.CartId;
                        productsCart.ProductId = productId;
                        dataBaseContext.Update(productsCart);
                        dataBaseContext.Update(product);
                        dataBaseContext.Update(cart);
                        dataBaseContext.SaveChanges();
                    }
                    else if (productsCart.CartCount == cartCount || productsCart.CartCount == null)
                    {
                        dataBaseContext.Update(product);
                        dataBaseContext.Update(cart);
                        dataBaseContext.Remove(productsCart);
                        dataBaseContext.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Items are lest than the count needed to be removed");
                    }

                }
            }
            return icartRepository.GetCartByUserId(userId);

        }
    }

}
