using AutoMapper;
using awsss.Domain.Models;
using awsss.Resources.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Mapping
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>().ForMember(src => src.OrderItems,des => des.MapFrom(r => r.OrderProducts.Select(r => r.Product)));
        }
    }
}
