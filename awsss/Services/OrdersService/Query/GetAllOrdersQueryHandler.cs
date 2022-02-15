using AutoMapper;
using awsss.Persistence;
using awsss.Resources.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace awsss.Services.OrdersService.Query
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderResponse>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllOrdersQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<OrderResponse>>(orders);

        }
    }
}
