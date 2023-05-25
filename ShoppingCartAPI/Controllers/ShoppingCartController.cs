using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCartAPI.Response;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        ICategoryRepository iCategoryRepository;
        IUserRepository iUserRepository;
        IAddressRepository iAddressRepository;
        IProductCartRepository iProductCart;
        ICartRespository iCartRepository;
        IOrderRepository iOrderRepository;
        IProductCatergoryRepository iProductCategoryRepository;

        private readonly ILogger<ShoppingCartController> _logger;
        public ShoppingCartController(ICategoryRepository iCategoryRepository, IUserRepository _iUserRepository, IAddressRepository _iAddressRepository, IProductCartRepository _iProductRepository, ICartRespository _iCartRepository, IOrderRepository _iorderRepository, IProductCatergoryRepository iproductrepo , ILogger<ShoppingCartController> logger)
        {
            ICategoryRepository _iCategoryRepository;
            iUserRepository = _iUserRepository;
            iAddressRepository = _iAddressRepository;
            iProductCart = iProductCart;
            iCartRepository = _iCartRepository;
            iOrderRepository = _iorderRepository;
            iProductCategoryRepository = iproductrepo;
            _logger = logger;
        }

        

        [HttpGet("Cart")]
        [Authorize]
        public Tuple<List<ProductCartView>, float> GetCartByUserId([FromHeader] int userId)
        {
            _logger.LogInformation("Cart executing...");
            try
            {
                return iCartRepository.GetCartByUserId(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }//get Cart details using user id
        [HttpPost("AddProductInCart")]
        [Authorize]
        public ResponseMessage AddFoodInCart([FromHeader] int productId, [FromHeader] int userId, [FromHeader] int cartCount)
        {
            _logger.LogInformation("Product Cart executing...");
            try
            {
                iProductCart.AddProductInCart(productId, userId, cartCount);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Product cart updated  successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Product cart could not updated  successfully" };
            }
        }//add items in cart using productid, userid and productcount
        [HttpDelete("RemoveProductFromCart")]
        [Authorize]
        public ResponseMessage RemoveFoodFromcart([FromHeader] int productId, [FromHeader] int userId, [FromHeader] int cartCount)
        {
            _logger.LogInformation("Food Item Cart executing...");
            try
            {
                iProductCart.RemoveProductFromcart(productId, userId, cartCount);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Product removed from cart  successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Product could not be removed from cart  successfully" };
            }
        }//update cart
    }
}
