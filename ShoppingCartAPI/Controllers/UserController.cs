using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Response;
using ShoppingCartApp.Services.Interface;
using ShoppingCartApp.Services.ModelViews;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository iUserRepository;
        IAddressRepository iAddressRepository;
        IProductCartRepository iProductCart;
        ICartRespository iCartRepository;
        IOrderRepository iOrderRepository;
        IProductCatergoryRepository iProductCategoryRepository;
        

       private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository _iUserRepository, IAddressRepository _iAddressRepository, IProductCartRepository _iProductRepository, ICartRespository _iCartRepository, IOrderRepository _iorderRepository, IProductCatergoryRepository iproductrepo , ILogger<UserController> logger)
        {
            iUserRepository = _iUserRepository;
            iAddressRepository = _iAddressRepository;
            iProductCart = iProductCart;
            iCartRepository = _iCartRepository;
            iOrderRepository = _iorderRepository;
            iProductCategoryRepository = iproductrepo;
            _logger = logger;
        }
        [HttpGet("User")]
        [Authorize]
        public IEnumerable<UserView> GettAllUsers()
        {
            _logger.LogInformation("User Controller executing...");
            try
            {
                return iUserRepository.GettAllUsers();
            }
            catch (Exception)
            {
                return null;
            }

        }//get all users
        [HttpPost("User")]
        [Authorize]
        public ResponseMessage AddUsers(IEnumerable<UserView> userView)
        {
           _logger.LogInformation("User Controller executing...");
            try
            {
                iUserRepository.AddUsers(userView);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "New User added successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "User could not be added successfully" };
            }
        }//add users, can add multiple in a go
        [HttpPut("UserbyId")]
        [Authorize]
        public ResponseMessage UpdateUserById([FromHeader] int id, UserView userView)
        {
           _logger.LogInformation("User Controller executing...");
            try
            {
                iUserRepository.UpdateUserById(id, userView);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "User details updated successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "User details could not be updated successfully" };
            }
        }//update user details by userid
        [HttpDelete("UserbyId")]
        [Authorize]
        public ResponseMessage DeleteUserById([FromHeader] int id)
        {
            //_logger.LogInformation("User Controller executing...");
            try
            {
                iUserRepository.DeleteUserById(id);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Users data deleted successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Users data could not be deleted successfully" };
            }
        }//delete user dtaa using userid
        [HttpGet("AddressbyUserId")]
        [Authorize]
        public List<AddressView> GetAddressByUserId([FromHeader] int userId)
        {
            //_logger.LogInformation("Address Controller executing...");
            try
            {
                return iAddressRepository.GetAddressByUserId(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }//get address of user using user id
        [HttpPost("Address")]
        [Authorize]
        public ResponseMessage AddAddress(IEnumerable<AddressView> addressViews)
        {
           // _logger.LogInformation("Address Controller executing...");
            try
            {
                iAddressRepository.AddAddress(addressViews);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "new address for the user added successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "new address for the user could not be added successfully" };
            }
        }//add addresses
        [HttpPut("AddressbyAddressId")]
        [Authorize]

        public ResponseMessage UpdateAddress([FromHeader] int id, AddressView addressView)
        {
           // _logger.LogInformation("Address Controller executing...");
            try
            {
                iAddressRepository.UpdateAddress(id, addressView);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Address Data Updated successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Address Data could not be Updated successfully" };
            }
        }//update address using addressId
        [HttpDelete("AddressByAddressId")]
        [Authorize]
        public ResponseMessage DeleteAddress([FromHeader] int id)
        {
          //  _logger.LogInformation("Address Controller executing...");
            try
            {
                iAddressRepository.DeleteAddress(id);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Address Data deleted successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Address Data could not be deleted successfully" };
            }
        }//delete address using addressId
        
        
        
    }
}
