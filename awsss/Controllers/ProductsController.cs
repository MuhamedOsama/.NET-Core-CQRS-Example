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

namespace awsss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _service;
        public ProductsController(ApplicationDbContext context, IProductService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            return Ok(await _service.GetProducts());
        }
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> AddProduct(CreateProductRequest model)
        {
            return Ok(await _service.AddProduct(model));
        }
    }
}
