using awsss.Resources.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Domain.Services
{
    public interface IOrderService
    {
        public Task<List<OrderResponse>> GetOrders();
        public Task<OrderResponse> AddOrder(CreateOrderRequest model);
    }
}
