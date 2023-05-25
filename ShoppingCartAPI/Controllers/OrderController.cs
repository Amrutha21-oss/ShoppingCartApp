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
    [ApiController]
    public class OrderController : ControllerBase
    {
        IUserRepository iUserRepository;
        IAddressRepository iAddressRepository;
        IProductCartRepository iProductCart;
        ICartRespository iCartRepository;
        IOrderRepository iOrderRepository;
        IProductCatergoryRepository iProductCategoryRepository;

        private readonly ILogger<OrderController> _logger;

        public OrderController(IUserRepository _iUserRepository, IAddressRepository _iAddressRepository, IProductCartRepository _iProductRepository, ICartRespository _iCartRepository, IOrderRepository _iorderRepository, IProductCatergoryRepository iproductrepo, ILogger<OrderController> logger)
        {
            iUserRepository = _iUserRepository;
            iAddressRepository = _iAddressRepository;
            iProductCart = iProductCart;
            iCartRepository = _iCartRepository;
            iOrderRepository = _iorderRepository;
            iProductCategoryRepository = iproductrepo;
            _logger = logger;
        }
        [HttpGet("OrderbyUserId")]
        [Authorize]
        public List<OrderView> GetOrderByUserid(int userId)
        {
            _logger.LogInformation("Order executing...");
            try
            {
                return iOrderRepository.GetOrderByUserid(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }//get order details using userid

        [HttpPost("Order")]
        [Authorize]
        public ResponseMessage AddOrder([FromHeader] int userId, [FromHeader] int addressId)
        {
            _logger.LogInformation("Order executing...");
            try
            {
                iOrderRepository.AddOrder(userId, addressId);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Order placed successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Order couldn't be placed." };
            }
        }//add orders using userid and addressid
    }
}
