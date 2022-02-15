using AutoMapper;
using awsss.Domain.Models;
using awsss.Persistence;
using awsss.Resources.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace awsss.Services.OrdersService.Command
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddOrderCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new();
            order.OrderProducts = new Collection<OrderProduct>();
            await _context.AddAsync(order, cancellationToken);
            foreach (var productId in request.ProductIds)
            {
                var isProductExist = await _context.Products.AnyAsync(r => r.Id == productId, cancellationToken: cancellationToken);
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
