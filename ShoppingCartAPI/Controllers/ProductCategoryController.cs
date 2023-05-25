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
    public class ProductCategoryController : ControllerBase
    {
        ICategoryRepository iCategoryRepository;
        IUserRepository iUserRepository;
        IAddressRepository iAddressRepository;
        IProductCartRepository iProductCart;
        ICartRespository iCartRepository;
        IOrderRepository iOrderRepository;
        IProductCatergoryRepository iProductCategoryRepository;



        private readonly ILogger<ProductCategoryController> _logger;
        public ProductCategoryController(ICategoryRepository iCategoryRepository, IUserRepository _iUserRepository, IAddressRepository _iAddressRepository, IProductCartRepository _iProductRepository, ICartRespository _iCartRepository, IOrderRepository _iorderRepository, IProductCatergoryRepository iproductrepo, ILogger<ProductCategoryController> logger)
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
        [HttpGet("Category")]
        [Authorize]
        public IEnumerable<CategoryView> GetAllCategory()
        {
            _logger.LogInformation("Category Executing ....");
            try
            {
                return iCategoryRepository.GetAllCategory();
            }
            catch (Exception)
            {
                return Enumerable.Empty<CategoryView>();
            }

        }//get all category
        [HttpPost("Category")]
        [Authorize]
        public ResponseMessage AddCategory(IEnumerable<CategoryView> categoryView)
        {
            _logger.LogInformation("Category Executing ....");
            try
            {
                iCategoryRepository.AddCategory(categoryView);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "category added successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "category could not be added successfully" };
            }

        }//add category...can add multiple in a go
        [HttpPut("CategorybyId")]
        [Authorize]
        public ResponseMessage UpdateCategoryById([FromHeader] int id, CategoryView categoryView)
        {
            _logger.LogInformation("Category Executing ....");
            try
            {
                iCategoryRepository.UpdateCategoryById(id, categoryView);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "category updated successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "category could not be updated successfully" };
            }

        }//update category using category id
        [HttpDelete("CategorybyId")]
        [Authorize]
        public ResponseMessage DeleteCategoryById([FromHeader] int id)
        {
            _logger.LogInformation("Category Executing ....");
            try
            {
                iCategoryRepository.DeleteCategoryById(id);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "category deleted successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "category could not be deleted successfully" };
            }

        }//delete Product category using category id
        [HttpGet("ProductCategory")]
        [Authorize]
        public IEnumerable<ProductCategoryView> DisplayAll()
        {
            _logger.LogInformation("Product category Executing ....");
            try
            {
                return iProductCategoryRepository.DisplayAll();
            }
            catch (Exception)
            {
                return Enumerable.Empty<ProductCategoryView>();
            }

        }//get all Productcategory..this is a mapping table of Product and category table
        [HttpPost("ProductCategory")]
        [Authorize]
        public ResponseMessage AddCategory(int itemId, int categoryid)
        {
            _logger.LogInformation("Product category Executing ....");
            try
            {
                iProductCategoryRepository.AddProductCategory(itemId, categoryid);
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Product category added successfully" };
            }
            catch (Exception)
            {
                return new ResponseMessage { StatusCode = Response.StatusCode, Message = "Product category could not be added successfully" };
            }
        }//add Productcategory..this is a mapping table of Product and category table

    }
}
