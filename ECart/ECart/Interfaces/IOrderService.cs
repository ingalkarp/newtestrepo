using ECart.Dto;
using System.Collections.Generic;

namespace ECart.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(int userId, OrdersDto orderDetails);
        List<OrdersDto> GetOrderList(int userId);

        //List<OrdersDto> GetOrderList();
    }
}
