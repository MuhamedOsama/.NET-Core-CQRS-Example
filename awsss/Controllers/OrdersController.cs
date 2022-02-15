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
using awsss.Services.OrdersService.Query;
using MediatR;
using awsss.Services.OrdersService.Command;

namespace awsss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderService _service;
        private readonly IMediator _mediator;

        public OrdersController(ApplicationDbContext context, IOrderService service, IMediator mediator)
        {
            _context = context;
            _service = service;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> AddOrder(AddOrderCommand model)
        {
            return Ok(await _mediator.Send(model));
        }


    }
}
