using ECart.Dto;
using ECart.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECart.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CheckOutController : Controller
    {
        readonly IOrderService _orderService;
        readonly ICartService _cartService;

        public CheckOutController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        
        [HttpPost("{userId}")]
        public int Post(int userId, [FromBody] OrdersDto checkedOutItems)
        {
            _orderService.CreateOrder(userId, checkedOutItems);
            return _cartService.ClearCart(userId);
        }
    }
}
