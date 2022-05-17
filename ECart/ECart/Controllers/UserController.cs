using ECart.Interfaces;
using ECart.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECart.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        readonly IUserService _userService;
        readonly ICartService _cartService;

        public UserController(IUserService userService, ICartService cartService)
        {
            _userService = userService;
            _cartService = cartService;
        }
     [HttpGet("{userId}")]
        public int Get(int userId)
        {
            int cartItemCount = _cartService.GetCartItemCount(userId);
            return cartItemCount;
        }
     [HttpGet]
        [Route("validateUserName/{userName}")]
        public bool ValidateUserName(string userName)
        {
            return _userService.CheckUserAvailabity(userName);
        }

          [HttpPost]
        public void Post([FromBody] UserMaster userData)
        {
            _userService.RegisterUser(userData);
        }
    }
}
