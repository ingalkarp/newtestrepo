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

        /// <summary>
        /// Get the list of all the orders placed by a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<List<OrdersDto>> Get(int userId)
        {
            return await Task.FromResult(_orderService.GetOrderList(userId)).ConfigureAwait(true);
        }

        //[HttpGet]
        ////public async Task<List<OrdersDto>> Get()
        ////{
        ////    return await Task.FromResult(_orderService.GetOrderList()).ConfigureAwait(true);
        ////}

        //[HttpGet]
        //public async Task<List<Product>> Get()
        //{
        //    return await Task.FromResult(_productService.GetAllProducts()).ConfigureAwait(true);
        //}
    }
}
