using awsss.Resources.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Services.OrdersService.Command
{
    public record AddOrderCommand(List<int> ProductIds) :IRequest<OrderResponse>;
}
