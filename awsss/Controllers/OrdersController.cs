using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using awsss.Domain.Models;
using awsss.Persistence;
using awsss.Domain.Services;
using awsss.Resources.Order;

namespace awsss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderService _service;

        public OrdersController(ApplicationDbContext context, IOrderService service)
        {
            _context = context;
            _service = service;

        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            return Ok(await _service.GetOrders());
        }
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> AddOrder(CreateOrderRequest model)
        {
            return Ok(await _service.AddOrder(model));
        }


    }
}
