using awsss.Resources.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Services.OrdersService.Query
{
    public record GetAllOrdersQuery() : IRequest<List<OrderResponse>>;
}
