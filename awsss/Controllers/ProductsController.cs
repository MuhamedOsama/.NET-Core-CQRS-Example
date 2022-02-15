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
using awsss.Resources.Product;
using MediatR;
using awsss.Services.ProductService.Query;
using awsss.Services.ProductService.Command;

namespace awsss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public ProductsController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> AddProduct(AddProductCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
