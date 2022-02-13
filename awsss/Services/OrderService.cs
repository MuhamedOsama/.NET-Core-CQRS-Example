using AutoMapper;
using awsss.Domain.Models;
using awsss.Domain.Services;
using awsss.Persistence;
using awsss.Resources.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderResponse>> GetOrders()
        {
            return _mapper.Map<List<OrderResponse>>(await _context.Orders.ToListAsync());
        }
        public async Task<OrderResponse> AddOrder(CreateOrderRequest model)
        {
            var order = new Order
            {
                
            };
            order.OrderProducts = new Collection<OrderProduct>();
            await _context.AddAsync(order);
            foreach (var productId in model.ProductIds)
            {
                var isProductExist = await _context.Products.AnyAsync(r => r.Id == productId);
                if (isProductExist)
                {
                    var product = await _context.Products.FindAsync(productId);
                    if (!product.IsFree)
                    {
                        order.TotalAmount += product.Price;
                    }
                    order.OrderProducts.Add(new OrderProduct { ProductId = productId, OrderId = order.Id });
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderResponse>(order);
        }
    }
}
