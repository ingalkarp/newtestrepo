using System.Collections.Generic;
using System.Threading.Tasks;
using ECart.Dto;
using ECart.Interfaces;
using ECart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECart.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<List<OrdersDto>> Get(int userId)
        {
            return await Task.FromResult(_orderService.GetOrderList(userId)).ConfigureAwait(true);
        }
    }
}
